using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    public class ForeignKey
    {
        public string FkTableName { get; private set; }
        public string FkSchema { get; private set; }
        public string PkTableName { get; private set; }
        public string PkTableNameFiltered { get; private set; }
        public string PkSchema { get; private set; }
        public string FkColumn { get; private set; }
        public string PkColumn { get; private set; }
        public string ConstraintName { get; private set; }
        public int Ordinal { get; private set; }
        public bool CascadeOnDelete { get; private set; }
        public bool IsNotEnforced { get; private set; }

        // User settable via ForeignKeyFilter callback
        public string AccessModifier { get; set; }
        public bool IncludeReverseNavigation { get; set; }
        public bool IncludeRequiredAttribute { get; set; }

        public ForeignKey(string fkTableName, string fkSchema, string pkTableName, string pkSchema, string fkColumn, string pkColumn, string constraintName, string pkTableNameFiltered, int ordinal, bool cascadeOnDelete, bool isNotEnforced)
        {
            ConstraintName = constraintName;
            PkColumn = pkColumn;
            FkColumn = fkColumn;
            PkSchema = pkSchema;
            PkTableName = pkTableName;
            FkSchema = fkSchema;
            FkTableName = fkTableName;
            PkTableNameFiltered = pkTableNameFiltered;
            Ordinal = ordinal;
            CascadeOnDelete = cascadeOnDelete;
            IsNotEnforced = isNotEnforced;

            IncludeReverseNavigation = true;
        }

        public string PkTableHumanCase(string suffix)
        {
            var singular = Inflector.MakeSingular(PkTableNameFiltered);
            var pkTableHumanCase = (Settings.UsePascalCase ? Inflector.ToTitleCase(singular) : singular).Replace(" ", "").Replace("$", "");
            if (string.Compare(PkSchema, "dbo", StringComparison.OrdinalIgnoreCase) != 0 && Settings.PrependSchemaName)
                pkTableHumanCase = PkSchema + "_" + pkTableHumanCase;
            pkTableHumanCase += suffix;
            return pkTableHumanCase;
        }
    }
}
