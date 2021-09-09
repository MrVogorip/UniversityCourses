using System;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;

namespace sorting_visualization
{
    public partial class MainWindow : Window
    {
        private int n = 20;
        private int d = 100;
        private double r = 130;
        private double a = 140, b = 230;
        private int[] numbers;
        private Line[] lines;
        private Thread SortingThread;

        public MainWindow()
        {
            InitializeComponent();
            Closed += EndProgram;
            InitializeRandom();
        }
        private int[] ArrayGeneration()
        {
            Random random = new Random();
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
                numbers[i] = random.Next(5, 175);
            return numbers;
        }
        private void InitializeLines(int[] array)
        {
            DrawingCanvas.Children.Clear();
            lines = new Line[array.Length];
            for (int i = 0; i < array.Length; i++)
                AddLine((double)(i + 1) * 30, array[i], i);
        }
        private void AddLine(double q, int gr, int i)
        {
            Line line = new Line();
            line.X1 = a + q;
            line.Y1 = b;
            line.X2 = -Math.Cos(((double)gr / 180) * Math.PI) * r + line.X1;
            line.Y2 = -Math.Sin(((double)gr / 180) * Math.PI) * r + line.Y1;
            line.StrokeThickness = 2;
            line.Stroke = Brushes.Red;
            lines[i] = line;
            DrawingCanvas.Children.Add(line);
        }
        private void EndProgram(object sender, EventArgs e)
        {
            if (SortingThread != null)
                SortingThread.Abort();
            Close();
        }
        private void RunSorting()
        {
            RunButton.IsEnabled = false;
            RandomButton.IsEnabled = false;
            InputN.IsEnabled = false;
            InputDelay.IsEnabled = false;
            SortingThread = new Thread(delegate ()
            {
                Sorting();
            });
            SortingThread.Start();
        }
        private void Sorting()
        {
            numbers = BulbSort(numbers);
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                RunButton.IsEnabled = true;
                RandomButton.IsEnabled = true;
                InputN.IsEnabled = true;
                InputDelay.IsEnabled = true;
            });
        }
        private int[] BulbSort(int[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    Orange(i, j);
                    if (input[j] < input[i])
                    {
                        int temp = input[j];
                        input[j] = input[i];
                        input[i] = temp;
                        SwapLines(i, j);
                        PrintArray(numbers);
                    }
                    Thread.Sleep(d);
                    Red(i, j);
                }
                Geen(i);
            }
            return input;
        }
        private void Orange(int index1, int index2)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                lines[index1].Stroke = Brushes.Orange;
                lines[index2].Stroke = Brushes.Orange;
            });
        }
        private void Red(int index1, int index2)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                lines[index1].Stroke = Brushes.Red;
                lines[index2].Stroke = Brushes.Red; 
            });
        }
        private void Geen(int index1)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                lines[index1].Stroke = Brushes.Green; 
            });
        }
        private void SwapLines(int index1, int index2)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                double Onex1 = lines[index1].X1;
                double Oney1 = lines[index1].Y1;
                double Twox1 = lines[index2].X1;
                double Twoy1 = lines[index2].Y1;
                lines[index2].X1 = Onex1;
                lines[index2].Y1 = Oney1;
                lines[index2].X2 = -Math.Cos(((double)numbers[index2] / 180) * Math.PI) * r + lines[index2].X1;
                lines[index2].Y2 = -Math.Sin(((double)numbers[index2] / 180) * Math.PI) * r + lines[index2].Y1;
                lines[index1].X1 = Twox1;
                lines[index1].Y1 = Twoy1;
                lines[index1].X2 = -Math.Cos(((double)numbers[index1] / 180) * Math.PI) * r + lines[index1].X1;
                lines[index1].Y2 = -Math.Sin(((double)numbers[index1] / 180) * Math.PI) * r + lines[index1].Y1;
            });
            Line temp = lines[index1];
            lines[index1] = lines[index2];
            lines[index2] = temp;
        }
        private void InitializeRandom()
        {
            n = (int)InputN.Value;
            d = (int)InputDelay.Value;
            numbers = ArrayGeneration();
            InitializeLines(numbers);
            OutputTextBox.Children.Clear();
            foreach (int i in numbers)
            {
                Label label = new Label();
                label.Background = Brushes.Wheat;
                label.FontSize = 30;
                label.Content = i;
                label.BorderThickness = new Thickness(4);
                OutputTextBox.Children.Add(label);
            }
        }
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeRandom();
        }
        private void InputN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Quantity.Content = ((int)InputN.Value).ToString();
        }
        private void InputDelay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Delay.Content = ((int)InputDelay.Value).ToString();
        }
        private void PrintArray(int[] array)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                OutputTextBox.Children.Clear();
                foreach (int i in array)
                {
                    Label label = new Label();
                    label.Background = Brushes.Wheat;
                    label.FontSize = 30;
                    label.Content = i;
                    label.BorderThickness = new Thickness(4);
                    OutputTextBox.Children.Add(label);
                }
            });
        }
        private void RunSortingButton_Click(object sender, RoutedEventArgs e)
        {
            RunSorting();
        }
    }
}
