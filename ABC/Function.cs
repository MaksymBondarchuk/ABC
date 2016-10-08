using System;
using System.Collections.Generic;

namespace ABC
{
    public class Function
    {
        /// <summary>
        /// Function
        /// </summary>
        public Func<List<double>, double> F { get; set; }

        /// <summary>
        /// Lower boundary
        /// </summary>
        public double BoundLower { get; set; }

        /// <summary>
        /// Upper boundary
        /// </summary>
        public double BoundUpper { get; set; }

        public double Dimensions { get; set; }

        public int IterationsNumber { get; set; } = 10000;
    }
}