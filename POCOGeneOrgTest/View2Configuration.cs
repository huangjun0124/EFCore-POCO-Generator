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

    // View2
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class View2Configuration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<View2>
    {
        public View2Configuration()
            : this("dbo")
        {
        }

        public View2Configuration(string schema)
        {
            Property(x => x.StateId).IsOptional();
            Property(x => x.StateName).IsOptional();
            Property(x => x.ExpireAt).IsOptional();
            Property(x => x.Value).IsOptional();
        }
    }

}
// </auto-generated>
