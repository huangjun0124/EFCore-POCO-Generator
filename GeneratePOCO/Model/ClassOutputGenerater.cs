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
        private IOutput outPuter;
        public ClassOutputGenerater(IOutput outputer)
        {
            outPuter = outputer;
        }

        public void WriteToFiles()
        {
            Task.Run(async () => { await WriteToFilesAsync(); }).GetAwaiter().GetResult();
        }

        private async Task WriteToFilesAsync()
        {
            await GenerateDbContext();
            await GeneratePOCOClass();
            TablesToGenerateConfig.SaveConfig();
        }

        private async Task GenerateDbContext()
        {
            outPuter.Log("Begin to generate Dbcontext class...");
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
                if (TablesToGenerateConfig.TableHashSet.Contains(table.Name) && table.HasPrimaryKey)
                {
                    strDbSet.AppendLine(string.Format("\t\tpublic DbSet<{0}> {1} {{ get; set; }}", table.NameHumanCaseWithSuffix(),
                        Inflector.MakePlural(table.NameHumanCase)));
                    var pkStr = table.PrimaryKeyNameHumanCase();
                    if(!string.IsNullOrEmpty(pkStr))
                    {
                        strModelBind.AppendLine(string.Format("\t\t\tmodelBuilder.Entity<{0}>().HasKey({1});",
                        table.NameHumanCaseWithSuffix(), pkStr));
                    }
                }
            }
           
            strContent = string.Format(strContent, strDbSet.ToString(), strModelBind.ToString());
            File.WriteAllText(outFilePath, strContent);
            outPuter.Log("Dbcontext class generate success!");
        }

        private async Task GeneratePOCOClass()
        {
            var templatePath = PathHelper.GetActualPath(Settings.POCOClassTemplateFile);
            string outFileDir = Path.GetDirectoryName(templatePath);
            string strClassTemplate;
            using (var reader = File.OpenText(templatePath))
            {
                strClassTemplate = await reader.ReadToEndAsync();
            }
            foreach (var table in Settings.Tables)
            {
                if (TablesToGenerateConfig.TableHashSet.Contains(table.Name))
                {
                    var clsName = table.NameHumanCaseWithSuffix();
                    outPuter.Log($"Begin to generate table class 【{clsName}】 ");
                    var outFilePath = $"{outFileDir}\\{clsName}.cs";
                    var classHeader = GetPOCOClassHeader(table);
                    StringBuilder colBody = new StringBuilder();
                    foreach (Column col in table.Columns.OrderBy(x => x.Ordinal))
                    {
                        colBody.AppendLine(GetColumnBody(col));
                    }

                    var strContent = string.Format(strClassTemplate, classHeader, clsName, colBody.ToString());
                    byte[] bytes = Encoding.UTF8.GetBytes(strContent);
                    if (File.Exists(outFilePath))
                    {
                        File.Delete(outFilePath);
                    }
                    using (var stream = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        await stream.WriteAsync(bytes, 0, bytes.Length);
                    };
                    outPuter.Log($"Table class 【{clsName}】 generate done...");
                }
            }
        }

        private string GetPOCOClassHeader(Table table)
        {
            StringBuilder sb = new StringBuilder();
            if (!table.HasPrimaryKey)
            {
                sb.AppendLine( $"\t// The table '<{table.Name}>' is not usable by entity framework because it")
                        .AppendLine("\t// does not have a primary key. It is listed here for completeness.");
            }
            if (Settings.IncludeExtendedPropertyComments != CommentsStyle.None && !string.IsNullOrEmpty(table.ExtendedProperty))
            {
                var lines = table.ExtendedProperty
                    .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                sb.AppendLine("\t///<summary>");
                foreach (var line in lines.Select(x => x.Replace("///", string.Empty).Trim()))
                {
                    sb.AppendLine($"\t/// {System.Security.SecurityElement.Escape(line)}");
                }
                sb.AppendLine("\t///</summary>");
            }
            if (Settings.UseDataAnnotations)
            {
                int i = 0;
                for (; i < table.DataAnnotations.Count-1; i++)
                {
                    sb.AppendLine("\t[" + table.DataAnnotations[i] + "]");
                }

                if (i < table.DataAnnotations.Count)
                {
                    sb.Append("\t[" + table.DataAnnotations[i] + "]");
                }
            }
            return sb.ToString();
        }

        private string GetColumnBody(Column col)
        {
            StringBuilder sb = new StringBuilder();
            if ((Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock ||
                 Settings.IncludeComments == CommentsStyle.InSummaryBlock) &&
                !string.IsNullOrEmpty(col.SummaryComments))
            {
                sb.AppendLine("\t\t///<summary>");
                sb.AppendLine($"\t\t/// {System.Security.SecurityElement.Escape(col.SummaryComments)}");
                sb.AppendLine("\t\t///</summary>");
            }
            if (Settings.UseDataAnnotations || Settings.UseDataAnnotationsWithFluent)
            {
                foreach (var dataAnnotation in col.DataAnnotations)
                {
                    sb.AppendLine("\t\t[" + dataAnnotation + "]");
                }
            }
            sb.Append($"\t\t{col.Entity}");

            if (col.Ordinal < col.ParentTable.Columns.Count)
            {
                sb.AppendLine(string.Empty);
            }  

            return sb.ToString();
        }
    }
}
