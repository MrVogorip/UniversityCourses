using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using branch_and_bound.visualization.Algorithm;
using Microsoft.Win32;

namespace branch_and_bound.visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Point> points;
        private int[,] MatrixWay;
        private int Count_Cities;
        static public Thread thread;
        public MainWindow()
        {
            InitializeComponent();
            points = new List<Point>();
            Count_Cities = 0;
            ReloadButton.IsEnabled = false; // 
            Closed += EndProgram;
        }
        private void EndProgram(object sender, EventArgs e)
        {
            if (thread != null)
                thread.Abort();
            Close();
        }
        void DrawCities(int x,int y)
        {
            Point point = new Point(x, y);
            Ellipse ellipses = new Ellipse()
            {
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 15,
                Height = 15,
                Margin = new Thickness(point.X - 15, point.Y - 15, 0, 0),
            };
            TextBlock textBlocks = new TextBlock()
            {
                Text = Count_Cities.ToString(),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 15,
                Height = 15,
                TextAlignment = TextAlignment.Center,
                Margin = new Thickness(point.X - 15, point.Y - 15, 0, 0)
            };
            DrawCitiesWindow.Children.Add(ellipses);
            DrawCitiesWindow.Children.Add(textBlocks);
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Count_Cities++;
                if (Count_Cities > (int)CitiesRandomInput.Maximum)
                    throw new Exception("Error");
                int CursorX = (int)Mouse.GetPosition(null).X;
                int CursorY = (int)Mouse.GetPosition(null).Y;
                points.Add(new Point(CursorX, CursorY));
                DrawCities(CursorX, CursorY);
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CitiesRandomInput.IsEnabled = false;
                FindWayButton.IsEnabled = false;
                RandomButton.IsEnabled = false;
                MatrixWay = new int[points.Count, points.Count];
                for (int i = 0; i < points.Count; i++)
                {
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (i == j)
                            MatrixWay[i, j] = GlobalVar.INF;
                        else
                            MatrixWay[i, j] = (int)Math.Sqrt((points[i].X - points[j].X) * (points[i].X - points[j].X) + (points[i].Y - points[j].Y) * (points[i].Y - points[j].Y));
                    }
                }
                GlobalVar.N = points.Count;
                List<Tuple<int, int>> res = AlgorithmBranchAndBound.FindWay(MatrixWay);
                string result = "";
                thread = new Thread(delegate ()
                {
                    for (int i = 0; i < res.Count; i++)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                        {
                            Line line = new Line();
                            result += String.Format("{0,-2}", res[i].Item1 + 1);
                            result += String.Format(" -------> ");
                            result += String.Format("{0,-2}", res[i].Item2 + 1);
                            result += "\n";
                            line.X1 = points[res[i].Item1].X - 10;
                            line.Y1 = points[res[i].Item1].Y - 10;
                            line.X2 = points[res[i].Item2].X - 10;
                            line.Y2 = points[res[i].Item2].Y - 10;
                            line.Stroke = Brushes.Green;
                            line.StrokeThickness = 4;
                            ResultLbl.Content = "Route :\n" + result;
                            DrawLineWindow.Children.Add(line);
                        });
                        Thread.Sleep(500);
                    }
                });
                thread.Start();
                ImageWindow.IsEnabled = false;
                ReloadButton.IsEnabled = true;
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
                Reload_Click(sender, e);
            }
        }
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DrawLineWindow.Children.Clear();
                DrawCitiesWindow.Children.Clear();
                Count_Cities = 0;
                points = new List<Point>();
                int n = (int)CitiesRandomInput.Value;
                Random random = new Random();
                for (int i = 0; i < n; i++)
                {
                    Count_Cities++;
                    if (Count_Cities > (int)CitiesRandomInput.Maximum)
                        throw new Exception("Error");
                    int CursorX = 0;
                    int CursorY = 0;
                    bool tooClose = false;
                    do
                    {
                        CursorX = random.Next(50, 600);
                        CursorY = random.Next(50, 300);
                        tooClose = false;
                        if (i == 0) break;
                        for (int j = 0; j < points.Count; j++)
                        {
                            if ((int)Math.Sqrt((CursorX - points[j].X) * (CursorX - points[j].X) + (CursorY - points[j].Y) * (CursorY - points[j].Y)) < 60)
                            {
                                tooClose = true;
                                break;
                            }
                        }
                    } while (tooClose);
                    points.Add(new Point(CursorX, CursorY));
                    DrawCities(CursorX, CursorY);
                }
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }   
        private void CitiesRandomInput_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Quantity.Content = ((int)CitiesRandomInput.Value).ToString();
        }
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            if (thread != null)
                thread.Abort();
            DrawLineWindow.Children.Clear();
            DrawCitiesWindow.Children.Clear();
            ResultLbl.Content = "";
            points = new List<Point>();
            Count_Cities = 0;
            CitiesRandomInput.IsEnabled = true;
            FindWayButton.IsEnabled = true;
            RandomButton.IsEnabled = true;
            ImageWindow.IsEnabled = true;
            ReloadButton.IsEnabled = false;
        }
        private void OpenFile(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text documents (.txt)|*.txt";
                openFileDialog.ShowDialog();
                string temp = File.ReadAllText(openFileDialog.FileName);
                string[] Lines = temp.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                GlobalVar.N = Lines.Length;
                int[,] costMatrix = new int[GlobalVar.N, GlobalVar.N];
                for (int i = 0; i < costMatrix.GetLength(0); i++)
                {
                    string[] Items = Lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Count_Cities++;
                    if (Count_Cities > (int)CitiesRandomInput.Maximum)
                        throw new Exception("Error");
                    int X = 0;
                    int Y = 0;
                    if (!Int32.TryParse(Items[0], out X))
                        throw new Exception("Error");
                    if (!Int32.TryParse(Items[1], out Y))
                        throw new Exception("Error");
                    points.Add(new Point(X, Y));
                    DrawCities(X, Y);
                }
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text documents (.txt)|*.txt";
                saveFileDialog.ShowDialog();
                File.WriteAllText(saveFileDialog.FileName, ResultLbl.Content.ToString());
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.Message);
            }
        }
        private void CloseProgram(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
