using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreDbTest
{
    [Table("Job",Schema = "HangFire")]
    public class JobClass
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column("Id", TypeName = "int")]
        public int JobId { get; set; }

        /// <summary>
        /// State Id
        /// </summary>
        [Column("StateId", TypeName = "int")]
        public int StateId { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [Column("StateName", TypeName = "nvarchar(20)")]
        public string StateName { get; set; }

        /// <summary>
        /// 调用参数
        /// </summary>
        [Column("InvocationData", TypeName = "nvarchar(max)")]
        public string InvocationData { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        [Column("Arguments", TypeName = "nvarchar(max)")]
        public string Arguments { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CreatedAt", TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        [Column("ExpireAt", TypeName = "datetime")]
        public DateTime? ExpireAt { get; set; }
    }
}
