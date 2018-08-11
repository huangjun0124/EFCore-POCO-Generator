using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    class Utils
    {
        public static void InitConnectionString()
        {
            if (!string.IsNullOrEmpty(Settings.ConnectionString))
                return;

            Settings.ConnectionString = GetConnectionString(ref Settings.ConnectionStringName, out Settings.ProviderName, out Settings.ConfigFilePath);
        }

        private static string GetConnectionString(ref string connectionStringName, out string providerName, out string configFilePath)
        {
            providerName = null;
            configFilePath = AppDomain.CurrentDomain.BaseDirectory;
            var section = ConfigurationManager.ConnectionStrings[connectionStringName];
            providerName = section.ProviderName;
            return section.ConnectionString;
        }


        public static readonly Regex RxCleanUp = new Regex(@"[^\w\d\s_-]", RegexOptions.Compiled);
    }
}
