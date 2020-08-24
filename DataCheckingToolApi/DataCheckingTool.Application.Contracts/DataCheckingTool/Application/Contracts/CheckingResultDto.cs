using System.Collections.Generic;
using System.Linq;

namespace DataCheckingTool.Application.Contracts
{
    /// <summary>
    /// 检查结果集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CheckingResultDto<T> where T : ICheckSign
    {
        public CheckingResultDto(string name, string code, string level)
        {
            Name = name;
            Code = code;
            Level = level;
            ResultObjs = new List<T>();
        }
        /// <summary>
        /// 检查名字
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }
        /// <summary>
        /// 检查代码
        /// </summary>
        public string Code
        {
            get;
            protected set;
        }
        /// <summary>
        /// 检查级别
        /// </summary>
        public string Level
        {
            get;
            protected set;
        }
        /// <summary>
        /// 检查通过数量
        /// </summary>
        public int CheckNumberPass
        {
            get;
            protected set;
        }
        /// <summary>
        /// 检查未通过数量
        /// </summary>
        public int CheckNumberNotPass
        {
            get;
            protected set;
        }
        /// <summary>
        /// 检查结果集
        /// </summary>
        public List<T> ResultObjs
        {
            get;
            protected set;
        }
        /// <summary>
        /// 设置检查结果集合
        /// </summary>
        /// <param name="objs"></param>
        public void SetResultObjs(List<T> objs)
        {
            var checkObjs = objs as ICheckSign;
            CheckNumberPass = objs.Where(d => d.IsPass).Count();
            CheckNumberNotPass = objs.Where(d => !d.IsPass).Count();
            ResultObjs.AddRange(objs);
        }
    }
}
