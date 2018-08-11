using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GeneratePOCO
{
    // Settings - edit these in the main <name>.tt file *******************************************************************************
    public static class Settings
    {
        // As an alternative to ConnectionStringName above, which must match your app/web.config connection string name, you can override them below
        // Settings.ConnectionString = "Data Source=(local);Initial Catalog=Northwind;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator";
        // Settings.ProviderName = "System.Data.SqlClient";

        // Main settings
        public static string ConnectionStringName = "HangfireReadOnly";
        public static string ConnectionString; //= ConfigurationManager.ConnectionStrings["HangfireDbContext"].ConnectionString;
        public static string ProviderName; //= ConfigurationManager.ConnectionStrings["HangfireDbContext"].ProviderName;

        public static string Namespace = "";
        public static string DbContextName = "";


        public static int CommandTimeout = 600;
        public static bool IncludeViews = true;
        public static bool IncludeSynonyms;
        public static bool IncludeStoredProcedures;
        public static bool IncludeTableValuedFunctions;
        public static bool AddIDbContextFactory;
        public static bool AddUnitTestingDbContext;

        private static string _dbContextInterfaceName;
        public static string DbContextInterfaceName
        {
            get { return _dbContextInterfaceName ?? ("I" + DbContextName); }
            set { _dbContextInterfaceName = value; }
        }

        /// <summary>
        /// Specify what the base classes are for your database context interface
        /// </summary>
        public static string DbContextInterfaceBaseClasses = "System.IDisposable";
        /// <summary>
        /// Specify what the base class is for your DbContext. For ASP.NET Identity use "IdentityDbContext<ApplicationUser>"
        /// </summary>
        public static string DbContextBaseClass = "System.Data.Entity.DbContext";
        /// <summary>
        /// If true, then DbContext will have a default (parameterless) constructor which automatically passes in the connection string name, if false then no parameterless constructor will be created.
        /// </summary>
        public static bool AddParameterlessConstructorToDbContext = true;
        /// <summary>
        /// //defaults to "Name=" + ConnectionStringName
        /// </summary>
        private static string _defaultConstructorArgument;
        public static string DefaultConstructorArgument
        {
            get { return _defaultConstructorArgument ?? string.Format('"' + "Name={0}" + '"', ConnectionStringName); }
            set { _defaultConstructorArgument = value; }
        }

        public static string ConfigurationClassName = "Configuration";
        public static string CollectionInterfaceType = "System.Collections.Generic.ICollection";
        public static string CollectionType = "System.Collections.Generic.List";
        public static bool NullableShortHand;
        public static bool UseDataAnnotations;
        public static bool UseDataAnnotationsWithFluent;
        public static string EntityClassesModifiers = "public partial";
        public static string ConfigurationClassesModifiers = "internal";
        public static string DbContextClassModifiers = "public partial";
        public static string DbContextInterfaceModifiers = "public partial";
        public static string MigrationClassModifiers = "internal sealed";
        public static string ResultClassModifiers = "public partial";
        public static bool DbContextClassIsPartial()
        {
            return DbContextClassModifiers != null && DbContextClassModifiers.Contains("partial");
        }

        public static bool EntityClassesArePartial()
        {
            return EntityClassesModifiers != null && EntityClassesModifiers.Contains("partial");
        }

        public static bool ConfigurationClassesArePartial()
        {
            return ConfigurationClassesModifiers != null && ConfigurationClassesModifiers.Contains("partial");
        }
        public static bool GenerateSeparateFiles;
        public static bool UseMappingTables;
        public static bool UsePropertyInitializers;
        public static bool IsSqlCe;
        public static string FileExtension = ".cs";
        public static bool UsePascalCase;
        public static bool UsePrivateSetterForComputedColumns;
        public static CommentsStyle IncludeComments = CommentsStyle.AtEndOfField;
        public static bool IncludeQueryTraceOn9481Flag;
        public static CommentsStyle IncludeExtendedPropertyComments = CommentsStyle.InSummaryBlock;
        public static bool DisableGeographyTypes;
        public static bool PrependSchemaName;
        public static string TableSuffix;
        public static Regex SchemaFilterExclude;
        public static Regex SchemaFilterInclude;
        public static Regex TableFilterExclude;
        public static Regex TableFilterInclude;
        public static Regex StoredProcedureFilterExclude;
        public static Regex StoredProcedureFilterInclude;
        public static Func<StoredProcedure, bool> StoredProcedureFilter = (StoredProcedure sp) =>
        {
            // Example: Exclude any stored procedure in dbo schema with "order" in its name.
            //if(sp.Schema.Equals("dbo", StringComparison.InvariantCultureIgnoreCase) && sp.NameHumanCase.ToLowerInvariant().Contains("order"))
            //    return false;

            return true;
        };

        public static Func<Table, bool> ConfigurationFilter = (Table t) =>
        {
            return true;
        };

        public static Dictionary<string, string> StoredProcedureReturnTypes = new Dictionary<string, string>();
        public static Regex ColumnFilterExclude;
        public static bool UseLazyLoading;
        public static string[] FilenameSearchOrder;
        public static string[] AdditionalNamespaces;
        public static string[] AdditionalContextInterfaceItems;
        public static string[] AdditionalReverseNavigationsDataAnnotations;
        public static string[] AdditionalForeignKeysDataAnnotations;
        public static string ConfigFilePath;
        public static Func<string, string, bool, string> TableRename = (string name, string schema, bool isView) =>
        {
            // Example
            //if (name.StartsWith("tbl"))
            //    name = name.Remove(0, 3);
            //name = name.Replace("_AB", "");

            //if(isView)
            //    name = name + "View";

            // If you turn pascal casing off (UsePascalCase = false), and use the pluralisation service, and some of your
            // tables names are all UPPERCASE, some words ending in IES such as CATEGORIES get singularised as CATEGORy.
            // Therefore you can make them lowercase by using the following
            // return Inflector.MakeLowerIfAllCaps(name);

            // If you are using the pluralisation service and you want to rename a table, make sure you rename the table to the plural form.
            // For example, if the table is called Treez (with a z), and your pluralisation entry is
            //     new CustomPluralizationEntry("Tree", "Trees")
            // Use this TableRename function to rename Treez to the plural (not singular) form, Trees:
            // if (name == "Treez") return "Trees";

            return name;
        };
        public static Func<StoredProcedure, string> StoredProcedureRename = (sp) => sp.NameHumanCase;   // Do nothing by default

        // Use the following function to rename the return model automatically generated for stored procedure.
        // By default it's <proc_name>ReturnModel.
        // Example:
        /*Settings.StoredProcedureReturnModelRename = (name, sp) =>
        {
            if (sp.NameHumanCase.Equals("ComputeValuesForDate", StringComparison.InvariantCultureIgnoreCase))
                return "ValueSet";
            if (sp.NameHumanCase.Equals("SalesByYear", StringComparison.InvariantCultureIgnoreCase))
                return "SalesSet";

            return name;
        };*/
        public static Func<string, StoredProcedure, string> StoredProcedureReturnModelRename = (name, sp) => name; // Do nothing by default
        public static Func<Column, Table, Column> UpdateColumn = (Column column, Table table) =>
        {
            // Rename column
            //if (column.IsPrimaryKey && column.NameHumanCase == "PkId")
            //    column.NameHumanCase = "Id";

            // .IsConcurrencyToken() must be manually configured. However .IsRowVersion() can be automatically detected.
            //if (table.NameHumanCase.Equals("SomeTable", StringComparison.InvariantCultureIgnoreCase) && column.NameHumanCase.Equals("SomeColumn", StringComparison.InvariantCultureIgnoreCase))
            //    column.IsConcurrencyToken = true;

            // Remove table name from primary key
            //if (column.IsPrimaryKey && column.NameHumanCase.Equals(table.NameHumanCase + "Id", StringComparison.InvariantCultureIgnoreCase))
            //    column.NameHumanCase = "Id";

            // Remove column from poco class as it will be inherited from a base class
            //if (column.IsPrimaryKey && table.NameHumanCase.Equals("SomeTable", StringComparison.InvariantCultureIgnoreCase))
            //    column.Hidden = true;

            // Use the extended properties to perform tasks to column
            //if (column.ExtendedProperty == "HIDE")
            //    column.Hidden = true;

            // Apply the "override" access modifier to a specific column.
            // if (column.NameHumanCase == "id")
            //    column.OverrideModifier = true;
            // This will create: public override long id { get; set; }

            // Perform Enum property type replacement
            var enumDefinition = Settings.EnumDefinitions.FirstOrDefault(e =>
                (e.Schema.Equals(table.Schema, StringComparison.InvariantCultureIgnoreCase)) &&
                (e.Table.Equals(table.Name, StringComparison.InvariantCultureIgnoreCase) || e.Table.Equals(table.NameHumanCase, StringComparison.InvariantCultureIgnoreCase)) &&
                (e.Column.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase) || e.Column.Equals(column.NameHumanCase, StringComparison.InvariantCultureIgnoreCase)));

            if (enumDefinition != null)
            {
                column.PropertyType = enumDefinition.EnumType;
                if (!string.IsNullOrEmpty(column.Default))
                    column.Default = "(" + enumDefinition.EnumType + ") " + column.Default;
            }

            return column;
        };
        public static Func<IList<ForeignKey>, Table, Table, bool, ForeignKey> ForeignKeyProcessing = (foreignKeys, fkTable, pkTable, anyNullableColumnInForeignKey) =>
        {
            var foreignKey = foreignKeys.First();

            // If using data annotations and to include the [Required] attribute in the foreign key, enable the following
            //if (!anyNullableColumnInForeignKey)
            //   foreignKey.IncludeRequiredAttribute = true;

            return foreignKey;
        };
        public static Func<Table, Table, string, string[]> ForeignKeyAnnotationsProcessing = (Table fkTable, Table pkTable, string propName) =>
        {
            /* Example:
            // Each navigation property that is a reference to User are left intact
            if (pkTable.NameHumanCase.Equals("User") && propName.Equals("User"))
                return null;

            // all the others are marked with this attribute
            return new[] { "System.Runtime.Serialization.IgnoreDataMember" };
            */

            return null;
        };
        public static Func<ForeignKey, ForeignKey> ForeignKeyFilter = (ForeignKey fk) =>
        {
            // Return null to exclude this foreign key, or set IncludeReverseNavigation = false
            // to include the foreign key but not generate reverse navigation properties.
            // Example, to exclude all foreign keys for the Categories table, use:
            // if (fk.PkTableName == "Categories")
            //    return null;

            // Example, to exclude reverse navigation properties for tables ending with Type, use:
            // if (fk.PkTableName.EndsWith("Type"))
            //    fk.IncludeReverseNavigation = false;

            // You can also change the access modifier of the foreign-key's navigation property:
            // if(fk.PkTableName == "Categories") fk.AccessModifier = "internal";

            return fk;
        };
        public static Func<string, ForeignKey, string, Relationship, short, string> ForeignKeyName = (tableName, foreignKey, foreignKeyName, relationship, attempt) =>
        {
            string fkName;

            // 5 Attempts to correctly name the foreign key
            switch (attempt)
            {
                case 1:
                    // Try without appending foreign key name
                    fkName = tableName;
                    break;

                case 2:
                    // Only called if foreign key name ends with "id"
                    // Use foreign key name without "id" at end of string
                    fkName = foreignKeyName.Remove(foreignKeyName.Length - 2, 2);
                    break;

                case 3:
                    // Use foreign key name only
                    fkName = foreignKeyName;
                    break;

                case 4:
                    // Use table name and foreign key name
                    fkName = tableName + "_" + foreignKeyName;
                    break;

                case 5:
                    // Used in for loop 1 to 99 to append a number to the end
                    fkName = tableName;
                    break;

                default:
                    // Give up
                    fkName = tableName;
                    break;
            }

            // Apply custom foreign key renaming rules. Can be useful in applying pluralization.
            // For example:
            /*if (tableName == "Employee" && foreignKey.FkColumn == "ReportsTo")
                return "Manager";

            if (tableName == "Territories" && foreignKey.FkTableName == "EmployeeTerritories")
                return "Locations";

            if (tableName == "Employee" && foreignKey.FkTableName == "Orders" && foreignKey.FkColumn == "EmployeeID")
                return "ContactPerson";
            */

            // FK_TableName_FromThisToParentRelationshipName_FromParentToThisChildsRelationshipName
            // (e.g. FK_CustomerAddress_Customer_Addresses will extract navigation properties "address.Customer" and "customer.Addresses")
            // Feel free to use and change the following
            /*if (foreignKey.ConstraintName.StartsWith("FK_") && foreignKey.ConstraintName.Count(x => x == '_') == 3)
            {
                var parts = foreignKey.ConstraintName.Split('_');
                if (!string.IsNullOrWhiteSpace(parts[2]) && !string.IsNullOrWhiteSpace(parts[3]) && parts[1] == foreignKey.FkTableName)
                {
                    if (relationship == Relationship.OneToMany)
                        fkName = parts[3];
                    else if (relationship == Relationship.ManyToOne)
                        fkName = parts[2];
                }
            }*/

            return fkName;
        };
        public static Action<Table> ViewProcessing = (Table view) =>
        {
            // Below is example code for the Northwind database that configures the 'VIEW [Orders Qry]' and 'VIEW [Invoices]'
            //switch(view.Name)
            //{
            //case "Orders Qry":
            //    // VIEW [Orders Qry] uniquely identifies rows with the 'OrderID' column:
            //    view.Columns.Single( col => col.Name == "OrderID" ).IsPrimaryKey = true;
            //    break;
            //case "Invoices":
            //    // VIEW [Invoices] has a composite primary key (OrderID+ProductID), so both columns must be marked as a Primary Key:
            //    foreach( Column col in view.Columns.Where( c => c.Name == "OrderID" || c.Name == "ProductID" ) ) col.IsPrimaryKey = true;
            //    break;
            //}
        };
        public static Action<List<ForeignKey>, Tables> AddForeignKeys = (List<ForeignKey> foreignKeys, Tables tablesAndViews) =>
        {
            // In Northwind:
            // [Orders] (Table) to [Invoices] (View) is one-to-many using Orders.OrderID = Invoices.OrderID
            // [Order Details] (Table) to [Invoices] (View) is one-to-zeroOrOne - but uses a composite-key: ( [Order Details].OrderID,ProductID = [Invoices].OrderID,ProductID )
            // [Orders] (Table) to [Orders Qry] (View) is one-to-zeroOrOne ( [Orders].OrderID = [Orders Qry].OrderID )

            // AddRelationship is a helper function that creates ForeignKey objects and adds them to the foreignKeys list:
            //AddRelationship( foreignKeys, tablesAndViews, "orders_to_invoices"      , "dbo", "Orders"       , "OrderID"                       , "dbo", "Invoices", "OrderID" );
            //AddRelationship( foreignKeys, tablesAndViews, "orderDetails_to_invoices", "dbo", "Order Details", new[] { "OrderID", "ProductID" }, "dbo", "Invoices",  new[] { "OrderID", "ProductID" } );
            //AddRelationship( foreignKeys, tablesAndViews, "orders_to_ordersQry"     , "dbo", "Orders"       , "OrderID"                       , "dbo", "Orders Qry", "OrderID" );
        };
        public static string MigrationConfigurationFileName;
        public static string MigrationStrategy = "MigrateDatabaseToLatestVersion";
        public static string ContextKey;
        public static bool AutomaticMigrationsEnabled;
        public static bool AutomaticMigrationDataLossAllowed;
        public static List<EnumDefinition> EnumDefinitions = new List<EnumDefinition>();
        public static Dictionary<string, string> ColumnNameToDataAnnotation= new Dictionary<string, string>
        {
            // This is used when UseDataAnnotations == true or UseDataAnnotationsWithFluent == true;
            // It is used to set a data annotation on a column based on the columns name.
            // Make sure the column name is lowercase in the following array, regardless of how it is in the database
            // Column name       DataAnnotation to add
            { "email",           "EmailAddress" },
            { "emailaddress",    "EmailAddress" },
            { "creditcard",      "CreditCard" },
            { "url",             "Url" },
            { "fax",             "Phone" },
            { "phone",           "Phone" },
            { "phonenumber",     "Phone" },
            { "mobile",          "Phone" },
            { "mobilenumber",    "Phone" },
            { "telephone",       "Phone" },
            { "telephonenumber", "Phone" },
            { "password",        "DataType(DataType.Password)" },
            { "username",        "DataType(DataType.Text)" },
            { "postcode",        "DataType(DataType.PostalCode)" },
            { "postalcode",      "DataType(DataType.PostalCode)" },
            { "zip",             "DataType(DataType.PostalCode)" },
            { "zipcode",         "DataType(DataType.PostalCode)" }
        };
        public static Dictionary<string, string> ColumnTypeToDataAnnotation = new Dictionary<string, string>
        {
            // This is used when UseDataAnnotations == true or UseDataAnnotationsWithFluent == true;
            // It is used to set a data annotation on a column based on the columns's MS SQL type.
            // Make sure the column name is lowercase in the following array, regardless of how it is in the database
            // Column name       DataAnnotation to add
            { "date",            "DataType(DataType.Date)" },
            { "datetime",        "DataType(DataType.DateTime)" },
            { "datetime2",       "DataType(DataType.DateTime)" },
            { "datetimeoffset",  "DataType(DataType.DateTime)" },
            { "smallmoney",      "DataType(DataType.Currency)" },
            { "money",           "DataType(DataType.Currency)" }
        };
        public static bool IncludeCodeGeneratedAttribute;
        public static Tables Tables;
        public static HashSet<string> TableNameHashSet;

        public static List<StoredProcedure> StoredProcs;

        public static Elements ElementsToGenerate= Elements.Poco | Elements.Context | Elements.UnitOfWork | Elements.PocoConfiguration;

        public static string PocoNamespace, ContextNamespace, UnitOfWorkNamespace, PocoConfigurationNamespace;

        public static float TargetFrameworkVersion;
        public static Func<string, bool> IsSupportedFrameworkVersion = (string frameworkVersion) =>
        {
            var nfi = CultureInfo.InvariantCulture.NumberFormat;
            var isSupported = float.Parse(frameworkVersion, nfi);
            return isSupported <= TargetFrameworkVersion;
        };
    };
}
