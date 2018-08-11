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

    // State
    [Table("State", Schema = "HangFire")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class HangFire_State
    {

        ///<summary>
        /// Id (Primary key)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"Id", Order = 1, TypeName = "int")]
        [Index(@"PK_HangFire_State", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        ///<summary>
        /// JobId
        ///</summary>
        [Column(@"JobId", Order = 2, TypeName = "int")]
        [Index(@"IX_HangFire_State_JobId", 1, IsUnique = false, IsClustered = false)]
        [Required]
        [Display(Name = "Job ID")]
        public int JobId { get; set; }

        ///<summary>
        /// Name (length: 20)
        ///</summary>
        [Column(@"Name", Order = 3, TypeName = "nvarchar")]
        [Required(AllowEmptyStrings = true)]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        ///<summary>
        /// Reason (length: 100)
        ///</summary>
        [Column(@"Reason", Order = 4, TypeName = "nvarchar")]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Reason")]
        public string Reason { get; set; }

        ///<summary>
        /// CreatedAt
        ///</summary>
        [Column(@"CreatedAt", Order = 5, TypeName = "datetime")]
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        public System.DateTime CreatedAt { get; set; }

        ///<summary>
        /// Data
        ///</summary>
        [Column(@"Data", Order = 6, TypeName = "nvarchar(max)")]
        [Display(Name = "Data")]
        public string Data { get; set; }

        // Foreign keys

        /// <summary>
        /// Parent HangFire_Job pointed by [State].([JobId]) (FK_HangFire_State_Job)
        /// </summary>
        [ForeignKey("JobId"), Required] public virtual HangFire_Job HangFire_Job { get; set; } // FK_HangFire_State_Job
    }

}
// </auto-generated>
