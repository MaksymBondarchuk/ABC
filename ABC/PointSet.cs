using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Windows.Point;

namespace ABC
{
    public class FieldPoint
    {
        public Point Point { get; set; }

        public int IsCentroid { get; set; } //centroid mark
        public Color Color { get; set; } //color scheme
    };


    public class PointSet
    {
        public List<FieldPoint> Points { get; set; } = new List<FieldPoint>();

        public void Load(string fileName)
        {
            if (!File.Exists(fileName)) return;
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                var x = reader.ReadDouble();
                var y = reader.ReadDouble();

                var alpha = reader.ReadInt32();
                var r = reader.ReadInt32();
                var g = reader.ReadInt32();
                var b = reader.ReadInt32();
                var cnt = reader.ReadInt32();

                Points.Add(new FieldPoint
                {
                    Point = new Point(x, y),
                    Color = Color.FromArgb(alpha, r, g, b),
                    IsCentroid = cnt
                });
            }
        }
    }
}
