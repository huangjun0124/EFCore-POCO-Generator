using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GeneratePOCO.Utils;

namespace GeneratePOCO
{
    class Processer
    {
        public static IOutput Outputer;

        public void LoadAllTablesToSetting()
        {
            // Read schema
            var factory = GetDbProviderFactory();
            Settings.Tables = LoadTables(factory);
            Settings.TableNameHashSet = new HashSet<string>();
            foreach (var table in Settings.Tables)
            {
                if (!Settings.TableNameHashSet.Contains(table.Name))
                {
                    Settings.TableNameHashSet.Add(table.Name);
                }
            }
        }

        public void LoadAllProcedures()
        {
            // Read schema
            var factory = GetDbProviderFactory();
            Settings.StoredProcs = LoadStoredProcs(factory);
        }

        public static void ArgumentNotNull<T>(T arg, string name) where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static bool IsNullable(Column col)
        {
            return col.IsNullable && !CommonParams.NotNullable.Contains(col.PropertyType.ToLower());
        }

        #region output message
        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(CultureInfo.CurrentCulture, format, args));
        }

        public static void WriteLine(string message)
        {
            Outputer.Log(message);
        }

        public void Warning(string message)
        {
            Outputer.Log(string.Format(CultureInfo.CurrentCulture, "Warning: {0}", message), true);
        }
        public void Error(string message)
        {
            Outputer.Log(string.Format(CultureInfo.CurrentCulture, "Warning: {0}", message), true);
        }

        public void PrintError(String message, Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            while (ex != null)
            {
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.GetType().FullName);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine();
                ex = ex.InnerException;
            }
            String report = sb.ToString();

            Warning(message + " " + report);
        }
        #endregion

        private DbProviderFactory GetDbProviderFactory()
        {
            if (!string.IsNullOrEmpty(Settings.ProviderName))
            {
                try
                {
                    return DbProviderFactories.GetFactory(Settings.ProviderName);
                }
                catch (Exception x)
                {
                    PrintError("Failed to load provider \"" + Settings.ProviderName + "\".", x);
                }
            }
            else
            {
                Warning("Failed to find providerName in the connection string");
                WriteLine("");
                WriteLine("// ------------------------------------------------------------------------------------------------");
                WriteLine("//  Failed to find providerName in the connection string");
                WriteLine("// ------------------------------------------------------------------------------------------------");
                WriteLine("");
            }
            return null;
        }

        private Tables LoadTables(DbProviderFactory factory)
        {
            if (factory == null || !(Settings.ElementsToGenerate.HasFlag(Elements.Poco) ||
                                    Settings.ElementsToGenerate.HasFlag(Elements.Context) ||
                                    Settings.ElementsToGenerate.HasFlag(Elements.UnitOfWork) ||
                                    Settings.ElementsToGenerate.HasFlag(Elements.PocoConfiguration)))
                return new Tables();

            try
            {
                using (var conn = factory.CreateConnection())
                {
                    if (conn == null)
                        return new Tables();

                    conn.ConnectionString = Settings.ConnectionString;
                    conn.Open();

                    var reader = new SqlServerSchemaReader(conn, factory);
                    var tables = reader.ReadSchema();
                    var fkList = reader.ReadForeignKeys();
                    reader.IdentifyForeignKeys(fkList, tables);

                    foreach (var t in tables)
                    {
                        if (Settings.UseDataAnnotations || Settings.UseDataAnnotationsWithFluent)
                            t.SetupDataAnnotations();
                        t.Suffix = Settings.TableSuffix;
                    }

                    Settings.AddForeignKeys?.Invoke(fkList, tables);

                    // Work out if there are any foreign key relationship naming clashes
                    reader.ProcessForeignKeys(fkList, tables, true);
                    
                    tables.IdentifyMappingTables(fkList, Settings.UseMappingTables);

                    // Now we know our foreign key relationships and have worked out if there are any name clashes,
                    // re-map again with intelligently named relationships.
                    tables.ResetNavigationProperties();

                    reader.ProcessForeignKeys(fkList, tables, false);
                    
                    tables.IdentifyMappingTables(fkList, !Settings.UseMappingTables);

                    conn.Close();
                    return tables;
                }
                
            }
            catch (Exception x)
            {
                PrintError("Failed to read database schema in LoadTables().", x);
                return new Tables();
            }
        }

        /// <summary>AddRelationship overload for single-column foreign-keys.</summary>
        public static void AddRelationship(List<ForeignKey> fkList, Tables tablesAndViews, String name, String pkSchema, String pkTable, String pkColumn, String fkSchema, String fkTable, String fkColumn)
        {
            AddRelationship(fkList, tablesAndViews, name, pkSchema, pkTable, new String[] { pkColumn }, fkSchema, fkTable, new String[] { fkColumn });
        }

        public static void AddRelationship(List<ForeignKey> fkList, Tables tablesAndViews, String relationshipName, String pkSchema, String pkTableName, String[] pkColumns, String fkSchema, String fkTableName, String[] fkColumns)
        {
            // Argument validation:
            if (fkList == null) throw new ArgumentNullException(nameof(fkList));
            if (tablesAndViews == null) throw new ArgumentNullException(nameof(tablesAndViews));
            if (String.IsNullOrEmpty(relationshipName)) throw new ArgumentNullException(nameof(relationshipName));
            if (String.IsNullOrEmpty(pkSchema)) throw new ArgumentNullException(nameof(pkSchema));
            if (String.IsNullOrEmpty(pkTableName)) throw new ArgumentNullException(nameof(pkTableName));
            if (pkColumns == null) throw new ArgumentNullException(nameof(pkColumns));
            if (pkColumns.Length == 0 || pkColumns.Any(s => String.IsNullOrEmpty(s))) throw new ArgumentException(nameof(pkColumns));
            if (String.IsNullOrEmpty(fkSchema)) throw new ArgumentNullException(nameof(fkSchema));
            if (String.IsNullOrEmpty(fkTableName)) throw new ArgumentNullException(nameof(fkTableName));
            if (fkColumns == null) throw new ArgumentNullException(nameof(fkColumns));
            if (fkColumns.Length != pkColumns.Length || fkColumns.Any(s => String.IsNullOrEmpty(s))) throw new ArgumentException(nameof(fkColumns));

            //////////////////

            Table pkTable = tablesAndViews.GetTable(pkTableName, pkSchema);
            Table fkTable = tablesAndViews.GetTable(fkTableName, fkSchema);

            for (int i = 0; i < pkColumns.Length; i++)
            {
                String pkc = pkColumns[i];
                String fkc = fkColumns[i];

                String pkTableNameFiltered = Settings.TableRename(pkTableName, pkSchema, pkTable.IsView); // TODO: This can probably be done-away with. Is `AddRelationship` called before or after table.NameFiltered is set?

                ForeignKey fk = new ForeignKey(
                    fkTableName: fkTable.Name,
                    fkSchema: fkSchema,
                    pkTableName: pkTable.Name,
                    pkSchema: pkSchema,
                    fkColumn: fkc,
                    pkColumn: pkc,
                    constraintName: "AddRelationship: " + relationshipName,
                    pkTableNameFiltered: pkTableNameFiltered,
                    ordinal: Int32.MaxValue,
                    cascadeOnDelete: false,
                    isNotEnforced: false
                );
                fk.IncludeReverseNavigation = true;

                fkList.Add(fk);
                fkTable.HasForeignKey = true;
            }
        }

        private List<StoredProcedure> LoadStoredProcs(DbProviderFactory factory)
        {
            if (factory == null || !Settings.IncludeStoredProcedures)
                return new List<StoredProcedure>();

            try
            {
                using (var conn = factory.CreateConnection())
                {
                    if (conn == null)
                        return new List<StoredProcedure>();

                    conn.ConnectionString = Settings.ConnectionString;
                    conn.Open();

                    var reader = new SqlServerSchemaReader(conn, factory);
                    var storedProcs = reader.ReadStoredProcs();
                    conn.Close();
                    
                    using (var sqlConnection = new SqlConnection(Settings.ConnectionString))
                    {
                        foreach (var proc in storedProcs)
                            reader.ReadStoredProcReturnObject(sqlConnection, proc);
                    }

                    // Remove stored procs where the return model type contains spaces and cannot be mapped
                    // Also need to remove any TVF functions with parameters that are non scalar types, such as DataTable
                    var validStoredProcedures = new List<StoredProcedure>();
                    foreach (var sp in storedProcs)
                    {
                        if (!sp.ReturnModels.Any())
                        {
                            validStoredProcedures.Add(sp);
                            continue;
                        }

                        if (sp.ReturnModels.Any(returnColumns => returnColumns.Any(c => c.ColumnName.Contains(" "))))
                            continue;

                        if (sp.IsTVF && sp.Parameters.Any(c => c.PropertyType == "System.Data.DataTable"))
                            continue;

                        validStoredProcedures.Add(sp);
                    }
                    return validStoredProcedures;
                }
            }
            catch (Exception ex)
            {
                PrintError("Failed to read database schema for stored procedures.", ex);
                return new List<StoredProcedure>();
            }
        }



        // Calculates the relationship between a child table and it's parent table.
        public static Relationship CalcRelationship(Table parentTable, Table childTable, List<Column> childTableCols, List<Column> parentTableCols)
        {
            if (childTableCols.Count == 1 && parentTableCols.Count == 1)
                return CalcRelationshipSingle(parentTable, childTable, childTableCols.First(), parentTableCols.First());

            // This relationship has multiple composite keys

            // childTable FK columns are exactly the primary key (they are part of primary key, and no other columns are primary keys) //TODO: we could also check if they are an unique index
            bool childTableColumnsAllPrimaryKeys = (childTableCols.Count == childTableCols.Count(x => x.IsPrimaryKey)) && (childTableCols.Count == childTable.PrimaryKeys.Count());

            // parentTable columns are exactly the primary key (they are part of primary key, and no other columns are primary keys) //TODO: we could also check if they are an unique index
            bool parentTableColumnsAllPrimaryKeys = (parentTableCols.Count == parentTableCols.Count(x => x.IsPrimaryKey)) && (parentTableCols.Count == parentTable.PrimaryKeys.Count());

            // childTable FK columns are not only FK but also the whole PK (not only part of PK); parentTable columns are the whole PK (not only part of PK) - so it's 1:1
            if (childTableColumnsAllPrimaryKeys && parentTableColumnsAllPrimaryKeys)
                return Relationship.OneToOne;

            return Relationship.ManyToOne;
        }

        // Calculates the relationship between a child table and it's parent table.
        public static Relationship CalcRelationshipSingle(Table parentTable, Table childTable, Column childTableCol, Column parentTableCol)
        {
            if (!childTableCol.IsPrimaryKey && !childTableCol.IsUniqueConstraint)
                return Relationship.ManyToOne;

            if (!parentTableCol.IsPrimaryKey && !parentTableCol.IsUniqueConstraint)
                return Relationship.ManyToOne;

            if (childTable.PrimaryKeys.Count() != 1)
                return Relationship.ManyToOne;

            if (parentTable.PrimaryKeys.Count() != 1)
                return Relationship.ManyToOne;

            return Relationship.OneToOne;
        }

        // Callbacks **********************************************************************************************************************
        // This method will be called right before we write the POCO header.
        Action<Table> WritePocoClassAttributes = t =>
        {
            if (Settings.UseDataAnnotations)
            {
                foreach (var dataAnnotation in t.DataAnnotations)
                {
                    WriteLine("    [" + dataAnnotation + "]");
                }
            }

            // Example:
            // if(t.ClassName.StartsWith("Order"))
            //     WriteLine("    [SomeAttribute]");
        };

        // This method will be called right before we write the POCO header.
        Action<Table> WritePocoClassExtendedComments = t =>
        {
            if (Settings.IncludeExtendedPropertyComments != CommentsStyle.None && !string.IsNullOrEmpty(t.ExtendedProperty))
            {
                var lines = t.ExtendedProperty
                    .Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                WriteLine("    ///<summary>");
                foreach (var line in lines.Select(x => x.Replace("///", string.Empty).Trim()))
                {
                    WriteLine("    /// {0}", System.Security.SecurityElement.Escape(line));
                }
                WriteLine("    ///</summary>");
            }
        };

        // Writes optional base classes
        Func<Table, string> WritePocoBaseClasses = t =>
        {
            //if (t.ClassName == "User")
            //    return ": IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>";

            // Or use the maker class to dynamically build more complex definitions
            /* Example:
            var r = new BaseClassMaker("POCO.Sample.Data.MetaModelObject");
            r.AddInterface("POCO.Sample.Data.IObjectWithTableName");
            r.AddInterface("POCO.Sample.Data.IObjectWithId",
                t.Columns.Any(x => x.IsPrimaryKey && !x.IsNullable && x.NameHumanCase.Equals("Id", StringComparison.InvariantCultureIgnoreCase) && x.PropertyType == "long"));
            r.AddInterface("POCO.Sample.Data.IObjectWithUserId",
                t.Columns.Any(x => !x.IsPrimaryKey && !x.IsNullable && x.NameHumanCase.Equals("UserId", StringComparison.InvariantCultureIgnoreCase) && x.PropertyType == "long"));
            return r.ToString();
            */

            return "";
        };

        // Writes any boilerplate stuff inside the POCO class
        Action<Table> WritePocoBaseClassBody = t =>
        {
            // Do nothing by default
            // Example:
            // WriteLine("        // " + t.ClassName);
        };

        Func<Column, string> WritePocoColumn = c =>
        {
            bool commentWritten = false;
            if ((Settings.IncludeExtendedPropertyComments == CommentsStyle.InSummaryBlock ||
                 Settings.IncludeComments == CommentsStyle.InSummaryBlock) &&
                !string.IsNullOrEmpty(c.SummaryComments))
            {
                WriteLine(string.Empty);
                WriteLine("        ///<summary>");
                WriteLine("        /// {0}", System.Security.SecurityElement.Escape(c.SummaryComments));
                WriteLine("        ///</summary>");
                commentWritten = true;
            }
            if (Settings.UseDataAnnotations || Settings.UseDataAnnotationsWithFluent)
            {
                if (c.Ordinal > 1 && !commentWritten)
                    WriteLine(string.Empty);    // Leave a blank line before the next property

                foreach (var dataAnnotation in c.DataAnnotations)
                {
                    WriteLine("        [" + dataAnnotation + "]");
                }
            }

            // Example of adding a [Required] data annotation attribute to all non-null fields
            //if (!c.IsNullable)
            //    return "        [System.ComponentModel.DataAnnotations.Required] " + c.Entity;

            return "        " + c.Entity;
        };


    }
}
