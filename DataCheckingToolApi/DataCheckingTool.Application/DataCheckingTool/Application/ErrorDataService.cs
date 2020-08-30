using DataCheckingTool.Application.Contracts;
using DataCheckingTool.EntityFrameworkCore;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace DataCheckingTool.Application
{
    public class ErrorDataService : IErrorDataService, ITransientDependency
    {
        private readonly DCToolDapperRepository _dcToolDapperRepository;
        public ErrorDataService(DCToolDapperRepository dcToolDapperRepository)
        {
            _dcToolDapperRepository = dcToolDapperRepository;
        }
        /// <summary>
        /// 获得字段长度错误数据列表
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public List<dynamic> GetFieldLengthErrorData(Field field, int pageIndex = 0, int pageCount = 20)
        {
            var sql = SqlLength(field.TableName, field.Name, field.SelectFieldNames, field.CheckLength);
            sql = pageIndex == 0 ? sql : Page(sql, pageIndex, pageCount);
            return _dcToolDapperRepository.Query<dynamic>(sql);
        }
        /// <summary>
        /// 获取值域错误数据
        /// </summary>
        /// <param name="field"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<dynamic> GetValueDomainErrorData(Field field, int pageIndex = 0, int pageCount = 20)
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
                var sql = @$"select {field.SelectFieldNames} from {
                    field.TableName} a where a.{field.Name} not in({values.Substring(1)})";
                sql = pageIndex == 0 ? sql : Page(sql, pageIndex, pageCount);
                return _dcToolDapperRepository.Query<dynamic>(sql);
            }
            return null;
        }
        public string SqlLength(string tableName, string fieldName, string selectFieldNames, int fieldLength)
        {
            var selectFs = selectFieldNames.Contains(fieldName) ? selectFieldNames : $"{fieldName},{ selectFieldNames}";
            return $"SELECT {selectFs} FROM {tableName} WHERE length({fieldName})>{fieldLength}";
        }
        public string Page(string sql, int pageIndex = 1, int pageCount = 20)
        {
            return $@"SELECT *
                      FROM ({sql}
                               AND ROWNUM <= {pageIndex * pageCount}) table_alias
                     WHERE rownum >= {(pageIndex - 1) * pageCount}";
        }
    }
}
