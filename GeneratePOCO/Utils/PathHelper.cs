using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO.Utils
{
    public class PathHelper
    {
        public static string GetActualPath(string path)
        {
            return Path.GetFullPath(path);
        }

        public static string GetFileName(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public static FileInfo GetFileInfo(string filePath)
        {
            FileInfo fileInfo = new FileInfo(GetActualPath(filePath));
            return fileInfo;
        }
    }
}
