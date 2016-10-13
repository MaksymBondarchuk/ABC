using System.Drawing;
using Point = System.Windows.Point;

namespace ABC
{
    public class FieldPoint
    {
        public Point Point { get; set; }

        public int IsCentroid { get; set; }

        public Color Color { get; set; }

        public double SquareDistanceTo(Point point)
        {
            var x = Point.X - point.X;
            var y = Point.Y - point.Y;
            return x * x + y * y;
        }
    };
}