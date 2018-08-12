using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("YourTable", Schema = "dbo")]
    public class YourTable
    {
		///<summary>
		/// YourTableId (Primary key)
		///</summary>
		[Column(@"YourTableId", TypeName = "int")]
		public int YourTableId { get; set; }

		///<summary>
		/// intCol
		///</summary>
		[Column(@"intCol", TypeName = "int")]
		public int IntCol { get; set; }

		///<summary>
		/// intColNullable
		///</summary>
		[Column(@"intColNullable", TypeName = "int")]
		public int? IntColNullable { get; set; }

		///<summary>
		/// bIsLLL
		///</summary>
		[Column(@"bIsLLL", TypeName = "bit")]
		public bool? BIsLll { get; set; }

		///<summary>
		/// bigint
		///</summary>
		[Column(@"bigint", TypeName = "bigint")]
		public long? Bigint { get; set; }

		///<summary>
		/// binary (length: 1000)
		///</summary>
		[Column(@"binary", TypeName = "binary(1000)")]
		public byte[] Binary { get; set; }

		///<summary>
		/// date
		///</summary>
		[Column(@"date", TypeName = "date")]
		public System.DateTime? Date { get; set; }

		///<summary>
		/// datetime
		///</summary>
		[Column(@"datetime", TypeName = "datetime")]
		public System.DateTime Datetime { get; set; }

		///<summary>
		/// datetime2(7)
		///</summary>
		[Column(@"datetime2(7)", TypeName = "datetime2")]
		public System.DateTime? Datetime240741 { get; set; }

		///<summary>
		/// datetimeoffset(7)
		///</summary>
		[Column(@"datetimeoffset(7)", TypeName = "datetimeoffset")]
		public System.DateTimeOffset? Datetimeoffset40741 { get; set; }

		///<summary>
		/// float
		///</summary>
		[Column(@"float", TypeName = "float")]
		public double? @Float { get; set; }

		///<summary>
		/// money
		///</summary>
		[Column(@"money", TypeName = "money")]
		public decimal? Money { get; set; }

		///<summary>
		/// numeric(18, 6)
		///</summary>
		[Column(@"numeric(18, 6)", TypeName = "numeric")]
		public decimal? Numeric401844641 { get; set; }

		///<summary>
		/// real
		///</summary>
		[Column(@"real", TypeName = "real")]
		public float? Real { get; set; }

		///<summary>
		/// guid
		///</summary>
		[Column(@"guid", TypeName = "uniqueidentifier")]
		public System.Guid? Guid { get; set; }

		///<summary>
		/// xml
		///</summary>
		[Column(@"xml", TypeName = "xml")]
		public string Xml { get; set; }

		///<summary>
		/// bi (length: 50)
		///</summary>
		[Column(@"bi", TypeName = "varbinary(50)")]
		public byte[] Bi { get; set; }

    }
}
