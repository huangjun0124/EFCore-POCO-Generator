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

    // List
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class HangFire_ListConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<HangFire_List>
    {
        public HangFire_ListConfiguration()
            : this("HangFire")
        {
        }

        public HangFire_ListConfiguration(string schema)
        {
            Property(x => x.Value).IsOptional();
            Property(x => x.ExpireAt).IsOptional();
        }
    }

}
// </auto-generated>
