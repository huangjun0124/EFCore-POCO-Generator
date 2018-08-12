using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("Hash", Schema = "HangFire")]
    public class Hash
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
		/// Field (length: 100)
		///</summary>
		[Column(@"Field", TypeName = "nvarchar(100)")]
		public string Field { get; set; }

		///<summary>
		/// Value
		///</summary>
		[Column(@"Value", TypeName = "nvarchar(max)")]
		public string Value { get; set; }

		///<summary>
		/// ExpireAt
		///</summary>
		[Column(@"ExpireAt", TypeName = "datetime2")]
		public System.DateTime? ExpireAt { get; set; }

    }
}
