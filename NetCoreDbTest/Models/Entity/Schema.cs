using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	[Table("Schema", Schema = "HangFire")]
    public class Schema
    {
		///<summary>
		/// Version (Primary key)
		///</summary>
		[Column(@"Version", TypeName = "int")]
		public int Version { get; set; }

    }
}
