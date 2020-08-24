using DataCheckingTool.Application.Contracts;
using DataCheckingTool.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace DataCheckingTool.Application
{
    /// <summary>
    /// 表结构检查服务
    /// </summary>
    public class TableStructureCheckingService : ITableStructureCheckingService, ITransientDependency
    {
        private readonly DCToolDapperRepository _dcToolDapperRepository;
        public TableStructureCheckingService(DCToolDapperRepository dcToolDapperRepository)
        {
            _dcToolDapperRepository = dcToolDapperRepository;
        }
        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="tableNames">表名集合</param>
        /// <returns></returns>
        public CheckingResultDto<TableCheckingResultDto> CheckingTable(List<string> tableNames)
        {
            string userName = GlobalPara.DatabaseUserName();
            string tNames = "";
            foreach (var tName in tableNames)
            {
                tNames += string.Concat($",'{tName.ToUpper()}'");
            }
            string queryTable = $"SELECT TABLE_NAME FROM DBA_TABLES WHERE OWNER='{userName}' AND TABLE_NAME IN ({tNames.Substring(1)})";
            var existList = _dcToolDapperRepository.Query<string>(queryTable);
            var tscResultDtos = new List<TableCheckingResultDto>();
            foreach (var tableName in tableNames)
            {
                var tscResultDto = new TableCheckingResultDto(tableName, existList.Contains(tableName));
                tscResultDtos.Add(tscResultDto);
            }
            var ckResultDto = new CheckingResultDto<TableCheckingResultDto>("表格完整性检查", "001", "错误");
            ckResultDto.SetResultObjs(tscResultDtos);
            return ckResultDto;
        }
        /// <summary>
        /// 检查表字段是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public CheckingResultDto<FieldCheckingResultDto> CheckingTableColumn(Table table)
        {
            string userName = GlobalPara.DatabaseUserName();
            string fNames = "";
            var fList = table.Fields.Select(d => d.Name).ToList();
            foreach (var fName in fList)
            {
                fNames += string.Concat($",'{fName.ToUpper()}'");
            }
            string queryTable = $"SELECT COLUMN_NAME FROM DBA_TAB_COLUMNS WHERE OWNER='{userName}' AND TABLE_NAME='{table.Name}' AND COLUMN_NAME IN ({fNames.Substring(1)})";
            var existList = _dcToolDapperRepository.Query<string>(queryTable);
            var tscResultDtos = new List<FieldCheckingResultDto>();
            foreach (var tableName in fList)
            {
                var tscResultDto = new FieldCheckingResultDto(tableName, "表格字段是否存在", existList.Contains(tableName));
                tscResultDtos.Add(tscResultDto);
            }
            var ckResultDto = new CheckingResultDto<FieldCheckingResultDto>("表格完整性检查", "001", "错误");
            ckResultDto.SetResultObjs(tscResultDtos);
            return ckResultDto;
        }
    }
}
