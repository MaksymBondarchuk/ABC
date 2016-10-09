using System.Collections.Generic;

namespace ABC
{
    public class Source
    {
        public List<double> X { get; set; } = new List<double>();

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
