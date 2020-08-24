using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    /// <summary>
    /// 表结构检查结果
    /// </summary>
    public class TableCheckingResultDto : ICheckSign
    {
        public TableCheckingResultDto(string tableName, bool isPass)
        {
            TableName = tableName;
            IsPass = isPass;
        }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get;
            protected set;
        }
        /// <summary>
        /// 是否通过检查
        /// </summary>
        public bool IsPass
        {
            get;
            protected set;
        }
    }
}
