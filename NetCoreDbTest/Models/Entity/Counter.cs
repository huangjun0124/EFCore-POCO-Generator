using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("Counter", Schema = "HangFire")]
    public class Counter
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
		/// Value
		///</summary>
		[Column(@"Value", TypeName = "smallint")]
		public short Value { get; set; }

		///<summary>
		/// ExpireAt
		///</summary>
		[Column(@"ExpireAt", TypeName = "datetime")]
		public System.DateTime? ExpireAt { get; set; }

    }
}
