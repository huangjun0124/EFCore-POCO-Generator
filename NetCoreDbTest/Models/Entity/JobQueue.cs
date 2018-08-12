using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("JobQueue", Schema = "HangFire")]
    public class JobQueue
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
		/// Queue (length: 50)
		///</summary>
		[Column(@"Queue", TypeName = "nvarchar(50)")]
		public string Queue { get; set; }

		///<summary>
		/// FetchedAt
		///</summary>
		[Column(@"FetchedAt", TypeName = "datetime")]
		public System.DateTime? FetchedAt { get; set; }

    }
}
