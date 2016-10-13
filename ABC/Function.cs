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
        // ReSharper disable once InconsistentNaming
        public Func<List<Point>, List<FieldPoint>, double> SSE { get; set; }

        public Func<List<Point>, List<FieldPoint>, double> SM { get; set; }

        public Func<List<Point>, List<FieldPoint>, double> XB { get; set; }

        /// <summary>
        /// Lower boundary
        /// </summary>
        public Point BoundLower { get; set; }

        /// <summary>
        /// Upper boundary
        /// </summary>
        public Point BoundUpper { get; set; }

        public int IterationsNumber { get; set; } = 2500;
    }
}