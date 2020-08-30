using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    public class TableSCheckErrorDataInput
    {
        /// <summary>
        /// get or set table name
        /// </summary>
        public string TableName
        {
            get;
            set;
        }
        /// <summary>
        /// get or set filed name
        /// </summary>
        public string FieldName
        {
            get;
            set;
        }
        public int PageIndex
        {
            get;
            set;
        }
        public int PageCount
        {
            get;
            set;
        } = 20;
    }
}
