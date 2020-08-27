using DataCheckingTool.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application
{
    public interface IFieldErrorRulesService
    {
        /// <summary>
        /// 字段是否存在
        /// </summary>
        /// <param name="table">配置集合</param>
        /// <returns></returns>
        List<FieldCheckingResultDto<Field>> FieldsExist(Table table);
        /// <summary>
        /// 索引是否存在
        /// </summary>
        /// <param name="table">配置集合</param>
        /// <returns></returns>
        List<FieldCheckingResultDto<Field>> FieldsIndexExist(Table table);
        /// <summary>
        /// 字段及值长度是否符合要求
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        List<FieldCheckingResultDto<Field>> FieldValueLengthCheck(Table table);
    }
}
