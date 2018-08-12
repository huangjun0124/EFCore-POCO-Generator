using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest.Entity
{
	// The table '<FakerPK>' is not usable by entity framework because it
	// does not have a primary key. It is listed here for completeness.
	[NotMapped]
    public class FakerPk
    {
		///<summary>
		/// FakeId
		///</summary>
		[Column(@"FakeId", TypeName = "int")]
		public int? FakeId { get; set; }

		///<summary>
		/// Plures (length: 10)
		///</summary>
		[Column(@"Plures", TypeName = "nchar(10)")]
		public string Plures { get; set; }

		///<summary>
		/// Fucker
		///</summary>
		[Column(@"Fucker", TypeName = "real")]
		public float? Fucker { get; set; }

		///<summary>
		/// Baker
		///</summary>
		[Column(@"Baker", TypeName = "uniqueidentifier")]
		public System.Guid? Baker { get; set; }

    }
}
