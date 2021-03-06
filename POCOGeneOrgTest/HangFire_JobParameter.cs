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

    // JobParameter
    [Table("JobParameter", Schema = "HangFire")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class HangFire_JobParameter
    {

        ///<summary>
        /// Id (Primary key)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"Id", Order = 1, TypeName = "int")]
        [Index(@"PK_HangFire_JobParameter", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        ///<summary>
        /// JobId
        ///</summary>
        [Column(@"JobId", Order = 2, TypeName = "int")]
        [Index(@"IX_HangFire_JobParameter_JobIdAndName", 1, IsUnique = false, IsClustered = false)]
        [Required]
        [Display(Name = "Job ID")]
        public int JobId { get; set; }

        ///<summary>
        /// Name (length: 40)
        ///</summary>
        [Column(@"Name", Order = 3, TypeName = "nvarchar")]
        [Index(@"IX_HangFire_JobParameter_JobIdAndName", 2, IsUnique = false, IsClustered = false)]
        [Required(AllowEmptyStrings = true)]
        [MaxLength(40)]
        [StringLength(40)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        ///<summary>
        /// Value
        ///</summary>
        [Column(@"Value", Order = 4, TypeName = "nvarchar(max)")]
        [Display(Name = "Value")]
        public string Value { get; set; }

        // Foreign keys

        /// <summary>
        /// Parent HangFire_Job pointed by [JobParameter].([JobId]) (FK_HangFire_JobParameter_Job)
        /// </summary>
        [ForeignKey("JobId"), Required] public virtual HangFire_Job HangFire_Job { get; set; } // FK_HangFire_JobParameter_Job
    }

}
// </auto-generated>
