using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("JobParameter", Schema = "HangFire")]
    public class JobParameter
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
		/// Name (length: 40)
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
