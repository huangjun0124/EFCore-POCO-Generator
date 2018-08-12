using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("Server", Schema = "HangFire")]
    public class Server
    {
		///<summary>
		/// Id (Primary key) (length: 100)
		///</summary>
		[Column(@"Id", TypeName = "nvarchar(100)")]
		public string Id { get; set; }

		///<summary>
		/// Data
		///</summary>
		[Column(@"Data", TypeName = "nvarchar(max)")]
		public string Data { get; set; }

		///<summary>
		/// LastHeartbeat
		///</summary>
		[Column(@"LastHeartbeat", TypeName = "datetime")]
		public System.DateTime LastHeartbeat { get; set; }

    }
}
