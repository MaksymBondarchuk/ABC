using System.Collections.Generic;
using System.Windows;

namespace ABC
{
    public class Source
    {
        public List<Point> Centroids { get; } = new List<Point>();

        private double _f = double.MaxValue;

        public double F
        {
            get { return _f; }
            set
            {
                Fitness = value < 0 ? 1 - value : 1 / (1 + value);
                _f = value;
            }
        }

        public double Fitness { get; set; }

        public int Trials { get; set; }
    }
}
