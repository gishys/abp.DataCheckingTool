using DataCheckingTool.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application
{
    public interface IErrorDataService
    {
        /// <summary>
        /// 检查数据错误
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        void CheckErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20);
        /// <summary>
        /// 获得字段长度错误数据列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        void GetFieldLengthErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20);
        /// <summary>
        /// 获取值域错误数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        void GetValueDomainErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20);
        /// <summary>
        /// 获得唯一值错误数据（重复）
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        void GetUniqueValueErrorData(string tableName, string fieldName, int pageIndex = 0, int pageCount = 20);
    }
}
