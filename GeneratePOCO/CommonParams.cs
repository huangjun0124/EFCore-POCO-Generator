using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    class CommonParams
    {
        public const string CodeGeneratedAttribute = "[System.CodeDom.Compiler.GeneratedCode(\"EF.Reverse.POCO.Generator\", \"2.37.1.0\")]";
        public const string DataDirectory = "|DataDirectory|";

        public static readonly List<string> NotNullable = new List<string>
        {
            "string",
            "byte[]",
            "datatable",
            "system.data.datatable",
            "object",
            "microsoft.sqlserver.types.sqlgeography",
            "microsoft.sqlserver.types.sqlgeometry",
            "system.data.entity.spatial.dbgeography",
            "system.data.entity.spatial.dbgeometry",
            "system.data.entity.hierarchy.hierarchyid"
        };

        public static readonly List<string> ReservedKeywords = new List<string>
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
            "checked", "class", "const", "continue", "decimal", "default", "delegate", "do",
            "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed",
            "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator",
            "out", "override", "params", "private", "protected", "public", "readonly", "ref",
            "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string",
            "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
            "unchecked", "unsafe", "ushort", "using", "virtual", "volatile", "void", "while"
        };
    }
}
