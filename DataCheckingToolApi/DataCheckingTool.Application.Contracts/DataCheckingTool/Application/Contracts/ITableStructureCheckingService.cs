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
        CheckingResultDto<TableCheckingResultDto> CheckingTable(List<Table> tables);
        /// <summary>
        /// 检查表结构
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        CheckingResultDtos Checking(List<Table> tables);
        /// <summary>
        /// 检查表字段
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        CheckingResultDto<FieldCheckingResultDto<Field>> CheckingTableColumn(List<Table> tables);
    }
}
