using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC
{
    class Program
    {
        static void Main(string[] args)
        {
            var sphere = new Function
            {
                F = x => { return x.Sum(t => t * t); },
                BoundLower = -100,
                BoundUpper = 100,
                Dimensions = 50
            };

            var algorithm = new Algorithm();

            algorithm.Run(swarmSize: 50, func: sphere);
        }
    }
}
