using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace planimetry
{
    public partial class MainWindow : Window
    {
        double R1, X1, Y1, R2, X2, Y2;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Platform.Children.Clear();
            R1 = double.Parse(r1.Text);
            X1 = double.Parse(x1.Text);
            Y1 = double.Parse(y1.Text);
            R2 = double.Parse(r2.Text);
            X2 = double.Parse(x2.Text);
            Y2 = double.Parse(y2.Text);
            DrawCircle(R1, X1, Y1);
            DrawCircle(R2, X2, Y2);
            double d = Math.Sqrt((X2 - X1) * (X2 - X1) + (Y2 - Y1) * (Y2 - Y1));
            if (d > R1 + R2 || d < Math.Abs(R1 - R2))
            {
                Output.Content = "there are no solutions, the circles lie separately";
            }
            else
            {
                double a = (R1 * R1 - R2 * R2 + d * d) / (2 * d);
                double h = Math.Sqrt(R1 * R1 - a * a);
                double x3 = X1 + (a / d) * (X2 - X1);
                double y3 = Y1 + (a / d) * (Y2 - Y1);
                double x4 = x3 + (h / d) * (Y2 - Y1);
                double y4 = y3 - (h / d) * (X2 - X1);
                double x5 = x3 - (h / d) * (Y2 - Y1);
                double y5 = y3 + (h / d) * (X2 - X1);
                DrawPoint(x4, y4);
                DrawPoint(x5, y5);
                Output.Content = $"Intersection (x,y):" +
                    $"\n({Math.Round(x4,3)},{Math.Round(y4,3)})\n({Math.Round(x5,3)},{Math.Round(y5,3)})";
            }
        }
        private void DrawCircle(double r, double x, double y)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Black;
            AddEllipse(ellipse, r * 50, 1, x, y);
        }
        private void DrawPoint(double x, double y)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Red;
            AddEllipse(ellipse, 7, 3, x, y);
        }
        private void AddEllipse(Ellipse ellipse, double r, double h, double x, double y)
        {
            ellipse.Width = r; ellipse.Height = r;
            ellipse.StrokeThickness = h;
            Thickness thickness = new Thickness((x * 50) - 300, 0, 0, (y * 50) - 300);
            ellipse.Margin = thickness;
            Platform.Children.Add(ellipse);
        }
    }
}
