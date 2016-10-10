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
                F = (centroids, points) =>
                {
                    return points.Sum(point => 
                        centroids.Select(centroid => 
                        (point.Point.X - centroid.X)*(point.Point.X - centroid.X) + 
                        (point.Point.Y - centroid.Y)*(point.Point.Y - centroid.Y)).Min());
                },
                BoundLower = new Point(0, 0),
                BoundUpper = new Point(1100, 600)
            };

            var algorithm = new Algorithm();
            algorithm.Run(swarmSize: 50, func: sphere);

            //var ps = new PointSet();
            //ps.Load("Files\\Rings_5.dat");
            //ps.Dump2File("Files\\Rings_5 (1).dat");
        }
    }
}
