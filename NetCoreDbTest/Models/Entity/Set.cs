using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("Set", Schema = "HangFire")]
    public class Set
    {
		///<summary>
		/// Id (Primary key)
		///</summary>
		[Column(@"Id", TypeName = "int")]
		public int Id { get; set; }

		///<summary>
		/// Key (length: 100)
		///</summary>
		[Column(@"Key", TypeName = "nvarchar(100)")]
		public string Key { get; set; }

		///<summary>
		/// Score
		///</summary>
		[Column(@"Score", TypeName = "float")]
		public double Score { get; set; }

		///<summary>
		/// Value (length: 256)
		///</summary>
		[Column(@"Value", TypeName = "nvarchar(256)")]
		public string Value { get; set; }

		///<summary>
		/// ExpireAt
		///</summary>
		[Column(@"ExpireAt", TypeName = "datetime")]
		public System.DateTime? ExpireAt { get; set; }

    }
}
