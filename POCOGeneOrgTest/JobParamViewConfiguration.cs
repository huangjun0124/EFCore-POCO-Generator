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

    // JobParamView
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class JobParamViewConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<JobParamView>
    {
        public JobParamViewConfiguration()
            : this("dbo")
        {
        }

        public JobParamViewConfiguration(string schema)
        {
            Property(x => x.StateName).IsOptional();
            Property(x => x.ExpireAt).IsOptional();
            Property(x => x.ParamValue).IsOptional();
        }
    }

}
// </auto-generated>
