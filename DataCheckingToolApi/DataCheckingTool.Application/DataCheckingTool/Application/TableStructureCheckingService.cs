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
        private readonly IFieldErrorRulesService _ferService;
        public TableStructureCheckingService(
            DCToolDapperRepository dcToolDapperRepository,
            IFieldErrorRulesService ferService)
        {
            _dcToolDapperRepository = dcToolDapperRepository;
            _ferService = ferService;
        }
        /// <summary>
        /// 检查表结构
        /// </summary>
        /// <param name="tables"></param>
        /// <returns></returns>
        public CheckingResultDtos Checking(List<Table> tables)
        {
            var resultList = new CheckingResultDtos();
            resultList.TableCheck = CheckingTable(tables);
            resultList.FieldCheck = CheckingTableColumn(tables);
            return resultList;
        }
        /// <summary>
        /// 检查表是否存在
        /// </summary>
        /// <param name="tableNames">表名集合</param>
        /// <returns></returns>
        public CheckingResultDto<TableCheckingResultDto> CheckingTable(List<Table> tables)
        {
            var tableNames = tables.Select(d => d.Name).ToList();
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
            var ckResultDto = new CheckingResultDto<TableCheckingResultDto>("表格符合性检查", "001", "错误");
            ckResultDto.SetResultObjs(tscResultDtos);
            return ckResultDto;
        }
        /// <summary>
        /// 检查表字段
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public CheckingResultDto<FieldCheckingResultDto<Field>> CheckingTableColumn(List<Table> tables)
        {
            var tscResultDtos = new List<FieldCheckingResultDto<Field>>();
            foreach (var table in tables)
            {
                tscResultDtos.AddRange(_ferService.FieldsExist(table));
                tscResultDtos.AddRange(_ferService.FieldsIndexExist(table));
                tscResultDtos.AddRange(_ferService.FieldValueLengthCheck(table));
            }
            var ckResultDto = new CheckingResultDto<FieldCheckingResultDto<Field>>("字段符合性检查", "002", "错误");
            ckResultDto.SetResultObjs(tscResultDtos);
            return ckResultDto;
        }
    }
}
