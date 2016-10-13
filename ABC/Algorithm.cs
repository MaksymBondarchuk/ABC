using System;
using System.Collections.Generic;

namespace ABC
{
    public class Algorithm
    {
        #region Properties: private
        private Function Func { get; set; }

        private Random Random { get; } = new Random();

        private Swarm Swarm { get; } = new Swarm();
        #endregion

        private void Initialize(int swarmSize, Function func, string fileName)
        {
            Func = func;

            Swarm.ClustersNumber = Swarm.PointSet.Load(fileName);

            for (var s = 0; s < swarmSize; s++)
                Swarm.Sources.Add(Swarm.GenerateSource(Func, Random));
        }

        public double Run(int swarmSize, Function func, string fileName)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Initialize(swarmSize, func, fileName);

            var lastImprovementOn = 0;
            for (var iter = 0; iter < func.IterationsNumber; iter++)
            {
                // Employed bees phase
                for (var i = 0; i < Swarm.Sources.Count; i++)
                {
                    var j = Random.Next(Swarm.ClustersNumber);
                    var k = Random.Next(swarmSize);
                    Swarm.TryUpdateSource(Func, Random, i, j, k);
                }

                // Onlooker bees phase
                var probabilities = new List<double>();
                var sum = .0;
                foreach (var source in Swarm.Sources)
                {
                    sum += source.Fitness;
                    probabilities.Add(sum);
                }

                var p = Random.NextDouble() * sum;
                for (var i = 0; i < Swarm.Sources.Count; i++)
                    if (p <= probabilities[i])
                    {
                        var j = Random.Next(Swarm.ClustersNumber);
                        var k = Random.Next(swarmSize);
                        Swarm.TryUpdateSource(Func, Random, i, j, k);
                        break;
                    }

                if (Swarm.UpdateBest())
                    lastImprovementOn = iter;

                // Scout bee phase
                for (var i = 0; i < Swarm.Sources.Count; i++)
                    if (Swarm.Sources[i].Trials == 100)
                        Swarm.Sources[i] = Swarm.GenerateSource(Func, Random);

                Console.WriteLine($"#{iter,-4} Best source = {Swarm.BestSource.F,-7:0.00000}");
                                  //+
                                  //$" SM = {Func.SM(Swarm.BestSource.Centroids, Swarm.PointSet.Points)}" +
                                  //$" XB = {Func.XB(Swarm.BestSource.Centroids, Swarm.PointSet.Points)}");
            }

            watch.Stop();
            Console.WriteLine($"\nLast mprovement was on iteration #{lastImprovementOn}. " +
                              $"\nTime elapsed: {watch.Elapsed}" +
                              $"\nSM = {Func.SM(Swarm.BestSource.Centroids, Swarm.PointSet.Points)}" +
                              $"\nSM = {Func.XB(Swarm.BestSource.Centroids, Swarm.PointSet.Points)}");

            Swarm.PointSet.UpdatePoints(Swarm.BestSource.Centroids);
            foreach (var centroid in Swarm.BestSource.Centroids)
                Swarm.PointSet.Points.Add(new FieldPoint {IsCentroid = 1, Point = centroid});

            var newFileName = $"{fileName.Substring(0, fileName.Length - 4)} (1).dat";
            Swarm.PointSet.Dump2File(newFileName);

            Console.WriteLine($"Clustered data in \"{newFileName}\"");

            return Swarm.BestSource.F;
        }
    }
}
