using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
    [Table("MyTable")]
    public class MyTable
    {
        [Column("MyId", TypeName = "nchar(10)")]
        public string MyId { get; set; }

        [Column("MyTable", TypeName = "varchar(50)")]
        public string MyTable1 { get; set; }

        [Column("str1", TypeName = "char(10)")]
        public string str1 { get; set; }

        [Column("str2", TypeName = "nvarchar(100)")]
        public string str2 { get; set; }

        [Column("dec1", TypeName = "decimal(18,4)")]
        public decimal? dec1 { get; set; }

        [Column("varcharmax", TypeName = "varchar(max)")]
        public string varcharmax { get; set; }
    }
}
