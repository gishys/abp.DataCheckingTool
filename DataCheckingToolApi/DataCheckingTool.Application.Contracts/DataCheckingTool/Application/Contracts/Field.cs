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
        public string Type
        {
            get;
            set;
        }
        public int Length
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
    }
}
