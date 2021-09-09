using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PointF = System.Drawing.PointF;

namespace PointAffiliation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<PointF> Polygon = new List<PointF>();
        private List<PointF> Points = new List<PointF>();
        Polygon polygon = new Polygon();
        Dot dot0 = new Dot();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                            Points.Add(new PointF(
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            foreach (var item in Polygon)
                polygon.DotAdd(new Dot(item.X, item.Y));

            polygon.Build();
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


            for (int i = 0; i < Points.Count; i++)
            {
                line = new Line();
                line.StrokeThickness = 8;
                line.X1 = Points[i].X;
                line.X2 = Points[i].X + 4;
                line.Y2 = Points[i].Y + 4;
                line.Y1 = Points[i].Y;
                dot0.Set(Points[i].X, Points[i].Y);

                if (polygon.TestDotOnPolygon(dot0))
                    line.Stroke = Brushes.Red;
                else
                    line.Stroke = Brushes.Blue;
                Panel.Children.Add(line);
            }
        }
    }
}
