// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest
{

    // Job
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class HangFire_JobConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<HangFire_Job>
    {
        public HangFire_JobConfiguration()
            : this("HangFire")
        {
        }

        public HangFire_JobConfiguration(string schema)
        {
            Property(x => x.StateId).IsOptional();
            Property(x => x.StateName).IsOptional();
            Property(x => x.ExpireAt).IsOptional();
        }
    }

}
// </auto-generated>
