using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ABC
{
    internal static class Program
    {
        private static void Main()
        {
            var sphere = new Function
            {
                SSE = (centroids, points) =>
                {
                    return points.Sum(point =>
                        centroids.Select(centroid =>
                        (point.Point.X - centroid.X) * (point.Point.X - centroid.X) +
                        (point.Point.Y - centroid.Y) * (point.Point.Y - centroid.Y)).Min());
                },
                SM = (centroids, points) =>
                {
                    var centroidsPointsList = new List<List<FieldPoint>>();
                    for (var i = 0; i < centroids.Count; i++)
                        centroidsPointsList.Add(new List<FieldPoint>());
                    foreach (var point in points)
                    {
                        var minSquareDistance = double.MaxValue;
                        var minSquareDistanceTo = 0;
                        for (var i = 0; i < centroids.Count; i++)
                        {
                            var centroid = centroids[i];
                            var squareDistance = point.SquareDistanceTo(centroid);
                            if (squareDistance < minSquareDistance)
                            {
                                minSquareDistance = squareDistance;
                                minSquareDistanceTo = i;
                            }
                        }
                        centroidsPointsList[minSquareDistanceTo].Add(point);
                    }

                    var sm = .0;
                    foreach (var point in points)
                    {
                        var a = .0;
                        var b = double.MaxValue;
                        foreach (var centroidPointsList in centroidsPointsList)
                        {
                            if (centroidPointsList.Contains(point))
                                a = centroidPointsList.Sum(neighborPoint =>
                                            Math.Sqrt(point.SquareDistanceTo(neighborPoint.Point)))
                                            / (centroidPointsList.Count - 1);
                            else
                            {
                                var localB = centroidPointsList.Sum(foreignPoint =>
                                    Math.Sqrt(point.SquareDistanceTo(foreignPoint.Point)));
                                if (localB < b)
                                    b = localB;
                            }
                        }
                        sm += (b - a) / Math.Max(a, b);
                    }
                    return sm / points.Count;
                },
                XB = (centroids, points) =>
                {
                    var msdcc = (from centroid in centroids
                                 from otherCentroid in centroids
                                 where centroid != otherCentroid
                                 select Math.Pow(centroid.X - otherCentroid.X, 2) +
                                        Math.Pow(centroid.Y - otherCentroid.Y, 2))
                                        .Min();
                    var msdoc = points.Sum(point =>
                        centroids.Select(centroid =>
                        point.SquareDistanceTo(centroid)).Min());
                    return msdoc / (points.Count * msdcc);
                },
                BoundLower = new Point(0, 0),
                BoundUpper = new Point(1100, 600),
            };

            var algorithm = new Algorithm();
            algorithm.Run(swarmSize: 50, func: sphere, fileName: "Files\\Threerings_3.dat");
        }
    }
}
