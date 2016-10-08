using System;

namespace ABC
{
    public class Algorithm
    {
        #region Constants: private

        #endregion

        #region Properties: private

        private Function Func { get; set; }

        private Random Random { get; } = new Random();

        private Swarm Swarm { get; set; } = new Swarm();
        #endregion

        private void Initialize(int swarmSize, Function func)
        {
            Func = func;

            for (var s = 0; s < swarmSize; s++)
            {
                var source = new Source();
                for (var d = 0; d < Func.Dimensions; d++)
                    source.X.Add(Func.BoundLower + Random.NextDouble() * (Func.BoundUpper - Func.BoundLower));
                Swarm.Sources.Add(source);
            }
        }

        public void Run(int swarmSize, Function func)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Initialize(swarmSize, func);

            var lastImprovementOn = 0;
            for (var iter = 0; iter < func.IterationsNumber; iter++)
            {

            }

            watch.Stop();
            Console.WriteLine($"\nLast mprovement was on iteration #{lastImprovementOn}. Time elapsed: {watch.Elapsed}");
        }
    }
}
