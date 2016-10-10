using System;
using System.Collections.Generic;
using System.Windows;

namespace ABC
{
    public class Function
    {
        /// <summary>
        /// Function
        /// </summary>
        public Func<List<Point>, List<FieldPoint>, double> F { get; set; }

        /// <summary>
        /// Lower boundary
        /// </summary>
        public Point BoundLower { get; set; }

        /// <summary>
        /// Upper boundary
        /// </summary>
        public Point BoundUpper { get; set; }

        public int IterationsNumber { get; set; } = 10000;
    }
}