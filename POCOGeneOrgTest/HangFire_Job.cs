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
    [Table("Job", Schema = "HangFire")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class HangFire_Job
    {

        ///<summary>
        /// Id (Primary key). 主键
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"Id", Order = 1, TypeName = "int")]
        [Index(@"PK_HangFire_Job", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        ///<summary>
        /// StateId. 状态
        ///</summary>
        [Column(@"StateId", Order = 2, TypeName = "int")]
        [Display(Name = "State ID")]
        public int? StateId { get; set; }

        ///<summary>
        /// StateName (length: 20). 状态名
        ///</summary>
        [Column(@"StateName", Order = 3, TypeName = "nvarchar")]
        [Index(@"IX_HangFire_Job_StateName", 1, IsUnique = false, IsClustered = false)]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "State name")]
        public string StateName { get; set; }

        ///<summary>
        /// InvocationData. 调用参数
        ///</summary>
        [Column(@"InvocationData", Order = 4, TypeName = "nvarchar(max)")]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Invocation data")]
        public string InvocationData { get; set; }

        ///<summary>
        /// Arguments
        ///</summary>
        [Column(@"Arguments", Order = 5, TypeName = "nvarchar(max)")]
        [Required(AllowEmptyStrings = true)]
        [Display(Name = "Arguments")]
        public string Arguments { get; set; }

        ///<summary>
        /// CreatedAt
        ///</summary>
        [Column(@"CreatedAt", Order = 6, TypeName = "datetime")]
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        public System.DateTime CreatedAt { get; set; }

        ///<summary>
        /// ExpireAt. 过期时间
        ///</summary>
        [Column(@"ExpireAt", Order = 7, TypeName = "datetime")]
        [Index(@"IX_HangFire_Job_ExpireAt", 1, IsUnique = false, IsClustered = false)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Expire at")]
        public System.DateTime? ExpireAt { get; set; }

        // Reverse navigation

        /// <summary>
        /// Child HangFire_JobParameters where [JobParameter].[JobId] point to this entity (FK_HangFire_JobParameter_Job)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<HangFire_JobParameter> HangFire_JobParameters { get; set; } = new System.Collections.Generic.List<HangFire_JobParameter>(); // JobParameter.FK_HangFire_JobParameter_Job
        /// <summary>
        /// Child HangFire_States where [State].[JobId] point to this entity (FK_HangFire_State_Job)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<HangFire_State> HangFire_States { get; set; } = new System.Collections.Generic.List<HangFire_State>(); // State.FK_HangFire_State_Job
    }

}
// </auto-generated>
