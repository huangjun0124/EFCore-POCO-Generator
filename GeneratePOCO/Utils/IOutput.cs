using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO.Utils
{
    interface IOutput
    {
        void Log(string message, bool isWarn=false);
    }
}
