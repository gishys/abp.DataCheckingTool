using DataCheckingTool.Application.Contracts;
using DataCheckingTool.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nm.ExcelHelper;
using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace DataCheckingTool.Application
{
    public class ErrorDataService : IErrorDataService, ITransientDependency
    {
        private readonly DCToolDapperRepository _dcToolDapperRepository;
        private readonly IOptions<DataCheckingToolDataOptions> _dctOptions;
        private readonly IEPPlusExcelService _ePPlusExcelService;
        public ErrorDataService(
            DCToolDapperRepository dcToolDapperRepository,
            IOptions<DataCheckingToolDataOptions> dctOptions,
            IEPPlusExcelService ePPlusExcelService)
        {
            _dcToolDapperRepository = dcToolDapperRepository;
            _dctOptions = dctOptions;
            _ePPlusExcelService = ePPlusExcelService;
        }
        /// <summary>
        /// 检查数据错误
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        public void CheckErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20)
        {
            var tables = _dctOptions.Value?.TableStructureChecking?.Tables;
            var fields = tables.GetFields(tableName, fieldName);
            var dataList = new Dictionary<string, List<dynamic>>();
            if (fields != null)
            {
                foreach (var field in fields)
                {
                    var dataVD = GetValueDomainErrorData(field, pageIndex, pageCount);
                    if (dataVD?.Count > 0)
                        dataList.Add($"ValueNotDomain-{field.Name}", dataVD);

                    var dataUV = GetUniqueValueErrorData(field, pageIndex, pageCount);
                    if (dataUV?.Count > 0)
                        dataList.Add($"ValueRepeat-{field.Name}", dataUV);

                    var dataFL = GetFieldLengthErrorData(field, pageIndex, pageCount);
                    if (dataFL?.Count > 0)
                        dataList.Add($"ValueOverLimit-{field.Name}", dataFL);

                    var dataEmpty = GetEmptyData(field, pageIndex, pageCount);
                    if (dataEmpty?.Count > 0)
                        dataList.Add($"ValueEmpty-{field.Name}", dataEmpty);
                }
                if (dataList.Count > 0)
                    _ePPlusExcelService.Export(dataList);
            }
        }
        /// <summary>
        /// 获得字段长度错误数据列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        public void GetFieldLengthErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20)
        {
            GetErrorData(GetFieldLengthErrorData, tableName, fieldName, pageIndex, pageCount);
        }
        /// <summary>
        /// 获取值域错误数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        public void GetValueDomainErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20)
        {
            GetErrorData(GetValueDomainErrorData, tableName, fieldName, pageIndex, pageCount);
        }
        /// <summary>
        /// 获得唯一值错误数据（重复）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        public void GetUniqueValueErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20)
        {
            GetErrorData(GetUniqueValueErrorData, tableName, fieldName, pageIndex, pageCount);
        }

        #region private
        private string SqlLength(string tableName, string fieldName, string selectFieldNames, int fieldLength)
        {
            var selectFs = selectFieldNames.Contains(fieldName) ? selectFieldNames : $"{fieldName},{ selectFieldNames}";
            return $"SELECT {selectFs} FROM {tableName} WHERE length({fieldName})>{fieldLength}";
        }
        private string Page(string sql, int pageIndex = 1, int pageCount = 20)
        {
            sql = $"select * from({sql}) where 1=1 ";
            return $@"SELECT *
                      FROM ({sql}
                               AND ROWNUM <= {pageIndex * pageCount}) table_alias
                     WHERE rownum >= {(pageIndex - 1) * pageCount}";
        }
        private void GetErrorData(Func<Field, int, int, List<dynamic>> func,
            string tableName, string fieldName, int pageIndex = 0, int pageCount = 20)
        {
            var tables = _dctOptions.Value?.TableStructureChecking?.Tables;
            var fields = tables.GetFields(tableName, fieldName);
            var dataList = new Dictionary<string, List<dynamic>>();
            if (fields != null)
            {
                foreach (var field in fields)
                {
                    var data = func(field, pageIndex, pageCount);
                    if (data != null)
                        dataList.Add(field.Name, data);
                }
                if (dataList.Count > 0)
                    _ePPlusExcelService.Export(dataList);
            }
        }
        private List<dynamic> GetUniqueValueErrorData(Field field, int pageIndex = 0, int pageCount = 20)
        {
            if (field.UniqueValue)
            {
                var f = field.Name;
                var sql = $"SELECT A.{f},COUNT(A.{f}) SL FROM {field.TableName} A WHERE A.LIFECYCLE=0 OR A.LIFECYCLE IS NULL GROUP BY A.{f} HAVING COUNT(A.{f})>1";
                sql = pageIndex == 0 ? sql : Page(sql, pageIndex, pageCount);
                return _dcToolDapperRepository.Query<dynamic>(sql);
            }
            return null;
        }
        private List<dynamic> GetFieldLengthErrorData(Field field, int pageIndex = 0, int pageCount = 20)
        {
            if (field.CheckLength <= 0)
                return null;
            var sql = SqlLength(field.TableName, field.Name, field.SelectFieldNames, field.CheckLength);
            sql = pageIndex == 0 ? sql : Page(sql, pageIndex, pageCount);
            return _dcToolDapperRepository.Query<dynamic>(sql);
        }
        private List<dynamic> GetValueDomainErrorData(Field field, int pageIndex = 0, int pageCount = 20)
        {
            if (field.CVDomainDto != null)
            {
                if (field.CVDomainDto.CVDSource == CheckValueDomainSource.Database)
                {
                    field.CVDomainDto.Domain =
                        _dcToolDapperRepository.Query<string>(field.CVDomainDto.DomainDbSourceSql);
                }
                string values = "";
                foreach (var tName in field.CVDomainDto.Domain)
                {
                    values += string.Concat($",'{tName.ToUpper()}'");
                }
                var nullValue = values.ToUpper().Contains("NULL") && field.CanBeEmpty ? $" OR {field.Name} IS NULL" : "";
                var sql = @$"select {field.SelectFieldNames} from {
                    field.TableName} a where a.{field.Name} not in({values.Substring(1)}){nullValue}";
                sql = pageIndex == 0 ? sql : Page(sql, pageIndex, pageCount);
                return _dcToolDapperRepository.Query<dynamic>(sql);
            }
            return null;
        }
        private List<dynamic> GetEmptyData(Field field, int pageIndex = 0, int pageCount = 20)
        {
            if (!field.CanBeEmpty)
            {
                var sql = $"SELECT {field.SelectFieldNames} FROM {field.TableName} A WHERE A.{field.Name} IS NULL";
                sql = pageIndex == 0 ? sql : Page(sql, pageIndex, pageCount);
                return _dcToolDapperRepository.Query<dynamic>(sql);
            }
            return null;
        }
        #endregion
    }
}
