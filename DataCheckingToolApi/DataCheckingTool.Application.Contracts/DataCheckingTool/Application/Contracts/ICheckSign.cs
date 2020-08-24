using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    /// <summary>
    /// 检查是否通过标记
    /// </summary>
    public interface ICheckSign
    {
        /// <summary>
        /// 检查是否通过
        /// </summary>
        bool IsPass
        {
            get;
        }
    }
}
