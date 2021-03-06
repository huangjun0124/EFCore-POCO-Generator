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
    [Table("JobParamView", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class JobParamView
    {

        ///<summary>
        /// StateName (length: 20)
        ///</summary>
        [Column(@"StateName", Order = 1, TypeName = "nvarchar")]
        [MaxLength(20)]
        [StringLength(20)]
        [Display(Name = "State name")]
        public string StateName { get; set; }

        ///<summary>
        /// InvocationData (Primary key)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"InvocationData", Order = 2, TypeName = "nvarchar(max)")]
        [Required(AllowEmptyStrings = true)]
        [Key]
        [Display(Name = "Invocation data")]
        public string InvocationData { get; set; }

        ///<summary>
        /// Arguments (Primary key)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"Arguments", Order = 3, TypeName = "nvarchar(max)")]
        [Required(AllowEmptyStrings = true)]
        [Key]
        [Display(Name = "Arguments")]
        public string Arguments { get; set; }

        ///<summary>
        /// CreatedAt (Primary key)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"CreatedAt", Order = 4, TypeName = "datetime")]
        [Required]
        [Key]
        [DataType(DataType.DateTime)]
        [Display(Name = "Created at")]
        public System.DateTime CreatedAt { get; set; }

        ///<summary>
        /// ExpireAt
        ///</summary>
        [Column(@"ExpireAt", Order = 5, TypeName = "datetime")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Expire at")]
        public System.DateTime? ExpireAt { get; set; }

        ///<summary>
        /// ParamName (Primary key) (length: 40)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"ParamName", Order = 6, TypeName = "nvarchar")]
        [Required(AllowEmptyStrings = true)]
        [MaxLength(40)]
        [StringLength(40)]
        [Key]
        [Display(Name = "Param name")]
        public string ParamName { get; set; }

        ///<summary>
        /// ParamValue
        ///</summary>
        [Column(@"ParamValue", Order = 7, TypeName = "nvarchar(max)")]
        [Display(Name = "Param value")]
        public string ParamValue { get; set; }
    }

}
// </auto-generated>
