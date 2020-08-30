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
        List<dynamic> GetFieldLengthErrorDataList(Field field);
    }
}
