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

    // Hash
    [Table("Hash", Schema = "HangFire")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.1.0")]
    public class HangFire_Hash
    {

        ///<summary>
        /// Id (Primary key)
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"Id", Order = 1, TypeName = "int")]
        [Index(@"PK_HangFire_Hash", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        ///<summary>
        /// Key (length: 100)
        ///</summary>
        [Column(@"Key", Order = 2, TypeName = "nvarchar")]
        [Index(@"UX_HangFire_Hash_Key_Field", 1, IsUnique = true, IsClustered = false)]
        [Index(@"IX_HangFire_Hash_Key", 1, IsUnique = false, IsClustered = false)]
        [Required(AllowEmptyStrings = true)]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Key")]
        public string Key { get; set; }

        ///<summary>
        /// Field (length: 100)
        ///</summary>
        [Column(@"Field", Order = 3, TypeName = "nvarchar")]
        [Index(@"UX_HangFire_Hash_Key_Field", 2, IsUnique = true, IsClustered = false)]
        [Required(AllowEmptyStrings = true)]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Field")]
        public string Field { get; set; }

        ///<summary>
        /// Value
        ///</summary>
        [Column(@"Value", Order = 4, TypeName = "nvarchar(max)")]
        [Display(Name = "Value")]
        public string Value { get; set; }

        ///<summary>
        /// ExpireAt
        ///</summary>
        [Column(@"ExpireAt", Order = 5, TypeName = "datetime2")]
        [Index(@"IX_HangFire_Hash_ExpireAt", 1, IsUnique = false, IsClustered = false)]
        [DataType(DataType.DateTime)]
        [Display(Name = "Expire at")]
        public System.DateTime? ExpireAt { get; set; }
    }

}
// </auto-generated>