using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    class Resource
    {
        private static Dictionary<string, string> resDictionary = new Dictionary<string, string>()
        {
            { "TableName","表名"},
            { "COL_TYPE","类型"},
            { "true","是"},
            { "false","否"},
            { "IsView","是否视图"}
        };

        public static string GetString(string key)
        {
            if (resDictionary.ContainsKey(key))
                return resDictionary[key];
            return key;
        }
    }
}
