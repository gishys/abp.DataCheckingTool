using DataCheckingTool.Application.Contracts;
using DataCheckingTool.EntityFrameworkCore;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace DataCheckingTool.Application
{
    public class ErrorDataService: IErrorDataService,ITransientDependency
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
        public List<dynamic> GetFieldLengthErrorDataList(Field field)
        {
            var sql = SqlLength(field.TableName, field.Name, field.SelectFieldNames, field.CheckLength);
            return _dcToolDapperRepository.Query<dynamic>(Page(sql));
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
                     WHERE table_alias.rowno >= {(pageIndex - 1) * pageCount}";
        }
    }
}
