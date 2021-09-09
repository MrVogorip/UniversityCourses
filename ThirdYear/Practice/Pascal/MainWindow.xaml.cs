using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Numerics;

namespace pascal
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static BigInteger factorial(BigInteger num)
        {
            BigInteger fact = 1;
            for (; num > 0; fact *= num--) ;
            return fact;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            main.Children.Clear();
            int n = 0;
            if (!Int32.TryParse(nBox.Text, out n))
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK);
                return;
            }
            if (n > 200)
            {
                n = 200;
            }
            for (int i = 0; i <= n; i++)
            {
                Label label = new Label();
                label.Content = "   ";
                for (int c = 0; c <= i; c++)
                {
                    BigInteger number = factorial(i) / (factorial(c) * factorial(i - c));
                    label.Content += number + "   ";
                }
                label.Background = Brushes.OrangeRed;
                label.HorizontalAlignment = HorizontalAlignment.Center;
                main.Children.Add(label);
            }
        }
    }
}
