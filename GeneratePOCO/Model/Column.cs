using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    [DebuggerDisplay("Name={Name} ClassName={NameHumanCase} SqlPropertyType={SqlPropertyType} PropertyType={PropertyType} MaxLength={MaxLength}")]
    public class Column
    {
        /// <summary>
        /// Raw name of the column as obtained from the database
        /// </summary>
        public string Name;
        /// <summary>
        /// Name adjusted for C# output
        /// </summary>
        public string NameHumanCase;

        /// <summary>
        /// Adds 'override' to the property declaration
        /// </summary>
        public bool OverrideModifier = false; 

        public int DateTimePrecision;
        public string Default;
        public int MaxLength;
        public int Precision;
        /// <summary>
        /// 字段在 SQL 中的类型
        /// </summary>
        public string SqlPropertyType;
        /// <summary>
        /// 字段对应的 C# 类型
        /// </summary>
        public string PropertyType;
        public int Scale;
        public int Ordinal;
        public int PrimaryKeyOrdinal;
        public string ExtendedProperty;
        public string SummaryComments;
        public string UniqueIndexName;
        public bool AllowEmptyStrings = true;

        public bool IsIdentity;
        public bool IsRowGuid;
        public bool IsComputed;
        public ColumnGeneratedAlwaysType GeneratedAlwaysType;
        public bool IsNullable;
        public bool IsPrimaryKey;
        public bool IsUniqueConstraint;
        public bool IsUnique;
        public bool IsStoreGenerated;
        public bool IsRowVersion;
        public bool IsConcurrencyToken; //  Manually set via callback
        public bool IsFixedLength;
        public bool IsUnicode;
        public bool IsMaxLength;
        public bool Hidden;
        public bool IsForeignKey;

        public string Config;
        public List<string> ConfigFk = new List<string>();
        public string Entity;
        public List<PropertyAndComments> EntityFk = new List<PropertyAndComments>();

        public List<string> DataAnnotations;
        public List<Index> Indexes = new List<Index>();

        public Table ParentTable;

        public void ResetNavigationProperties()
        {
            ConfigFk = new List<string>();
            EntityFk = new List<PropertyAndComments>();
        }

        private void SetupEntity()
        {
            var comments = string.Empty;
            if (Settings.IncludeComments != CommentsStyle.None)
            {
                comments = Name;
                if (IsPrimaryKey)
                {
                    if (IsUniqueConstraint)
                        comments += " (Primary key via unique index " + UniqueIndexName + ")";
                    else
                        comments += " (Primary key)";
                }

                if (MaxLength > 0)
                    comments += string.Format(" (length: {0})", MaxLength);
            }

            var inlineComments = Settings.IncludeComments == CommentsStyle.AtEndOfField ? " // " + comments : string.Empty;

            SummaryComments = string.Empty;
            if (Settings.IncludeComments == CommentsStyle.InSummaryBlock && !string.IsNullOrEmpty(comments))
            {
                SummaryComments = comments;
            }
            if (Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock && !string.IsNullOrEmpty(ExtendedProperty))
            {
                if (string.IsNullOrEmpty(SummaryComments))
                    SummaryComments = ExtendedProperty;
                else
                    SummaryComments += ". " + ExtendedProperty;
            }

            if (Settings.IncludeExtendedPropertyComments == CommentsStyle.AtEndOfField && !string.IsNullOrEmpty(ExtendedProperty))
            {
                if (string.IsNullOrEmpty(inlineComments))
                    inlineComments = " // " + ExtendedProperty;
                else
                    inlineComments += ". " + ExtendedProperty;
            }
            var initialization = Settings.UsePropertyInitializers ? (Default == string.Empty ? "" : string.Format(" = {0};", Default)) : "";
            Entity = string.Format(
                "public {0}{1} {2} {{ get; {3}set; }}{4}{5}",
                (OverrideModifier ? "override " : ""), WrapIfNullable(PropertyType, this), NameHumanCase, Settings.UsePrivateSetterForComputedColumns && IsComputed ? "private " : string.Empty, initialization, inlineComments
            );
        }

        private string WrapIfNullable(string propType, Column col)
        {
            if (!col.IsNullable || propType=="string" || propType=="byte[]")
                return propType;
            return string.Format(Settings.NullableShortHand ? "{0}?" : "System.Nullable<{0}>", propType);
        }

        private void SetupConfig()
        {
            DataAnnotations = new List<string>();
            var sb = new StringBuilder();

            if (Settings.UseDataAnnotations)
                DataAnnotations.Add(string.Format("Column(@\"{0}\", TypeName = \"{1}\")", Name, SqlPropertyType));
            else
                sb.AppendFormat(".HasColumnName(@\"{0}\").HasColumnType(\"{1}\")", Name, SqlPropertyType);
            
            // i don't want to use index
            //if (Settings.UseDataAnnotations && Indexes.Any())
            //{
            //    foreach (var index in Indexes)
            //    {
            //        DataAnnotations.Add(string.Format("Index(@\"{0}\", {1}, IsUnique = {2}, IsClustered = {3})",
            //            index.IndexName,
            //            index.KeyOrdinal,
            //            index.IsUnique ? "true" : "false",
            //            index.IsClustered ? "true" : "false"));
            //    }
            //}

            if (IsNullable)
            {
                sb.Append(".IsOptional()");
            }
            else
            {
                //if (!IsComputed && (Settings.UseDataAnnotations || Settings.UseDataAnnotationsWithFluent))
                //{
                //    if (PropertyType.Equals("string", StringComparison.InvariantCultureIgnoreCase) && this.AllowEmptyStrings)
                //    {
                //        DataAnnotations.Add("Required(AllowEmptyStrings = true)");
                //    }
                //    else
                //    {
                //        DataAnnotations.Add("Required");
                //    }
                //}

                if (!Settings.UseDataAnnotations)
                {
                    sb.Append(".IsRequired()");
                }
            }

            if (IsFixedLength || IsRowVersion)
            {
                sb.Append(".IsFixedLength()");
                // DataAnnotations.Add("????");
            }

            if (!IsUnicode)
            {
                sb.Append(".IsUnicode(false)");
                // DataAnnotations.Add("????");
            }

            if (!IsMaxLength && MaxLength > 0)
            {

                //if (Settings.UseDataAnnotations || Settings.UseDataAnnotationsWithFluent)
                //{
                //    DataAnnotations.Add( string.Format("MaxLength({0})", MaxLength));

                //    if (Settings.IsColumnAddMaxLentghAnnotation && PropertyType.Equals("string", StringComparison.InvariantCultureIgnoreCase))
                //        DataAnnotations.Add(string.Format("StringLength({0})", MaxLength));
                //}

                if (!Settings.UseDataAnnotations)
                {
                    sb.AppendFormat(".HasMaxLength({0})", MaxLength);
                }
            }

            if (IsMaxLength)
            {
                //if (Settings.UseDataAnnotations || Settings.UseDataAnnotationsWithFluent)
                //{
                //    DataAnnotations.Add("MaxLength");
                //}

                if (!Settings.UseDataAnnotations)
                {
                    sb.Append(".IsMaxLength()");
                }
            }

            if ((Precision > 0 || Scale > 0) && PropertyType == "decimal")
            {
                sb.AppendFormat(".HasPrecision({0},{1})", Precision, Scale);
                // DataAnnotations.Add("????");
            }

            if (IsRowVersion)
            {
                if (Settings.UseDataAnnotations)
                {
                    //DataAnnotations.Add("Timestamp");
                }
                else
                    sb.Append(".IsRowVersion()");
            }

            if (IsConcurrencyToken)
            {
                sb.Append(".IsConcurrencyToken()");
                // DataAnnotations.Add("????");
            }

            var config = sb.ToString();
            if (!string.IsNullOrEmpty(config))
                Config = string.Format("Property(x => x.{0}){1};", NameHumanCase, config);

            // No need to do this in my project
            //if (IsPrimaryKey && Settings.UseDataAnnotations)
            //    DataAnnotations.Add("Key");

            //string valueFromName, valueFromType;
            //if (Settings.ColumnNameToDataAnnotation.TryGetValue(NameHumanCase.ToLowerInvariant(), out valueFromName))
            //{
            //    DataAnnotations.Add(valueFromName);
            //}
            //else if (Settings.ColumnTypeToDataAnnotation.TryGetValue(SqlPropertyType.ToLowerInvariant(), out valueFromType))
            //{
            //    DataAnnotations.Add(valueFromType);
            //}
            
        }

        public void SetupEntityAndConfig()
        {
            SetupEntity();
            SetupConfig();
        }

        public void CleanUpDefault()
        {
            if (string.IsNullOrWhiteSpace(Default))
            {
                Default = string.Empty;
                return;
            }

            // Remove outer brackets
            while (Default.First() == '(' && Default.Last() == ')' && Default.Length > 2)
            {
                Default = Default.Substring(1, Default.Length - 2);
            }

            // Remove unicode prefix
            if (IsUnicode && Default.StartsWith("N") && !Default.Equals("NULL", StringComparison.InvariantCultureIgnoreCase))
                Default = Default.Substring(1, Default.Length - 1);

            if (Default.First() == '\'' && Default.Last() == '\'' && Default.Length >= 2)
                Default = string.Format("\"{0}\"", Default.Substring(1, Default.Length - 2));

            string lower = Default.ToLower();
            string lowerPropertyType = PropertyType.ToLower();

            // Cleanup default
            switch (lowerPropertyType)
            {
                case "bool":
                    Default = (Default == "0" || lower == "\"false\"" || lower == "false") ? "false" : "true";
                    break;

                case "string":
                case "datetime":
                case "datetime2":
                case "system.datetime":
                case "timespan":
                case "system.timespan":
                case "datetimeoffset":
                case "system.datetimeoffset":
                    if (Default.First() != '"')
                        Default = string.Format("\"{0}\"", Default);
                    if (Default.Contains('\\') || Default.Contains('\r') || Default.Contains('\n'))
                        Default = "@" + Default;
                    else
                        Default = string.Format("\"{0}\"", Default.Substring(1, Default.Length - 2).Replace("\"", "\\\"")); // #281 Default values must be escaped if contain double quotes
                    break;

                case "long":
                case "short":
                case "int":
                case "double":
                case "float":
                case "decimal":
                case "byte":
                case "guid":
                case "system.guid":
                    if (Default.First() == '\"' && Default.Last() == '\"' && Default.Length > 2)
                        Default = Default.Substring(1, Default.Length - 2);
                    break;

                case "byte[]":
                case "system.data.entity.spatial.dbgeography":
                case "system.data.entity.spatial.dbgeometry":
                    Default = string.Empty;
                    break;
            }

            // Ignore defaults we cannot interpret (we would need SQL to C# compiler)
            if (lower.StartsWith("create default"))
            {
                Default = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(Default))
            {
                Default = string.Empty;
                return;
            }

            // Validate default
            switch (lowerPropertyType)
            {
                case "long":
                    long l;
                    if (!long.TryParse(Default, out l))
                        Default = string.Empty;
                    break;

                case "short":
                    short s;
                    if (!short.TryParse(Default, out s))
                        Default = string.Empty;
                    break;

                case "int":
                    int i;
                    if (!int.TryParse(Default, out i))
                        Default = string.Empty;
                    break;

                case "datetime":
                case "datetime2":
                case "system.datetime":
                    DateTime dt;
                    if (!DateTime.TryParse(Default, out dt))
                        Default = (lower.Contains("getdate()") || lower.Contains("sysdatetime")) ? "System.DateTime.Now" : (lower.Contains("getutcdate()") || lower.Contains("sysutcdatetime")) ? "System.DateTime.UtcNow" : string.Empty;
                    else
                        Default = string.Format("System.DateTime.Parse({0})", Default);
                    break;

                case "datetimeoffset":
                case "system.datetimeoffset":
                    DateTimeOffset dto;
                    if (!DateTimeOffset.TryParse(Default, out dto))
                        Default = lower.Contains("sysdatetimeoffset") ? "System.DateTimeOffset.Now" : lower.Contains("sysutcdatetime") ? "System.DateTimeOffset.UtcNow" : string.Empty;
                    else
                        Default = string.Format("System.DateTimeOffset.Parse({0})", Default);
                    break;

                case "timespan":
                case "system.timespan":
                    TimeSpan ts;
                    Default = TimeSpan.TryParse(Default, out ts) ? string.Format("System.TimeSpan.Parse({0})", Default) : string.Empty;
                    break;

                case "double":
                    double d;
                    if (!double.TryParse(Default, out d))
                        Default = string.Empty;
                    if (Default.ToLowerInvariant().EndsWith("."))
                        Default += "0";
                    break;

                case "float":
                    float f;
                    if (!float.TryParse(Default, out f))
                        Default = string.Empty;
                    if (!Default.ToLowerInvariant().EndsWith("f"))
                        Default += "f";
                    break;

                case "decimal":
                    decimal dec;
                    if (!decimal.TryParse(Default, out dec))
                        Default = string.Empty;
                    else
                        Default += "m";
                    break;

                case "byte":
                    byte b;
                    if (!byte.TryParse(Default, out b))
                        Default = string.Empty;
                    break;

                case "bool":
                    bool x;
                    if (!bool.TryParse(Default, out x))
                        Default = string.Empty;
                    break;

                case "string":
                    if (lower.Contains("newid()") || lower.Contains("newsequentialid()"))
                        Default = "System.Guid.NewGuid().ToString()";
                    if (lower.StartsWith("space("))
                        Default = "\"\"";
                    if (lower == "null")
                        Default = string.Empty;
                    break;

                case "guid":
                case "system.guid":
                    if (lower.Contains("newid()") || lower.Contains("newsequentialid()"))
                        Default = "System.Guid.NewGuid()";
                    else if (lower.Contains("null"))
                        Default = "null";
                    else
                        Default = string.Format("System.Guid.Parse(\"{0}\")", Default);
                    break;
            }
        }
    }
}
