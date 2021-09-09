using System.Windows;

namespace Network
{
    public partial class MainWindow : Window
    {
        Vector[] X = 
        {
            new Vector(0, 0),
            new Vector(0, 1),
            new Vector(1, 0),
            new Vector(1, 1)
        };
        Vector[] Y = 
        {
            new Vector(0.0), // 0 ^ 0 = 0
            new Vector(1.0), // 0 ^ 1 = 1
            new Vector(1.0), // 1 ^ 0 = 1
            new Vector(0.0) // 1 ^ 1 = 0
        };
        public MainWindow()
        {
            InitializeComponent();
        }
        private void FormUpdate(int n)
        {
            PrintInfo.Content = string.Empty;
            NetworkAlgorithm network = new NetworkAlgorithm(new int[] { 2, n, 1 });
            network.Train(X, Y, 0.5, 1e-7, 100000);
            for (int i = 0; i < 4; i++)
            {
                Vector output = network.Forward(X[i]);
                PrintInfo.Content += $"X: {X[i][0]} {X[i][1]}, Y: {Y[i][0]}, output: {output[0]}\n";
            }
        }
        private void NumberNodes_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumberNodesInfo.Content = (int)NumberNodes.Value;
        }
        private void TrainBnt_Click(object sender, RoutedEventArgs e)
        {
            FormUpdate((int)NumberNodes.Value);
        }
    }
}
