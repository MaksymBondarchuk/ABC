using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public List<FieldPoint> Points { get; } = new List<FieldPoint>();

        private string GetFullFilePath(string fileName)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            return directoryInfo == null ? string.Empty : Path.Combine(directoryInfo.FullName, fileName);
        }

        public void Load(string fileName)
        {
            var filePath = GetFullFilePath(fileName);

            if (!File.Exists(filePath)) return;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                var size = reader.ReadInt32();

                for (var i = 0; i < size; i++)
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

        public void Dump2File(string fileName)
        {
            var filePath = GetFullFilePath(fileName);

            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                writer.Write(Points.Count);
                foreach (var point in Points)
                {
                    writer.Write(point.Point.X);
                    writer.Write(point.Point.Y);
                    writer.Write((int)point.Color.A);
                    writer.Write((int)point.Color.R);
                    writer.Write((int)point.Color.G);
                    writer.Write((int)point.Color.B);
                    writer.Write(point.IsCentroid);
                }
            }
        }
    }
}
