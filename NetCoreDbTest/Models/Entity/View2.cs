using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("View2", Schema = "dbo")]
    public class View2
    {
		///<summary>
		/// Id (Primary key)
		///</summary>
		[Column(@"Id", TypeName = "int")]
		public int Id { get; set; }

		///<summary>
		/// StateId
		///</summary>
		[Column(@"StateId", TypeName = "int")]
		public int? StateId { get; set; }

		///<summary>
		/// StateName (length: 20)
		///</summary>
		[Column(@"StateName", TypeName = "nvarchar(20)")]
		public string StateName { get; set; }

		///<summary>
		/// InvocationData (Primary key)
		///</summary>
		[Column(@"InvocationData", TypeName = "nvarchar(max)")]
		public string InvocationData { get; set; }

		///<summary>
		/// Arguments (Primary key)
		///</summary>
		[Column(@"Arguments", TypeName = "nvarchar(max)")]
		public string Arguments { get; set; }

		///<summary>
		/// CreatedAt (Primary key)
		///</summary>
		[Column(@"CreatedAt", TypeName = "datetime")]
		public System.DateTime CreatedAt { get; set; }

		///<summary>
		/// ExpireAt
		///</summary>
		[Column(@"ExpireAt", TypeName = "datetime")]
		public System.DateTime? ExpireAt { get; set; }

		///<summary>
		/// Expr1 (Primary key)
		///</summary>
		[Column(@"Expr1", TypeName = "int")]
		public int Expr1 { get; set; }

		///<summary>
		/// JobId (Primary key)
		///</summary>
		[Column(@"JobId", TypeName = "int")]
		public int JobId { get; set; }

		///<summary>
		/// Name (Primary key) (length: 40)
		///</summary>
		[Column(@"Name", TypeName = "nvarchar(40)")]
		public string Name { get; set; }

		///<summary>
		/// Value
		///</summary>
		[Column(@"Value", TypeName = "nvarchar(max)")]
		public string Value { get; set; }

    }
}
