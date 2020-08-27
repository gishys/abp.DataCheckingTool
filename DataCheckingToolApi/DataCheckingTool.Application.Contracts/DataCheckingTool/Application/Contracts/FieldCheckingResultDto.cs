using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    public class FieldCheckingResultDto<T> : ICheckSign
    {
        public FieldCheckingResultDto(string name,string checkingDiscript,bool isPass)
        {
            Name = name;
            CheckingDiscript = checkingDiscript;
            IsPass = isPass;
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }
        /// <summary>
        /// 检查描述
        /// </summary>
        public string CheckingDiscript
        {
            get;
            protected set;
        }
        public T SourceObj
        {
            get;
            protected set;
        }
        public T CheckObj
        {
            get;
            protected set;
        }
        /// <summary>
        /// 错误数据数量
        /// </summary>
        public int ErrorDataCount
        {
            get;
            set;
        }
        /// <summary>
        /// 是否通过检查
        /// </summary>
        public bool IsPass
        {
            get;
            protected set;
        }
        /// <summary>
        /// 设置字段对象
        /// </summary>
        /// <param name="source"></param>
        /// <param name="check"></param>
        public void SetObj(T source, T check)
        {
            SourceObj = source;
            CheckObj = check;
        }
    }
}
