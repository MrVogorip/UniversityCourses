using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace graph_visualization
{
    public partial class MainWindow : Window
    {
        private const int Xindent = 50;
        private const int Yindent = 50;
        private const int Radius = 280;
        private Ellipse[] ellipses;
        private TextBlock[] textBlocks;
        private List<Line> Lines = new List<Line>();
        private Table table;
        private int n = 0;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_InputData(object sender, RoutedEventArgs e)
        {
            try
            {
                table = new Table();
                ellipses = null;
                textBlocks = null;
                DrawWindow.Children.Clear();
                PanelTier.Children.Clear();
                table.Start(txtBoxInput.Text);
                n = table.n;
                ellipses = new Ellipse[n];
                textBlocks = new TextBlock[n];
                Draw1.IsEnabled = true;
                Draw2.IsEnabled = true;

            }
            catch (Exception ex)
            {
                MessageBoxResult result = MessageBox.Show(ex.Message);
            }
        }
        private void Button_Bild1(object sender, RoutedEventArgs e)
        {
            bild_circles1();
            build();
        }
        private void Button_Bild2(object sender, RoutedEventArgs e)
        {
            bild_circles2();
            build();
        }

        private void build()
        {
            bild_line(table.Tabl[0]);
            for (int i = 0; i < table.Tier.Count; i++)
            {
                Label label = new Label();
                label.Content = table.Tier[i];
                PanelTier.Children.Add(label);

            }
            Label characteristic = new Label();
            characteristic.Content = "1)Count Diameter = " + (table.Tabl.Count - 1).ToString()
                + "\n2)Graph radius = " + table.FindRadiusAndCentre().Item1
                + "\n3)Graph center = " + table.FindRadiusAndCentre().Item2
                + "\n4)Numbers of vertices for which eccentricity coincides with the diameter = " + table.LastTier()
                + "\n5)Connected graph = " + table.Connectivity(); 
            PanelTier.Children.Add(characteristic);
            table = null;
            Draw1.IsEnabled = false;
            Draw2.IsEnabled = false;
        }
        private void bild_circles2()
        {
            int m = n / 2;
            if (n % 2 == 1)
                m++;
            for (int i = 0; i < n; i++)
            {
                double rad = 0;
                double x = 0;
                double y = 0;
                if (i < m)
                {
                    rad = i * ((double)360 / m) / 180 * Math.PI;
                    x = (Math.Cos(rad) * Radius / 2 + Xindent) + 260;
                    y = (Math.Sin(rad) * Radius / 2 + Yindent) + 260;
                }
                else
                {
                    rad = i * ((double)360 / (n - m)) / 180 * Math.PI;
                    x = (Math.Cos(rad) * Radius + Xindent) + 260;
                    y = (Math.Sin(rad) * Radius + Yindent) + 260;
                }
                ellipses[i] = new Ellipse()
                {
                    Stroke = Brushes.Red,
                    Fill = Brushes.DeepSkyBlue,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = Xindent,
                    Height = Yindent,
                    Margin = new Thickness(x, y, 0, 0),
                };
                textBlocks[i] = new TextBlock()
                {
                    Text = (i + 1).ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = Xindent,
                    Height = Yindent,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(x, y, 0, 0)
                };
                DrawWindow.Children.Add(ellipses[i]);
                DrawWindow.Children.Add(textBlocks[i]);
            }
        }
        private void bild_circles1()
        {
            for (int i = 0; i < n; i++)
            {
                double rad = i * ((double)360 / n) / 180 * Math.PI;
                double x = (Math.Cos(rad) * Radius + Xindent) + 260;
                double y = (Math.Sin(rad) * Radius + Yindent) + 260;
                ellipses[i] = new Ellipse()
                {
                    Stroke = Brushes.Red,
                    Fill = Brushes.DeepSkyBlue,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = Xindent,
                    Height = Yindent,
                    Margin = new Thickness(x, y, 0, 0),
                };
                textBlocks[i] = new TextBlock()
                {
                    Text = (i + 1).ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = Xindent,
                    Height = Yindent,
                    TextAlignment = TextAlignment.Center,
                    Margin = new Thickness(x, y, 0, 0)
                };
                DrawWindow.Children.Add(ellipses[i]);
                DrawWindow.Children.Add(textBlocks[i]);
            }
        }
        private void bild_line(List<int>[] Tier)
        {
            for (int i = 0; i < Tier.Length; i++)
            {
                for (int j = 0; j < Tier[i].Count; j++)
                {
                    Line line = new Line();
                    line.VerticalAlignment = ellipses[i].VerticalAlignment;
                    line.HorizontalAlignment = ellipses[i].HorizontalAlignment;
                    line.X1 = ellipses[i].Margin.Left + 20;
                    line.Y1 = ellipses[i].Margin.Top + 20;
                    line.X2 = ellipses[Tier[i][j] - 1].Margin.Left + 20;
                    line.Y2 = ellipses[Tier[i][j] - 1].Margin.Top + 20;
                    line.Stroke = Brushes.Red;
                    DrawWindow.Children.Add(line);
                    Lines.Add(line);
                }
            }
        }
    }
}
