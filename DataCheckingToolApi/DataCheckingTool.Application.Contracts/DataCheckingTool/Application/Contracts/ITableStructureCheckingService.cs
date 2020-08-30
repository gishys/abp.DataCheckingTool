using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    /// <summary>
    /// 表结构检查服务
    /// </summary>
    public interface ITableStructureCheckingService
    {
        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="tableNames">表名集合</param>
        /// <returns></returns>
        CheckingResultDto<TableCheckingResultDto> CheckingTable(List<string> tableNames);
    }
}
