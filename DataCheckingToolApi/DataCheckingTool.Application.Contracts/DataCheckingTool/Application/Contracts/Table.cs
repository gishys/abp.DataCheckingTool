using System.Collections.Generic;

namespace DataCheckingTool.Application.Contracts
{
    public class Table
    {
        public string Name
        {
            get;
            set;
        }
        public List<Field> Fields
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
