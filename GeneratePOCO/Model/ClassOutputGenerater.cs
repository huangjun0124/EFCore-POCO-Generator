using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratePOCO.Utils;

namespace GeneratePOCO
{
    class ClassOutputGenerater
    {
        public string Dbcontext { get; set; }
        public Dictionary<string,string> ClassDictionary { get; } = new Dictionary<string, string>();

        public void WriteToFiles()
        {
            Task.Run(async () => { await WriteToFilesAsync(); }).GetAwaiter().GetResult();
        }

        private async Task WriteToFilesAsync()
        {
            await GenerateDbContext();
        }

        private async Task GenerateDbContext()
        {
            var templatePath = PathHelper.GetActualPath(Settings.DbContextTemplateFile);
            string outFilePath = $"{Path.GetDirectoryName(templatePath)}\\{Path.GetFileNameWithoutExtension(templatePath)}.cs";
            string strContent;
            using (var reader = File.OpenText(templatePath))
            {
                strContent = await reader.ReadToEndAsync();
            }
            StringBuilder strDbSet = new StringBuilder();
            StringBuilder strModelBind = new StringBuilder();
            foreach (var table in Settings.Tables)
            {
                if (TablesToGenerateConfig.TableNamesConfig.TableHashSet.Contains(table.Name))
                {
                    strDbSet.AppendLine(string.Format("public DbSet<{0}> {1}s { get; set; }", table.NameHumanCaseWithSuffix(),
                        Inflector.MakePlural(table.NameHumanCase)));
                    strModelBind.AppendLine(string.Format("modelBuilder.Entity<{0}>().HasKey(x => x.{1});",
                        table.NameHumanCaseWithSuffix(), table.PrimaryKeyNameHumanCase()));
                }
            }
           
            strContent = string.Format(strContent, strDbSet.ToString(), strModelBind.ToString());
            File.WriteAllText(outFilePath, strContent);

            byte[] bytes = Encoding.UTF8.GetBytes(strContent);
            using (var stream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await stream.WriteAsync(bytes, 0, bytes.Length);
            };
        }
    }
}
