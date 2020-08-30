using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    /// <summary>
    /// 值域检查配置
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CheckValueDomainDto<T>
    {
        public CheckValueDomainDto()
        { }
        public CheckValueDomainDto(List<string> domain)
        {
            Domain = domain;
            CVDType = CheckValueDomainType.Group;
        }
        public CheckValueDomainDto(T minValue, T maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            CVDType = CheckValueDomainType.Range;
        }
        /// <summary>
        /// 值域类型
        /// </summary>
        public CheckValueDomainType CVDType
        {
            get;
            set;
        }
        /// <summary>
        /// 数据来源
        /// </summary>
        public CheckValueDomainSource CVDSource
        {
            get;
            set;
        }
        /// <summary>
        /// 域数据库来源sql语句
        /// </summary>
        public string DomainDbSourceSql
        {
            get;
            set;
        }
        /// <summary>
        /// 最小值
        /// </summary>
        public T MinValue
        {
            get;
            set;
        }
        /// <summary>
        /// 最大值
        /// </summary>
        public T MaxValue
        {
            get;
            set;
        }
        /// <summary>
        /// 一组值
        /// </summary>
        public List<string> Domain
        {
            get;
            set;
        }
    }
    public enum CheckValueDomainType
    {
        /// <summary>
        /// 域为一个范围
        /// </summary>
        Range = 0,
        /// <summary>
        /// 域为一组数据
        /// </summary>
        Group = 1
    }
    public enum CheckValueDomainSource
    {
        /// <summary>
        /// 来源为xml文件
        /// </summary>
        Xml = 0,
        /// <summary>
        /// 来源为数据库
        /// </summary>
        Database = 1
    }
}
