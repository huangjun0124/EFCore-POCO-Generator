using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("MyTable", Schema = "dbo")]
    public class MyTable
    {
		///<summary>
		/// MyId (Primary key) (length: 10)
		///</summary>
		[Column(@"MyId", TypeName = "nchar(10)")]
		public string MyId { get; set; }

		///<summary>
		/// MyTable (length: 50)
		///</summary>
		[Column(@"MyTable", TypeName = "varchar(50)")]
		public string MyTable_ { get; set; }

		///<summary>
		/// str1 (length: 10)
		///</summary>
		[Column(@"str1", TypeName = "char(10)")]
		public string Str1 { get; set; }

		///<summary>
		/// str2 (length: 100)
		///</summary>
		[Column(@"str2", TypeName = "nvarchar(100)")]
		public string Str2 { get; set; }

		///<summary>
		/// dec1
		///</summary>
		[Column(@"dec1", TypeName = "decimal")]
		public decimal? Dec1 { get; set; }

		///<summary>
		/// varcharmax
		///</summary>
		[Column(@"varcharmax", TypeName = "varchar(max)")]
		public string Varcharmax { get; set; }

    }
}
