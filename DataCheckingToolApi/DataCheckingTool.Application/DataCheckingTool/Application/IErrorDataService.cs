using DataCheckingTool.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application
{
    public interface IErrorDataService
    {
        /// <summary>
        /// 获得字段长度错误数据列表
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        List<dynamic> GetFieldLengthErrorData(Field field, int pageIndex = 1, int pageCount = 20);
        /// <summary>
        /// 获取值域错误数据
        /// </summary>
        /// <param name="field"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        List<dynamic> GetValueDomainErrorData(Field field, int pageIndex = 0, int pageCount = 20);
    }
}
