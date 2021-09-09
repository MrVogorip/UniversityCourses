using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LocusRegionalSearchAlgorithm
{
    public class QMyPoint
    {
        public QMyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X;
        public int Y;
        public int Dominating;
        public bool LeftShell;
        public int index;
        public double Angle;
    }

    public struct CoordinateWithIndex
    {
        public int Coord;
        public int Ind;
    };


    public partial class MainWindow : Window
    {
        QMyPoint LeftTopPoint;
        QMyPoint RightTopPoint;
        QMyPoint LeftBotomPoint;
        QMyPoint RightBotomPoint;
        int X1Y1 = 0, L1 = 0, L2 = 0;
        int FirstX;
        int SecondX;
        int FirstY;
        int SecondY;
        List<QMyPoint> PointArray = new List<QMyPoint>();
        List<QMyPoint> DominatingPointArray = new List<QMyPoint>();
        delegate bool O(QMyPoint p);
        delegate bool E(int a, int b);
        delegate bool I(bool i, bool j);


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
                            PointArray.Add(new QMyPoint(
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
                    using (StreamReader sr = new StreamReader(path))
                    {
                        var words = sr.ReadToEnd().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        X1Y1 = int.Parse(words[0]);
                        L1 = int.Parse(words[1]);
                        L2 = int.Parse(words[2]);
                        FirstX = X1Y1;
                        FirstY = X1Y1;
                        SecondX = L1;
                        SecondY = L2;
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Info", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs ex)
        {
            List<CoordinateWithIndex> Xcoordinates = new List<CoordinateWithIndex>();
            List<CoordinateWithIndex> Ycoordinates = new List<CoordinateWithIndex>();

            for (int i = 0; i < PointArray.Count; i++)
            {
                CoordinateWithIndex needCoord2 = new CoordinateWithIndex();

                needCoord2.Coord = PointArray[i].X;
                needCoord2.Ind = i;
                Xcoordinates.Add(needCoord2);

                needCoord2.Coord = PointArray[i].Y;
                needCoord2.Ind = i;
                Ycoordinates.Add(needCoord2);
            }

            CoordinateWithIndex needCoord = new CoordinateWithIndex();
            needCoord.Coord = int.MaxValue;
            needCoord.Ind = PointArray.Count;
            Xcoordinates.Add(needCoord);
            needCoord.Coord = int.MaxValue;
            Ycoordinates.Add(needCoord);

            Merge(ref Xcoordinates, 0, Xcoordinates.Count - 1);
            Merge(ref Ycoordinates, 0, Ycoordinates.Count - 1);

            for (int i = 0; i < Xcoordinates.Count; i++)
            {
                for (int j = 0; j < Ycoordinates.Count; j++)
                {

                    DominatingPointArray.Add(new QMyPoint(Xcoordinates[i].Coord, Ycoordinates[j].Coord));

                    int Dominating = 0;
                    if (!(i == 0 || j == 0))
                    {
                        {
                            if (Xcoordinates[i].Coord > PointArray[Ycoordinates[j - 1].Ind].X)
                                Dominating = DominatingPointArray[DominatingPointArray.Count - 2].Dominating + 1;
                            else
                                Dominating = DominatingPointArray[DominatingPointArray.Count - 2].Dominating;
                        }
                    }

                    var last = DominatingPointArray.Last();
                    last.Dominating = Dominating;
                }
            }


            if (FirstX > SecondX && FirstY > SecondY)
            {
                LeftTopPoint = new QMyPoint(SecondX, FirstY);
                RightTopPoint = new QMyPoint(FirstX, FirstY);
                LeftBotomPoint = new QMyPoint(SecondX, SecondY);
                RightBotomPoint = new QMyPoint(FirstX, SecondY);
            }

            if (FirstX <= SecondX && FirstY <= SecondY)
            {
                LeftTopPoint = new QMyPoint(FirstX, SecondY);
                RightTopPoint = new QMyPoint(SecondX, SecondY);
                LeftBotomPoint = new QMyPoint(FirstX, FirstY);
                RightBotomPoint = new QMyPoint(SecondX, FirstY);
            }

            if (FirstX > SecondX && FirstY < SecondY)
            {
                LeftTopPoint = new QMyPoint(SecondX, SecondY);
                RightTopPoint = new QMyPoint(FirstX, SecondY);
                LeftBotomPoint = new QMyPoint(SecondX, FirstY);
                RightBotomPoint = new QMyPoint(FirstX, FirstY);
            }

            if (FirstX <= SecondX && FirstY >= SecondY)
            {
                LeftTopPoint = new QMyPoint(FirstX, FirstY);
                RightTopPoint = new QMyPoint(SecondX, FirstY);
                LeftBotomPoint = new QMyPoint(FirstX, SecondY);
                RightBotomPoint = new QMyPoint(SecondX, SecondY);
            }


            for (int i = 0; i < 1000; i++)
            {
                FastSearch(ref RightTopPoint);
                FastSearch(ref LeftTopPoint);
                FastSearch(ref RightBotomPoint);
                FastSearch(ref LeftBotomPoint);
            }

            int pointCount = RightTopPoint.Dominating - LeftTopPoint.Dominating - RightBotomPoint.Dominating + LeftBotomPoint.Dominating;

            MessageBox.Show((pointCount - 4).ToString(), "");
            E e = (g, v) => g < v;
            I I = (s, f) => s && f;
            O o_1 = (l) => I(I(!e((int)l.Y, X1Y1), e((int)l.Y, L1)), e((int)l.Y, L2));
            O o_2 = (l) => I(e((int)l.X, L2), !e((int)l.X, X1Y1));
            Line L = new Line();
            L.Stroke = Brushes.Black;
            L.X1 = X1Y1;
            L.Y1 = X1Y1;
            L.X2 = X1Y1;
            L.Y2 = L1;

            Line R = new Line();
            R.Stroke = Brushes.Black;
            R.X1 = X1Y1;
            R.X2 = L2;
            R.Y1 = L1;
            R.Y2 = L1;

            Line lT = new Line();
            lT.Stroke = Brushes.Black;
            lT.X1 = L2;
            lT.X2 = L2;
            lT.Y1 = X1Y1;
            lT.Y2 = L1;

            Line b = new Line();
            b.Stroke = Brushes.Black;
            b.X1 = X1Y1;
            b.X2 = L2;
            b.Y2 = X1Y1;
            b.Y1 = X1Y1;

            Panel.Children.Add(L);
            Panel.Children.Add(lT);
            Panel.Children.Add(R);
            Panel.Children.Add(b);
            int cnt = 0;
            for (int i = 0; i < PointArray.Count; i++)
            {
                Line line = new Line();
                line.StrokeThickness = 8;
                line.X1 = PointArray[i].X;
                line.X2 = PointArray[i].X + 4;
                line.Y2 = PointArray[i].Y + 4;
                line.Y1 = PointArray[i].Y;
                if (o_1(PointArray[i]) && o_2(PointArray[i]))
                {
                    cnt++;
                    line.Stroke = Brushes.Red;
                }
                else line.Stroke = Brushes.Black;
                Panel.Children.Add(line);
            }
        }

        private void Merge(ref List<CoordinateWithIndex> coordArray, int first, int last)
        {
            if (last <= first)
                return;
            int mid = first + (last - first) / 2;

            Merge(ref coordArray, first, mid);
            Merge(ref coordArray, mid + 1, last);

            List<CoordinateWithIndex> bufferList = coordArray.ToArray().ToList();

            for (int k = first; k <= last; k++)
                bufferList[k] = coordArray[k];

            int i = first, j = mid + 1;

            for (int k = first; k <= last; k++)
            {

                if (i > mid)
                {
                    coordArray[k] = bufferList[j];
                    j++;
                }
                else if (j > last)
                {
                    coordArray[k] = bufferList[i];
                    i++;
                }
                else if ((bufferList[j].Coord < bufferList[i].Coord))
                {
                    coordArray[k] = bufferList[j];
                    j++;
                }
                else
                {
                    coordArray[k] = bufferList[i];
                    i++;
                }
            }
        }

        private void FastSearch(ref QMyPoint point)
        {
            int deffX = PointArray.Count + 1;
            int leftIndex = 0;

            int rightIndex = PointArray.Count - 2;

            while (true)
            {

                int midIndex = ((rightIndex - leftIndex) / 2 + leftIndex);

                if (point.X >= DominatingPointArray[(midIndex + 1) * deffX].X)//>=
                {
                    leftIndex = midIndex + 1;
                }
                else if (point.X < DominatingPointArray[midIndex * deffX].X)//>
                {
                    if (midIndex == 0)
                        break;

                    rightIndex = midIndex;
                }
                else
                if (point.X >= DominatingPointArray[midIndex * deffX].X
                    && point.X < DominatingPointArray[(midIndex + 1) * deffX].X)
                {
                    leftIndex = midIndex * deffX;
                    rightIndex = leftIndex + deffX - 2;

                    int leftLimitIndex = midIndex * deffX;

                    while (true)
                    {
                        midIndex = ((rightIndex - leftIndex) / 2 + leftIndex);
                        if (point.Y >= DominatingPointArray[midIndex + 1].Y)//>=
                        {
                            leftIndex = midIndex + 1;
                        }
                        else if (point.Y < DominatingPointArray[midIndex].Y)//<
                        {
                            if (midIndex == leftLimitIndex)
                                break;

                            rightIndex = midIndex;
                        }
                        else if (point.Y >= DominatingPointArray[midIndex].Y && point.Y < DominatingPointArray[midIndex + 1].Y)//>= <
                        {
                            point.Dominating = DominatingPointArray[midIndex + 1 + deffX].Dominating;
                            break;
                        }
                        else
                            break;
                    }
                    break;
                }
                else
                    break;
            }
        }

    }
}
