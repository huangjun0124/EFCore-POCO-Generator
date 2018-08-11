using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    public class EnumDefinition
    {
        public string Schema;
        public string Table;
        public string Column;
        public string EnumType;
    }

    public enum TableTemporalType
    {
        None,
        Verioned,
        History
    }

    public enum ColumnGeneratedAlwaysType
    {
        NotApplicable = 0,
        AsRowStart = 1,
        AsRowEnd = 2
    }

    public enum StoredProcedureParameterMode
    {
        In,
        InOut,
        Out
    }

    public enum Relationship
    {
        OneToOne,
        OneToMany,
        ManyToOne,
        ManyToMany,
        DoNotUse
    }

    [Flags]
    public enum CommentsStyle
    {
        None,
        InSummaryBlock,
        AtEndOfField
    };

    // Settings to allow selective code generation
    [Flags]
    public enum Elements
    {
        None = 0,
        Poco = 1,
        Context = 2,
        UnitOfWork = 4,
        PocoConfiguration = 8
    };
}
