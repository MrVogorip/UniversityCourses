using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Listing
{
    class WorkItem
    {
        public int WorkDuration
        {
            get;
            set;
        }
        public void performWork()
        {
            Thread.Sleep(WorkDuration);
        }
    }
    class ContextPartitioner : OrderablePartitioner<WorkItem>
    {
        protected WorkItem[] dataItems;
        protected int targetSum;
        private long sharedStartIndex = 0;
        private object lockObj = new object();
        private EnumerableSource enumSource;
        public ContextPartitioner(WorkItem[] data, int target) :
            base(true, false, true)
        {
            dataItems = data;
            targetSum = target;
            enumSource = new EnumerableSource(this);
        }

        public override bool SupportsDynamicPartitions
        {
            get
            {
                return true;
            }
        }

        public override IList<IEnumerator<KeyValuePair<long, WorkItem>>>
        GetOrderablePartitions(int partitionCount)
        {
            // create the list which will be the result
            IList<IEnumerator<KeyValuePair<long, WorkItem>>> partitionsList = new List<IEnumerator<KeyValuePair<long, WorkItem>>>();
            // get the IEnumerable that will generate dynamic partitions
            IEnumerable<KeyValuePair<long, WorkItem>> enumObj =
            GetOrderableDynamicPartitions();
            // create the required number of partitions
            for (int i = 0; i < partitionCount; i++)
            {
                partitionsList.Add(enumObj.GetEnumerator());
            }
            // return the result
            return partitionsList;
        }
        public override IEnumerable<KeyValuePair<long, WorkItem>>
        GetOrderableDynamicPartitions()
        {
            return enumSource;
        }
        private Tuple<long, long> getNextChunk()
        {
            Tuple<long, long> result;
            lock (lockObj)
            {
                if (sharedStartIndex < dataItems.Length)
                {
                    int sum = 0;
                    long endIndex = sharedStartIndex;
                    while (endIndex < dataItems.Length && sum < targetSum)
                    {
                        sum += dataItems[endIndex].WorkDuration;
                        endIndex++;
                    }
                    result = new Tuple<long, long>(sharedStartIndex, endIndex);
                    sharedStartIndex = endIndex;
                }
                else
                {
                    result = new Tuple<long, long>(-1, -1);
                }
            }
            return result;
        }

        class EnumerableSource : IEnumerable<KeyValuePair<long, WorkItem>>
        {
            ContextPartitioner parentPartitioner;
            public EnumerableSource(ContextPartitioner parent)
            {
                parentPartitioner = parent;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<WorkItem>)this).GetEnumerator();
            }
            IEnumerator<KeyValuePair<long, WorkItem>>
            IEnumerable<KeyValuePair<long, WorkItem>>.GetEnumerator()
            {
                return new
                ChunkEnumerator(parentPartitioner).GetEnumerator();
            }
        }
        class ChunkEnumerator
        {
            private ContextPartitioner parentPartitioner;
            public ChunkEnumerator(ContextPartitioner parent)
            {
                parentPartitioner = parent;
            }

            public IEnumerator<KeyValuePair<long, WorkItem>> GetEnumerator()
            {
                while (true)
                {
                    Tuple<long, long> chunkIndices =
                    parentPartitioner.getNextChunk();
                    if (chunkIndices.Item1 == -1 && chunkIndices.Item2 == -1)
                    { break; }
                    else
                    {
                        for (long i = chunkIndices.Item1; i < chunkIndices.Item2; i++)
                        {
                            yield return new KeyValuePair<long, WorkItem>(
                            i, parentPartitioner.dataItems[i]);
                        }
                    }
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            WorkItem[] sourceData = new WorkItem[10000];
            for (int i = 0; i < sourceData.Length; i++)
            {
                sourceData[i] = new WorkItem()
                {
                    WorkDuration = rnd.Next(1, 11)
                };
            }
            WorkItem[] resultData = new WorkItem[sourceData.Length];
            OrderablePartitioner<WorkItem> cPartitioner =
            new ContextPartitioner(sourceData, 100);

            Parallel.ForEach(cPartitioner, (WorkItem item,
            ParallelLoopState loopState, long index) =>
            {
                item.performWork();
                resultData[index] = item;
            });

            for (int i = 0; i < sourceData.Length; i++)
            {
                if (sourceData[i].WorkDuration !=
                resultData[i].WorkDuration)
                {
                    Console.WriteLine("Discrepancy at index {0}", i);
                    break;
                }
            }
            Console.WriteLine("Press enter to finish"); Console.ReadLine();
        }
    }
}
