using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    class LambdaCollections
    {
        public static readonly Func<StoredProcedure, string> WriteStoredProcFunctionName = sp => sp.NameHumanCase;

        public static readonly Func<StoredProcedure, bool> StoredProcHasOutParams = (sp) =>
        {
            return sp.Parameters.Any(x => x.Mode != StoredProcedureParameterMode.In);
        };

        public static readonly Func<StoredProcedure, bool, string> WriteStoredProcFunctionParams = (sp, includeProcResult) =>
        {
            var sb = new StringBuilder();
            int n = 1;
            int count = sp.Parameters.Count;
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendFormat("{0}{1}{2} {3}{4}",
                    p.Mode == StoredProcedureParameterMode.In ? "" : "out ",
                    p.PropertyType,
                    CommonParams.NotNullable.Contains(p.PropertyType.ToLower()) ? string.Empty : "?",
                    p.NameHumanCase,
                    (n++ < count) ? ", " : string.Empty);
            }
            if (includeProcResult && sp.ReturnModels.Count > 0 && sp.ReturnModels.First().Count > 0)
                sb.AppendFormat((sp.Parameters.Count > 0 ? ", " : "") + "out int procResult");
            return sb.ToString();
        };

        public static readonly Func<StoredProcedure, string> WriteStoredProcFunctionOverloadCall = (sp) =>
        {
            var sb = new StringBuilder();
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendFormat("{0}{1}, ",
                    p.Mode == StoredProcedureParameterMode.In ? "" : "out ",
                    p.NameHumanCase);
            }
            sb.Append("out procResult");
            return sb.ToString();
        };

        public static readonly Func<StoredProcedure, string> WriteStoredProcFunctionSqlAtParams = sp =>
        {
            var sb = new StringBuilder();
            int n = 1;
            int count = sp.Parameters.Count;
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendFormat("{0}{1}{2}",
                    p.Name,
                    p.Mode == StoredProcedureParameterMode.In ? string.Empty : " OUTPUT",
                    (n++ < count) ? ", " : string.Empty);
            }
            return sb.ToString();
        };

        public static readonly Func<StoredProcedureParameter, string> WriteStoredProcSqlParameterName = p => p.NameHumanCase + "Param";

        public static readonly Func<StoredProcedure, bool, string> WriteStoredProcFunctionDeclareSqlParameter = (sp, includeProcResult) =>
        {
            var sb = new StringBuilder();
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                var isNullable = !CommonParams.NotNullable.Contains(p.PropertyType.ToLower());
                var getValueOrDefault = isNullable ? ".GetValueOrDefault()" : string.Empty;
                var isGeography = p.PropertyType == "System.Data.Entity.Spatial.DbGeography";

                sb.AppendLine(
                    $"            var {WriteStoredProcSqlParameterName(p)} = new System.Data.SqlClient.SqlParameter"
                    + $" {{ ParameterName = \"{p.Name}\", "
                    + (isGeography ? "UdtTypeName = \"geography\"" : $"SqlDbType = System.Data.SqlDbType.{p.SqlDbType}")
                    + ", Direction = System.Data.ParameterDirection."
                    + (p.Mode == StoredProcedureParameterMode.In ? "Input" : "Output")
                    + (p.Mode == StoredProcedureParameterMode.In
                        ? ", Value = " + (isGeography
                            ? $"Microsoft.SqlServer.Types.SqlGeography.Parse({p.NameHumanCase}.AsText())"
                            : p.NameHumanCase + getValueOrDefault)
                        : string.Empty)
                    + (p.MaxLength != 0 ? ", Size = " + p.MaxLength : string.Empty)
                    + ((p.Precision > 0 || p.Scale > 0) ? ", Precision = " + p.Precision + ", Scale = " + p.Scale : string.Empty)
                    + (p.PropertyType.ToLower().Contains("datatable") ? ", TypeName = \"" + p.UserDefinedTypeName + "\"" : string.Empty)
                    + " };");

                if (p.Mode == StoredProcedureParameterMode.In)
                {
                    sb.AppendFormat(
                        isNullable
                            ? "            if (!{0}.HasValue){1}                {0}Param.Value = System.DBNull.Value;{1}{1}"
                            : "            if ({0}Param.Value == null){1}                {0}Param.Value = System.DBNull.Value;{1}{1}",
                        p.NameHumanCase, Environment.NewLine);
                }
            }
            if (includeProcResult && sp.ReturnModels.Count < 2)
                sb.AppendLine("            var procResultParam = new System.Data.SqlClient.SqlParameter { ParameterName = \"@procResult\", SqlDbType = System.Data.SqlDbType.Int, Direction = System.Data.ParameterDirection.Output };");
            return sb.ToString();
        };

        public static readonly Func<StoredProcedure, string> WriteTableValuedFunctionDeclareSqlParameter = sp =>
        {
            var sb = new StringBuilder();
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                sb.AppendLine(string.Format("            var {0}Param = new System.Data.Entity.Core.Objects.ObjectParameter(\"{1}\", typeof({2})) {{ Value = (object){3} }};",
                    p.NameHumanCase,
                    p.Name.Substring(1),
                    p.PropertyType,
                    p.NameHumanCase + (p.Mode == StoredProcedureParameterMode.In && CommonParams.NotNullable.Contains(p.PropertyType.ToLowerInvariant()) ? string.Empty : " ?? System.DBNull.Value")));
            }
            return sb.ToString();
        };

        public static readonly Func<StoredProcedure, bool, string> WriteStoredProcFunctionSqlParameterAnonymousArray = (sp, includeProcResultParam) =>
        {
            var sb = new StringBuilder();
            bool hasParam = false;
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                sb.Append(string.Format("{0}Param, ", p.NameHumanCase));
                hasParam = true;
            }
            if (includeProcResultParam)
                sb.Append("procResultParam");
            else if (hasParam)
                sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        };

        public static readonly Func<StoredProcedure, string> WriteTableValuedFunctionSqlParameterAnonymousArray = sp =>
        {
            if (sp.Parameters.Count == 0)
                return "new System.Data.Entity.Core.Objects.ObjectParameter[] { }";
            var sb = new StringBuilder();
            foreach (var p in sp.Parameters.OrderBy(x => x.Ordinal))
            {
                sb.Append(string.Format("{0}Param, ", p.NameHumanCase));
            }
            return sb.ToString().Substring(0, sb.Length - 2);
        };

        public static readonly Func<StoredProcedure, bool, string> WriteStoredProcFunctionSetSqlParameters = (sp, isFake) =>
        {
            var sb = new StringBuilder();
            foreach (var p in sp.Parameters.Where(x => x.Mode != StoredProcedureParameterMode.In).OrderBy(x => x.Ordinal))
            {
                var Default = string.Format("default({0})", p.PropertyType);
                var notNullable = CommonParams.NotNullable.Contains(p.PropertyType.ToLower());

                if (isFake)
                    sb.AppendLine(string.Format("            {0} = {1};", p.NameHumanCase, Default));
                else
                {
                    sb.AppendLine(string.Format("            if (IsSqlParameterNull({0}Param))", p.NameHumanCase));
                    sb.AppendLine(string.Format("                {0} = {1};", p.NameHumanCase, notNullable ? Default : "null"));
                    sb.AppendLine("            else");
                    sb.AppendLine(string.Format("                {0} = ({1}) {2}Param.Value;", p.NameHumanCase, p.PropertyType, p.NameHumanCase));
                }
            }
            return sb.ToString();
        };

        public static readonly Func<StoredProcedure, string> WriteStoredProcReturnModelName = sp =>
        {
            if (Settings.StoredProcedureReturnTypes.ContainsKey(sp.NameHumanCase))
                return Settings.StoredProcedureReturnTypes[sp.NameHumanCase];
            if (Settings.StoredProcedureReturnTypes.ContainsKey(sp.Name))
                return Settings.StoredProcedureReturnTypes[sp.Name];

            var name = string.Format("{0}ReturnModel", sp.NameHumanCase);
            if (Settings.StoredProcedureReturnModelRename != null)
            {
                var customName = Settings.StoredProcedureReturnModelRename(name, sp);
                if (!string.IsNullOrEmpty(customName))
                    name = customName;
            }

            return name;
        };

        public static readonly Func<DataColumn, string> WriteStoredProcReturnColumn = col =>
        {
            var columnName = CommonParams.ReservedKeywords.Contains(col.ColumnName) ? "@" + col.ColumnName : col.ColumnName;

            return string.Format("public {0} {1} {{ get; set; }}",
                StoredProcedure.WrapTypeIfNullable(
                    (col.DataType.Name.Equals("SqlHierarchyId") ? "Microsoft.SqlServer.Types." : col.DataType.Namespace + ".") +
                    col.DataType.Name, col),
                columnName);
        };

        public static readonly Func<StoredProcedure, string> WriteStoredProcReturnType = (sp) =>
        {
            var returnModelCount = sp.ReturnModels.Count;
            if (returnModelCount == 0)
                return "int";

            var spReturnClassName = WriteStoredProcReturnModelName(sp);
            return (returnModelCount == 1) ? string.Format("System.Collections.Generic.List<{0}>", spReturnClassName) : spReturnClassName;
        };
    }
}
