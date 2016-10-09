using System.Linq;

namespace ABC
{
    internal static class Program
    {
        private static void Main()
        {
            var sphere = new Function
            {
                F = x => { return x.Sum(t => t * t); },
                BoundLower = -100,
                BoundUpper = 100,
                Dimensions = 50
            };

            var algorithm = new Algorithm();
            //algorithm.Run(swarmSize: 50, func: sphere);

            var ps = new PointSet();
        }
    }
}
