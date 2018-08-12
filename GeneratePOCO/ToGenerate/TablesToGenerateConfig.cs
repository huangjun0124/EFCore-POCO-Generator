﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GeneratePOCO
{
    class TablesToGenerateConfig
    {
        private static TableViewToGenerate config;
        public static HashSet<string> TableHashSet { get; set; }

        public static TableViewToGenerate TableNamesConfig
        {
            get
            {
                if (config == null)
                {
                    using (StreamReader file = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TablesToGenerate.json")))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        config = (TableViewToGenerate)serializer.Deserialize(file, typeof(TableViewToGenerate));
                        TableHashSet = new HashSet<string>();
                        foreach (var table in config.tables)
                        {
                            if (!TableHashSet.Contains(table))
                            {
                                TableHashSet.Add(table);
                            }
                        }
                    }
                }
                return config;
            }
        }

        public static void SaveConfig()
        {
            if (config != null)
            {
                var strToWrite = JsonConvert.SerializeObject(config);
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TablesToGenerate.json"), strToWrite);
            }
        }
    }
}
