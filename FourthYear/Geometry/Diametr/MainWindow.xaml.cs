using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PointF = System.Drawing.PointF;

namespace Diametr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<PointF> Polygon = new List<PointF>();
        PointF PI;
        PointF PJ;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.ShowDialog();
                string path = saveFileDialog.FileName;
                if (!string.IsNullOrEmpty(path))
                {
                    using (var sw = new StreamWriter(path))
                    {
                        sw.WriteLine(PI.X + " " + PI.Y);
                        sw.WriteLine(PJ.X + " " + PJ.Y);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(path))
                {
                    using (var sr = new StreamReader(path))
                    {
                        var words = sr.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < words.Length; i++)
                        {
                            var cordi = words[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            Polygon.Add(new PointF(
                                 int.Parse(cordi[0]),
                                 int.Parse(cordi[1])));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private double CrossProduct(PointF point1, PointF point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var max = 0.0;
            for (int i = 0; i < Polygon.Count; i++)
            {
                for (int j = i + 1; j < Polygon.Count; j++)
                {
                    var prod = CrossProduct(Polygon[i], Polygon[j]);
                    if (prod >= max)
                    {
                        max = prod;
                        PI = Polygon[i];
                        PJ = Polygon[j];
                    }
                }
            }

            Line line = new Line();
            line.StrokeThickness = 2;
            line.X1 = Polygon[0].X;
            line.X2 = Polygon[Polygon.Count - 1].X;
            line.Y2 = Polygon[Polygon.Count - 1].Y;
            line.Y1 = Polygon[0].Y;
            line.Stroke = Brushes.Black;
            Panel.Children.Add(line);
            for (int i = 0; i < Polygon.Count - 1; i++)
            {
                line = new Line();
                line.StrokeThickness = 2;
                line.X1 = Polygon[i].X;
                line.X2 = Polygon[i + 1].X;
                line.Y2 = Polygon[i + 1].Y;
                line.Y1 = Polygon[i].Y;
                line.Stroke = Brushes.Black;
                Panel.Children.Add(line);
            }

            line = new Line();
            line.StrokeThickness = 4;
            line.X1 = PI.X;
            line.X2 = PJ.X;
            line.Y1 = PI.Y;
            line.Y2 = PJ.Y;
            line.Stroke = Brushes.Red;
            Panel.Children.Add(line);
        }
    }
}
