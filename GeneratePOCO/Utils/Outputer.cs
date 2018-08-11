using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    class Outputer
    {
        private static StreamWriter wl = new StreamWriter(@"D:\output.cs");
        private static StreamWriter log = new StreamWriter(@"D:\log.txt");

        public static void WriteLine(string message)
        {
            wl.WriteLine(message);
            wl.Flush();
        }

        public static void Log(string message)
        {
            log.WriteLine(message);
            log.Flush();
        }
    }
}
