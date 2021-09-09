using System.Collections.Generic;

namespace DelaunayVoronoi
{
    public class Point
    {
        private static int _counter;

        private readonly int _instanceId = _counter++;

        public double X { get; }
        public double Y { get; }
        public HashSet<Triangle> AdjacentTriangles { get; } = new HashSet<Triangle>();

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"{nameof(Point)} {_instanceId} {X:0.##}@{Y:0.##}";
        }
    }
}