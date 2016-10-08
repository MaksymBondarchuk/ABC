using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC
{
    class Swarm
    {
        public List<Source> Sources { get; set; } = new List<Source>();

        public Source BestSource { get; set; }
    }
}
