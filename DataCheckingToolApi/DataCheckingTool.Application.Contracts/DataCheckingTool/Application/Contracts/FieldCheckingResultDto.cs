using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    public class FieldCheckingResultDto : ICheckSign
    {
        public FieldCheckingResultDto(string name,string checkingDiscript,bool isPass)
        {
            Name = name;
            CheckingDiscript = checkingDiscript;
            isPass = IsPass;
        }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 检查描述
        /// </summary>
        public string CheckingDiscript
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
    }
}
