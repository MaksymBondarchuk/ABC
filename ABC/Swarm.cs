using System;
using System.Collections.Generic;
using System.Linq;

namespace ABC
{
    public class Swarm
    {
        public List<Source> Sources { get; } = new List<Source>();

        public Source BestSource { get; } = new Source();

        public void TryUpdateSource(Function func, Random random, int i, int j, int k)
        {
            var phi = -1 + random.NextDouble() * 2;
            var v = Sources[i].X[j] + phi * (Sources[i].X[j] - Sources[k].X[j]);

            var newSource = new Source();
            for (var x = 0; x < Sources[i].X.Count; x++)
                newSource.X.Add(x == j ? v : Sources[i].X[x]);
            newSource.F = func.F(newSource.X);

            if (newSource.F < Sources[i].F)
            {
                Sources[i] = newSource;
                Sources[i].Trials = 0;
            }
            else
                Sources[i].Trials++;
        }

        public Source GenerateSource(Function func, Random random)
        {
            var source = new Source();
            for (var d = 0; d < func.Dimensions; d++)
                source.X.Add(func.BoundLower + random.NextDouble() * (func.BoundUpper - func.BoundLower));
            source.F = func.F(source.X);
            return source;
        }

        public void UpdateBest()
        {
            var min = Sources.First();
            foreach (var source in Sources)
                if (source.F < min.F)
                    min = source;

            if (min.F < BestSource.F)
            {
                BestSource.X.Clear();
                BestSource.X.AddRange(min.X);
                BestSource.F = min.F;
            }
        }
    }
}
