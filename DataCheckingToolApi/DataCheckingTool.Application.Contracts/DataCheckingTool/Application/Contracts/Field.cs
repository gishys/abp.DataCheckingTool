using System;
using System.Collections.Generic;
using System.Text;

namespace DataCheckingTool.Application.Contracts
{
    public class Field
    {
        public string Name
        {
            get;
            set;
        }
        public string FieldType
        {
            get;
            set;
        }
        public int FieldLength
        {
            get;
            set;
        }
        public int CheckLength
        {
            get;
            set;
        }
        public string TableName
        {
            get;
            set;
        }
        public string SelectFieldNames
        {
            get;
            set;
        }
        public bool IsKey
        {
            get;
            set;
        } = false;
        public bool IsIndex
        {
            get;
            set;
        } = false;
        public CheckValueDomainDto<dynamic> CVDomainDto
        {
            get;
            set;
        }
    }
}
