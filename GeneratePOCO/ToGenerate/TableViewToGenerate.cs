using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    class TableViewToGenerate
    {
        public List<string> tables { get; set; }
        public List<string> views { get; set; }

        public HashSet<string> TableHashSet { get; set; }
    }
}
