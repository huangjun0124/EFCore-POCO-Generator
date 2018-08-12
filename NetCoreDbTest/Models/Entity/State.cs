using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("State", Schema = "HangFire")]
    public class State
    {
		///<summary>
		/// Id (Primary key)
		///</summary>
		[Column(@"Id", TypeName = "int")]
		public int Id { get; set; }

		///<summary>
		/// JobId
		///</summary>
		[Column(@"JobId", TypeName = "int")]
		public int JobId { get; set; }

		///<summary>
		/// Name (length: 20)
		///</summary>
		[Column(@"Name", TypeName = "nvarchar(20)")]
		public string Name { get; set; }

		///<summary>
		/// Reason (length: 100)
		///</summary>
		[Column(@"Reason", TypeName = "nvarchar(100)")]
		public string Reason { get; set; }

		///<summary>
		/// CreatedAt
		///</summary>
		[Column(@"CreatedAt", TypeName = "datetime")]
		public System.DateTime CreatedAt { get; set; }

		///<summary>
		/// Data
		///</summary>
		[Column(@"Data", TypeName = "nvarchar(max)")]
		public string Data { get; set; }

    }
}
