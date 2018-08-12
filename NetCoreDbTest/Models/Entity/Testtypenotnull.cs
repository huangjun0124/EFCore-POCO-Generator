using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("testtypenotnull", Schema = "dbo")]
    public class Testtypenotnull
    {
		///<summary>
		/// PKID (Primary key)
		///</summary>
		[Column(@"PKID", TypeName = "int")]
		public int Pkid { get; set; }

		///<summary>
		/// 1
		///</summary>
		[Column(@"1", TypeName = "bigint")]
		public long C1 { get; set; }

		///<summary>
		/// 2 (length: 500)
		///</summary>
		[Column(@"2", TypeName = "binary(500)")]
		public byte[] C2 { get; set; }

		///<summary>
		/// ddf
		///</summary>
		[Column(@"ddf", TypeName = "bit")]
		public bool Ddf { get; set; }

		///<summary>
		/// 34 (length: 1000)
		///</summary>
		[Column(@"34", TypeName = "char(1000)")]
		public string C34 { get; set; }

		///<summary>
		/// date
		///</summary>
		[Column(@"date", TypeName = "date")]
		public System.DateTime Date { get; set; }

		///<summary>
		/// dt
		///</summary>
		[Column(@"dt", TypeName = "datetime")]
		public System.DateTime Dt { get; set; }

		///<summary>
		/// dt2
		///</summary>
		[Column(@"dt2", TypeName = "datetime2")]
		public System.DateTime Dt2 { get; set; }

		///<summary>
		/// dec
		///</summary>
		[Column(@"dec", TypeName = "decimal")]
		public decimal Dec { get; set; }

		///<summary>
		/// flo
		///</summary>
		[Column(@"flo", TypeName = "float")]
		public double Flo { get; set; }

		///<summary>
		/// img (length: 2147483647)
		///</summary>
		[Column(@"img", TypeName = "image")]
		public byte[] Img { get; set; }

		///<summary>
		/// money
		///</summary>
		[Column(@"money", TypeName = "money")]
		public decimal Money { get; set; }

		///<summary>
		/// nc (length: 1000)
		///</summary>
		[Column(@"nc", TypeName = "nchar(1000)")]
		public string Nc { get; set; }

		///<summary>
		/// ntx (length: 1073741823)
		///</summary>
		[Column(@"ntx", TypeName = "ntext")]
		public string Ntx { get; set; }

		///<summary>
		/// numr
		///</summary>
		[Column(@"numr", TypeName = "numeric")]
		public decimal Numr { get; set; }

		///<summary>
		/// nvar (length: 50)
		///</summary>
		[Column(@"nvar", TypeName = "nvarchar(50)")]
		public string Nvar { get; set; }

		///<summary>
		/// mnax
		///</summary>
		[Column(@"mnax", TypeName = "nvarchar(max)")]
		public string Mnax { get; set; }

		///<summary>
		/// real
		///</summary>
		[Column(@"real", TypeName = "real")]
		public float Real { get; set; }

		///<summary>
		/// sd
		///</summary>
		[Column(@"sd", TypeName = "smalldatetime")]
		public System.DateTime Sd { get; set; }

		///<summary>
		/// si
		///</summary>
		[Column(@"si", TypeName = "smallint")]
		public short Si { get; set; }

		///<summary>
		/// sm
		///</summary>
		[Column(@"sm", TypeName = "smallmoney")]
		public decimal Sm { get; set; }

		///<summary>
		/// tex (length: 2147483647)
		///</summary>
		[Column(@"tex", TypeName = "text")]
		public string Tex { get; set; }

		///<summary>
		/// ti
		///</summary>
		[Column(@"ti", TypeName = "time")]
		public System.TimeSpan Ti { get; set; }

		///<summary>
		/// yin
		///</summary>
		[Column(@"yin", TypeName = "tinyint")]
		public byte Yin { get; set; }

		///<summary>
		/// uid
		///</summary>
		[Column(@"uid", TypeName = "uniqueidentifier")]
		public System.Guid Uid { get; set; }

		///<summary>
		/// vb (length: 50)
		///</summary>
		[Column(@"vb", TypeName = "varbinary(50)")]
		public byte[] Vb { get; set; }

		///<summary>
		/// vbm
		///</summary>
		[Column(@"vbm", TypeName = "varbinary(max)")]
		public byte[] Vbm { get; set; }

		///<summary>
		/// vc (length: 50)
		///</summary>
		[Column(@"vc", TypeName = "varchar(50)")]
		public string Vc { get; set; }

		///<summary>
		/// vm
		///</summary>
		[Column(@"vm", TypeName = "varchar(max)")]
		public string Vm { get; set; }

		///<summary>
		/// xml
		///</summary>
		[Column(@"xml", TypeName = "xml")]
		public string Xml { get; set; }

    }
}
