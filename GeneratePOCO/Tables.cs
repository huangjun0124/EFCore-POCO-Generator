using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratePOCO
{
    public class Tables : List<Table>
    {
        public Table GetTable(string tableName, string schema)
        {
            return this.SingleOrDefault(x =>
                string.Compare(x.Name, tableName, StringComparison.OrdinalIgnoreCase) == 0 &&
                string.Compare(x.Schema, schema, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public void SetPrimaryKeys()
        {
            foreach (var tbl in this)
            {
                tbl.SetPrimaryKeys();
            }
        }

        public void IdentifyMappingTables(List<ForeignKey> fkList, bool checkForFkNameClashes)
        {
            foreach (var tbl in this.Where(x => x.HasForeignKey))
            {
                tbl.IdentifyMappingTable(fkList, this, checkForFkNameClashes);
            }
        }

        public void ResetNavigationProperties()
        {
            foreach (var tbl in this)
            {
                tbl.ResetNavigationProperties();
            }
        }
    }
}
