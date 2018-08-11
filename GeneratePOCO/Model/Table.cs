using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    [DebuggerDisplay("Name={Name} IsView={IsView}")]
    public class Table
    {
        public string Name;
        public string NameHumanCase;
        public string Schema;
        public string Type;
        public string ClassName;
        public string Suffix;
        public string ExtendedProperty;
        public bool IsMapping;
        public bool IsView;
        public bool HasForeignKey;
        public bool HasNullableColumns;
        public bool HasPrimaryKey;
        public TableTemporalType TemporalType;

        public List<Column> Columns;
        public List<PropertyAndComments> ReverseNavigationProperty;
        public List<string> MappingConfiguration;
        public List<string> ReverseNavigationCtor;
        public List<string> ReverseNavigationUniquePropName;
        public List<string> ReverseNavigationUniquePropNameClashes;
        public List<string> DataAnnotations;

        public Table()
        {
            Columns = new List<Column>();
            ResetNavigationProperties();
            ReverseNavigationUniquePropNameClashes = new List<string>();
            DataAnnotations = new List<string>();
        }

        internal static string GetLazyLoadingMarker()
        {
            return Settings.UseLazyLoading ? "virtual " : string.Empty;
        }

        public string NameHumanCaseWithSuffix()
        {
            return NameHumanCase + Suffix;
        }

        public void ResetNavigationProperties()
        {
            MappingConfiguration = new List<string>();
            ReverseNavigationProperty = new List<PropertyAndComments>();
            ReverseNavigationCtor = new List<string>();
            ReverseNavigationUniquePropName = new List<string>();
            foreach (var col in Columns)
                col.ResetNavigationProperties();
        }

        public void SetPrimaryKeys()
        {
            HasPrimaryKey = Columns.Any(x => x.IsPrimaryKey);
            if (HasPrimaryKey)
                return; // Table has at least one primary key

            // This table is not allowed in EntityFramework as it does not have a primary key.
            // Therefore generate a composite key from all non-null fields.
            foreach (var col in Columns.Where(x => !x.IsNullable && !x.Hidden))
            {
                col.IsPrimaryKey = true;
                HasPrimaryKey = true;
            }
        }

        public IEnumerable<Column> PrimaryKeys
        {
            get
            {
                return Columns
                    .Where(x => x.IsPrimaryKey)
                    .OrderBy(x => x.PrimaryKeyOrdinal)
                    .ThenBy(x => x.Ordinal)
                    .ToList();
            }
        }

        public string PrimaryKeyNameHumanCase()
        {
            var data = PrimaryKeys.Select(x => "x." + x.NameHumanCase).ToList();
            var n = data.Count;
            if (n == 0)
                return string.Empty;
            if (n == 1)
                return "x => " + data.First();
            // More than one primary key
            return string.Format("x => new {{ {0} }}", string.Join(", ", data));
        }

        public Column this[string columnName]
        {
            get { return GetColumn(columnName); }
        }

        public Column GetColumn(string columnName)
        {
            return Columns.SingleOrDefault(x => string.Compare(x.Name, columnName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public string GetUniqueColumnName(string tableNameHumanCase, ForeignKey foreignKey, bool checkForFkNameClashes, bool makeSingular, Relationship relationship)
        {
            var addReverseNavigationUniquePropName = (checkForFkNameClashes || Name == foreignKey.FkTableName || (Name == foreignKey.PkTableName && foreignKey.IncludeReverseNavigation));
            if (ReverseNavigationUniquePropName.Count == 0)
            {
                ReverseNavigationUniquePropName.Add(NameHumanCase);
                ReverseNavigationUniquePropName.AddRange(Columns.Select(c => c.NameHumanCase));
            }

            if (!makeSingular)
                tableNameHumanCase = Inflector.MakePlural(tableNameHumanCase);

            if (checkForFkNameClashes && ReverseNavigationUniquePropName.Contains(tableNameHumanCase) && !ReverseNavigationUniquePropNameClashes.Contains(tableNameHumanCase))
                ReverseNavigationUniquePropNameClashes.Add(tableNameHumanCase); // Name clash

            // Attempt 1
            string fkName = (Settings.UsePascalCase ? Inflector.ToTitleCase(foreignKey.FkColumn) : foreignKey.FkColumn).Replace(" ", "").Replace("$", "");
            string name = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 1);
            string col;
            if (!ReverseNavigationUniquePropNameClashes.Contains(name) && !ReverseNavigationUniquePropName.Contains(name))
            {
                if (addReverseNavigationUniquePropName)
                {
                    ReverseNavigationUniquePropName.Add(name);
                }

                return name;
            }

            if (Name == foreignKey.FkTableName)
            {
                // Attempt 2
                if (fkName.ToLowerInvariant().EndsWith("id"))
                {
                    col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 2);
                    if (checkForFkNameClashes && ReverseNavigationUniquePropName.Contains(col) &&
                        !ReverseNavigationUniquePropNameClashes.Contains(col))
                        ReverseNavigationUniquePropNameClashes.Add(col); // Name clash

                    if (!ReverseNavigationUniquePropNameClashes.Contains(col) &&
                        !ReverseNavigationUniquePropName.Contains(col))
                    {
                        if (addReverseNavigationUniquePropName)
                        {
                            ReverseNavigationUniquePropName.Add(col);
                        }

                        return col;
                    }
                }

                // Attempt 3
                col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 3);
                if (checkForFkNameClashes && ReverseNavigationUniquePropName.Contains(col) &&
                    !ReverseNavigationUniquePropNameClashes.Contains(col))
                    ReverseNavigationUniquePropNameClashes.Add(col); // Name clash

                if (!ReverseNavigationUniquePropNameClashes.Contains(col) &&
                    !ReverseNavigationUniquePropName.Contains(col))
                {
                    if (addReverseNavigationUniquePropName)
                    {
                        ReverseNavigationUniquePropName.Add(col);
                    }

                    return col;
                }
            }

            // Attempt 4
            col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 4);
            if (checkForFkNameClashes && ReverseNavigationUniquePropName.Contains(col) && !ReverseNavigationUniquePropNameClashes.Contains(col))
                ReverseNavigationUniquePropNameClashes.Add(col); // Name clash

            if (!ReverseNavigationUniquePropNameClashes.Contains(col) && !ReverseNavigationUniquePropName.Contains(col))
            {
                if (addReverseNavigationUniquePropName)
                {
                    ReverseNavigationUniquePropName.Add(col);
                }

                return col;
            }

            // Attempt 5
            for (int n = 1; n < 99; ++n)
            {
                col = Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 5) + n;

                if (ReverseNavigationUniquePropName.Contains(col))
                    continue;

                if (addReverseNavigationUniquePropName)
                {
                    ReverseNavigationUniquePropName.Add(col);
                }

                return col;
            }

            // Give up
            return Settings.ForeignKeyName(tableNameHumanCase, foreignKey, fkName, relationship, 6);
        }

        public void AddReverseNavigation(Relationship relationship, string fkName, Table fkTable, string propName, string constraint, List<ForeignKey> fks, Table mappingTable = null)
        {
            var fkNames = "";
            switch (relationship)
            {
                case Relationship.OneToOne:
                case Relationship.OneToMany:
                case Relationship.ManyToOne:
                    fkNames = (fks.Count > 1 ? "(" : "") + string.Join(", ", fks.Select(x => "[" + x.FkColumn + "]").Distinct().ToArray()) + (fks.Count > 1 ? ")" : "");
                    break;
                case Relationship.ManyToMany:
                    break;
            }
            string accessModifier = fks != null && fks.FirstOrDefault() != null ? (fks.FirstOrDefault().AccessModifier ?? "public") : "public";
            switch (relationship)
            {
                case Relationship.OneToOne:
                    ReverseNavigationProperty.Add(
                        new PropertyAndComments()
                        {
                            AdditionalDataAnnotations = Settings.ForeignKeyAnnotationsProcessing(fkTable, this, propName),
                            Definition = string.Format("{0} {1}{2} {3} {{ get; set; }}{4}", accessModifier, GetLazyLoadingMarker(), fkTable.NameHumanCaseWithSuffix(), propName, Settings.IncludeComments != CommentsStyle.None ? " // " + constraint : string.Empty),
                            Comments = string.Format("Parent (One-to-One) {0} pointed by [{1}].{2} ({3})", NameHumanCaseWithSuffix(), fkTable.Name, fkNames, fks.First().ConstraintName)
                        }
                    );
                    break;

                case Relationship.OneToMany:
                    ReverseNavigationProperty.Add(
                        new PropertyAndComments()
                        {
                            AdditionalDataAnnotations = Settings.ForeignKeyAnnotationsProcessing(fkTable, this, propName),
                            Definition = string.Format("{0} {1}{2} {3} {{ get; set; }}{4}", accessModifier, GetLazyLoadingMarker(), fkTable.NameHumanCaseWithSuffix(), propName, Settings.IncludeComments != CommentsStyle.None ? " // " + constraint : string.Empty),
                            Comments = string.Format("Parent {0} pointed by [{1}].{2} ({3})", NameHumanCaseWithSuffix(), fkTable.Name, fkNames, fks.First().ConstraintName)
                        }
                    );
                    break;

                case Relationship.ManyToOne:
                    string initialization1 = string.Empty;
                    if (Settings.UsePropertyInitializers)
                        initialization1 = string.Format(" = new {0}<{1}>();", Settings.CollectionType, fkTable.NameHumanCaseWithSuffix());
                    ReverseNavigationProperty.Add(
                        new PropertyAndComments()
                        {
                            AdditionalDataAnnotations = Settings.ForeignKeyAnnotationsProcessing(fkTable, this, propName),
                            Definition = string.Format("{0} {1}{2}<{3}> {4} {{ get; set; }}{5}{6}", accessModifier, GetLazyLoadingMarker(), Settings.CollectionInterfaceType, fkTable.NameHumanCaseWithSuffix(), propName, initialization1, Settings.IncludeComments != CommentsStyle.None ? " // " + constraint : string.Empty),
                            Comments = string.Format("Child {0} where [{1}].{2} point to this entity ({3})", Inflector.MakePlural(fkTable.NameHumanCase), fkTable.Name, fkNames, fks.First().ConstraintName)
                        }
                    );
                    ReverseNavigationCtor.Add(string.Format("{0} = new {1}<{2}>();", propName, Settings.CollectionType, fkTable.NameHumanCaseWithSuffix()));
                    break;

                case Relationship.ManyToMany:
                    string initialization2 = string.Empty;
                    if (Settings.UsePropertyInitializers)
                        initialization2 = string.Format(" = new {0}<{1}>();", Settings.CollectionType, fkTable.NameHumanCaseWithSuffix());
                    ReverseNavigationProperty.Add(
                        new PropertyAndComments()
                        {
                            AdditionalDataAnnotations = Settings.ForeignKeyAnnotationsProcessing(fkTable, this, propName),
                            Definition = string.Format("{0} {1}{2}<{3}> {4} {{ get; set; }}{5}{6}", accessModifier, GetLazyLoadingMarker(), Settings.CollectionInterfaceType, fkTable.NameHumanCaseWithSuffix(), propName, initialization2, Settings.IncludeComments != CommentsStyle.None ? " // Many to many mapping" : string.Empty),
                            Comments = string.Format("Child {0} (Many-to-Many) mapped by table [{1}]", Inflector.MakePlural(fkTable.NameHumanCase), mappingTable == null ? string.Empty : mappingTable.Name)
                        }
                    );

                    ReverseNavigationCtor.Add(string.Format("{0} = new {1}<{2}>();", propName, Settings.CollectionType, fkTable.NameHumanCaseWithSuffix()));
                    break;

                default:
                    throw new ArgumentOutOfRangeException("relationship");
            }
        }

        public void AddMappingConfiguration(ForeignKey left, ForeignKey right, string leftPropName, string rightPropName)
        {
            MappingConfiguration.Add(string.Format(@"HasMany(t => t.{0}).WithMany(t => t.{1}).Map(m =>
            {{
                m.ToTable(""{2}""{5});
                m.MapLeftKey(""{3}"");
                m.MapRightKey(""{4}"");
            }});", leftPropName, rightPropName, left.FkTableName, left.FkColumn, right.FkColumn, Settings.IsSqlCe ? string.Empty : ", \"" + left.FkSchema + "\""));
        }

        public void IdentifyMappingTable(List<ForeignKey> fkList, Tables tables, bool checkForFkNameClashes)
        {
            IsMapping = false;

            var nonReadOnlyColumns = Columns.Where(c => !c.IsIdentity && !c.IsRowVersion && !c.IsStoreGenerated && !c.Hidden).ToList();

            // Ignoring read-only columns, it must have only 2 columns to be a mapping table
            if (nonReadOnlyColumns.Count != 2)
                return;

            // Must have 2 primary keys
            if (nonReadOnlyColumns.Count(x => x.IsPrimaryKey) != 2)
                return;

            // No columns should be nullable
            if (nonReadOnlyColumns.Any(x => x.IsNullable))
                return;

            // Find the foreign keys for this table
            var foreignKeys = fkList.Where(x =>
                                            string.Compare(x.FkTableName, Name, StringComparison.OrdinalIgnoreCase) == 0 &&
                                            string.Compare(x.FkSchema, Schema, StringComparison.OrdinalIgnoreCase) == 0)
                                    .ToList();

            // Each column must have a foreign key, therefore check column and foreign key counts match
            if (foreignKeys.Select(x => x.FkColumn).Distinct().Count() != 2)
                return;

            ForeignKey left = foreignKeys[0];
            ForeignKey right = foreignKeys[1];
            if (!left.IncludeReverseNavigation || !right.IncludeReverseNavigation)
                return;

            Table leftTable = tables.GetTable(left.PkTableName, left.PkSchema);
            if (leftTable == null)
                return;

            Table rightTable = tables.GetTable(right.PkTableName, right.PkSchema);
            if (rightTable == null)
                return;

            var leftPropName = leftTable.GetUniqueColumnName(rightTable.NameHumanCase, right, checkForFkNameClashes, false, Relationship.ManyToOne); // relationship from the mapping table to each side is Many-to-One
            var rightPropName = rightTable.GetUniqueColumnName(leftTable.NameHumanCase, left, checkForFkNameClashes, false, Relationship.ManyToOne); // relationship from the mapping table to each side is Many-to-One
            leftTable.AddMappingConfiguration(left, right, leftPropName, rightPropName);

            IsMapping = true;
            rightTable.AddReverseNavigation(Relationship.ManyToMany, rightTable.NameHumanCase, leftTable, rightPropName, null, null, this);
            leftTable.AddReverseNavigation(Relationship.ManyToMany, leftTable.NameHumanCase, rightTable, leftPropName, null, null, this);
        }

        public void SetupDataAnnotations()
        {
            var schema = String.Empty;
            if (!Settings.IsSqlCe)
                schema = String.Format(", Schema = \"{0}\"", Schema);
            DataAnnotations = new List<string>
            {
                HasPrimaryKey
                    ? string.Format("Table(\"{0}\"{1})", Name, schema)
                    : "NotMapped"
            };

        }
    }
}
