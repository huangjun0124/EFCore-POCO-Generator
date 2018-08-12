using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("JobParamView", Schema = "dbo")]
    public class JobParamView
    {
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
		/// ParamName (Primary key) (length: 40)
		///</summary>
		[Column(@"ParamName", TypeName = "nvarchar(40)")]
		public string ParamName { get; set; }

		///<summary>
		/// ParamValue
		///</summary>
		[Column(@"ParamValue", TypeName = "nvarchar(max)")]
		public string ParamValue { get; set; }

    }
}
