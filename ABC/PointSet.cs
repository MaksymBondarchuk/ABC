using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Point = System.Windows.Point;

namespace ABC
{
    public class PointSet
    {
        public List<FieldPoint> Points { get; } = new List<FieldPoint>();

        private static string GetFullFilePath(string fileName)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            return directoryInfo == null ? string.Empty : Path.Combine(directoryInfo.FullName, fileName);
        }

        public int Load(string fileName)
        {
            var filePath = GetFullFilePath(fileName);

            if (!File.Exists(filePath)) return -1;
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

            var clustersNumberMatch = Regex.Match(fileName, @"_([\d]+)\.dat$");
            return Convert.ToInt32(clustersNumberMatch.Groups[1].Value);
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

        public void UpdatePoints(List<Point> centroids)
        {
            var random = new Random();
            var colors = centroids.Select(centroid => 
                Color.FromArgb(255, random.Next(255), random.Next(255), random.Next(255))).ToList();

            foreach (var point in Points)
            {
                var minSquareDistance = double.MaxValue;
                var minSquareDistanceTo = 0;

                for (var i = 0; i < centroids.Count; i++)
                {
                    var centroid = centroids[i];
                    var squareDistance = (point.Point.X - centroid.X)*(point.Point.X - centroid.X) +
                                         (point.Point.Y - centroid.Y)*(point.Point.Y - centroid.Y);
                    if (squareDistance < minSquareDistance)
                    {
                        minSquareDistance = squareDistance;
                        minSquareDistanceTo = i;
                    }
                }

                point.Color = colors[minSquareDistanceTo];
            }
        }
    }
}
