using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    public class StoredProcedureParameter
    {
        public int Ordinal;
        public StoredProcedureParameterMode Mode;
        public string Name;
        public string NameHumanCase;
        public string SqlDbType;
        public string PropertyType;
        public string UserDefinedTypeName;
        public int DateTimePrecision;
        public int MaxLength;
        public int Precision;
        public int Scale;
    }
}
