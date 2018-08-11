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
            configFilePath = string.Empty;
            var result = "";
            var paths = new List<string>() {AppDomain.CurrentDomain.BaseDirectory};

            // Find a configuration file with the named connection string
            foreach (var path in paths)
            {
                var configFile = new ExeConfigurationFileMap { ExeConfigFilename = path };
                var appConfig = System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                var connSection = appConfig.ConnectionStrings;

                if (string.IsNullOrEmpty(connectionStringName))
                    continue;

                // Get the named connection string
                try
                {
                    result = connSection.ConnectionStrings[connectionStringName].ConnectionString;
                    providerName = connSection.ConnectionStrings[connectionStringName].ProviderName;
                    configFilePath = path;
                    return result;  // found it
                }
                catch
                {
                    result = "There is no connection string name called '" + connectionStringName + "'";
                }
            }
            return result;
        }


        public static readonly Regex RxCleanUp = new Regex(@"[^\w\d\s_-]", RegexOptions.Compiled);
    }
}
