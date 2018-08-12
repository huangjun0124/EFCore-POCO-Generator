using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("Job", Schema = "HangFire")]
    public class Job
    {
		///<summary>
		/// Id (Primary key). 主键
		///</summary>
		[Column(@"Id", TypeName = "int")]
		public int Id { get; set; }

		///<summary>
		/// StateId. 状态
		///</summary>
		[Column(@"StateId", TypeName = "int")]
		public int? StateId { get; set; }

		///<summary>
		/// StateName (length: 20). 状态名
		///</summary>
		[Column(@"StateName", TypeName = "nvarchar(20)")]
		public string StateName { get; set; }

		///<summary>
		/// InvocationData. 调用参数
		///</summary>
		[Column(@"InvocationData", TypeName = "nvarchar(max)")]
		public string InvocationData { get; set; }

		///<summary>
		/// Arguments
		///</summary>
		[Column(@"Arguments", TypeName = "nvarchar(max)")]
		public string Arguments { get; set; }

		///<summary>
		/// CreatedAt
		///</summary>
		[Column(@"CreatedAt", TypeName = "datetime")]
		public System.DateTime CreatedAt { get; set; }

		///<summary>
		/// ExpireAt. 过期时间
		///</summary>
		[Column(@"ExpireAt", TypeName = "datetime")]
		public System.DateTime? ExpireAt { get; set; }

    }
}
