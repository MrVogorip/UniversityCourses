using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
namespace QuickSort
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader fin = new StreamReader("input.txt");
            StreamWriter fout = new StreamWriter("output.txt");
            string str = "";
            string str1 = "";
            str1 = fin.ReadLine();
            str = fin.ReadLine();
            fin.Close();
            int N= Int32.Parse(str1);
            if (1 > N || N > 100000)
            {
                return;
            }
            int[] mas = new int[150000];
            int count = 0;
            string[] words = str.Split(new char[] { ' ' });
            foreach (string s in words)
            {
                mas[count] = Int32.Parse(s);
                count++;
            }
            Quicksort(mas, 0, N - 1);
            string re="";
            for (int i = 0; i < N; i++)
            {
                re += mas[i]+" ";
            }
            fout.Write(re);
            fout.Close();
        }
        static private void Quicksort(int[] ar, int left, int right)
        {
            if (left == right) return;
            int i = left + 1;
            int j = right;
            int pivot = ar[left];
            while (i < j)
            {
                if (ar[i] <= pivot) i++;
                else if (ar[j] > pivot) j--;
                else
                {
                    int m = ar[i]; ar[i] = ar[j]; ar[j] = m;
                }
            }
            if (ar[j] <= pivot)
            {
                int m = ar[left]; ar[left] = ar[right]; ar[right] = m;
                Quicksort(ar, left, right - 1);
            }
            else
            {
                Quicksort(ar, left, i - 1);
                Quicksort(ar, i, right);
            }
        }
    }
}
