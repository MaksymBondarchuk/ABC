using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ABC
{
    public class Swarm
    {
        public List<Source> Sources { get; } = new List<Source>();

        public Source BestSource { get; } = new Source();

        public PointSet PointSet { get; } = new PointSet();

        public int ClustersNumber { get; set; }

        public Source GenerateSource(Function func, Random random)
        {
            var source = new Source();
            for (var d = 0; d < ClustersNumber; d++)
            {
                var centroid = new Point
                {
                    X = func.BoundLower.X + random.NextDouble()*(func.BoundUpper.X - func.BoundLower.X),
                    Y = func.BoundLower.Y + random.NextDouble()*(func.BoundUpper.Y - func.BoundLower.Y)
                };
                source.Centroids.Add(centroid);
            }
            source.F = func.F(source.Centroids, PointSet.Points);
            return source;
        }

        public void TryUpdateSource(Function func, Random random, int i, int j, int k)
        {
            var phi = -1 + random.NextDouble() * 2;
            var vX = Sources[i].Centroids[j].X + phi * (Sources[i].Centroids[j].X - Sources[k].Centroids[j].X);
            var vY = Sources[i].Centroids[j].Y + phi * (Sources[i].Centroids[j].Y - Sources[k].Centroids[j].Y);

            var newSource = new Source();
            for (var x = 0; x < Sources[i].Centroids.Count; x++)
                newSource.Centroids.Add(x == j ? new Point(vX, vY) : Sources[i].Centroids[x]);
            newSource.F = func.F(newSource.Centroids, PointSet.Points);

            if (newSource.F < Sources[i].F)
            {
                Sources[i] = newSource;
                Sources[i].Trials = 0;
            }
            else
                Sources[i].Trials++;
        }

        public bool UpdateBest()
        {
            var min = Sources.First();
            foreach (var source in Sources)
                if (source.F < min.F)
                    min = source;

            if (!(min.F < BestSource.F))
                return false;

            BestSource.Centroids.Clear();
            BestSource.Centroids.AddRange(min.Centroids);
            BestSource.F = min.F;
            return true;
        }
    }
}
