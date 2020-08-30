using DataCheckingTool.Application.Contracts;
using DataCheckingTool.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace DataCheckingTool.Application
{
    public class FieldErrorRulesService : IFieldErrorRulesService, ITransientDependency
    {
        private readonly DCToolDapperRepository _dcToolDapperRepository;
        private List<Field> _fields = null;
        public FieldErrorRulesService(DCToolDapperRepository dcToolDapperRepository)
        {
            _dcToolDapperRepository = dcToolDapperRepository;
        }
        public List<Field> Init()
        {
            string userName = GlobalPara.DatabaseUserName();
            string queryTable = @$"SELECT A.COLUMN_NAME AS Name,
                                           A.DATA_TYPE AS FieldType,
                                           A.DATA_LENGTH AS FieldLength,
                                           A.TABLE_NAME AS TableName,
                                           NVL2(B.INDEX_NAME, 1, 0) AS IsIndex
                                      FROM DBA_TAB_COLUMNS A
                                      LEFT JOIN DBA_IND_COLUMNS B
                                        ON A.COLUMN_NAME = B.COLUMN_NAME
                                       AND A.TABLE_NAME = B.TABLE_NAME
                                       AND A.OWNER = B.TABLE_OWNER
                                 WHERE 
                                   A.OWNER = '{userName}'";
            return _dcToolDapperRepository.Query<Field>(queryTable);
        }
        public List<FieldCheckingResultDto<Field>> FieldsExist(Table table)
        {
            _fields = _fields == null ? Init() : _fields;
            var sourceFieldList = _fields.Where(d => d.TableName == table.Name).ToList();
            var tscResultDtos = new List<FieldCheckingResultDto<Field>>();
            foreach (var fieldName in table.Fields.Select(d => d.Name))
            {
                var tscResultDto = new FieldCheckingResultDto<Field>(fieldName, "字段是否存在",
                    sourceFieldList?.Count > 0 ? sourceFieldList.Select(d => d.Name).Contains(fieldName) : false);
                tscResultDtos.Add(tscResultDto);
            }
            return tscResultDtos;
        }
        public List<FieldCheckingResultDto<Field>> FieldsIndexExist(Table table)
        {
            _fields = _fields == null ? Init() : _fields;
            var sourceFieldList = _fields.Where(d => d.TableName == table.Name).ToList();
            var tscResultDtos = new List<FieldCheckingResultDto<Field>>();
            foreach (var field in table.Fields.Where(d => d.IsIndex))
            {
                var tscResultDto = new FieldCheckingResultDto<Field>(field.Name, "字段是否存在索引",
                    sourceFieldList?.Count > 0 ? sourceFieldList.Select(d => d.Name).Contains(field.Name) : false);
                tscResultDtos.Add(tscResultDto);
            }
            return tscResultDtos;
        }
        public List<FieldCheckingResultDto<Field>> FieldValueLengthCheck(Table table)
        {
            _fields = _fields == null ? Init() : _fields;
            var sourceFieldList = _fields.Where(d => d.TableName == table.Name).ToList();
            var tscResultDtos = new List<FieldCheckingResultDto<Field>>();
            foreach (var field in table.Fields)
            {
                var tempField = sourceFieldList.Where(d => d.Name == field.Name).FirstOrDefault();
                if (field.FieldLength > (tempField != null ? tempField.FieldLength : -1))
                {
                    var errorDataCount = _dcToolDapperRepository.Query<int>(
                        $"SELECT COUNT(*) FROM {field.TableName} WHERE LENGTH({field.Name})>{field.CheckLength}").FirstOrDefault();
                    if (errorDataCount > 0)
                    {
                        var tscResultDto = new FieldCheckingResultDto<Field>(field.Name, "字段长度及内容超过目标字段长度",
                            sourceFieldList?.Count > 0 ? sourceFieldList.Select(d => d.Name).Contains(field.Name) : false);
                        tscResultDto.ErrorDataCount = errorDataCount;
                        tscResultDtos.Add(tscResultDto);
                    }
                }
            }
            return tscResultDtos;
        }
    }
}
