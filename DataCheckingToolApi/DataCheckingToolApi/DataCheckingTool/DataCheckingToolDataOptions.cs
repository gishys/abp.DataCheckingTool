using DataCheckingTool.Application.Contracts;
using System.Collections.Generic;

namespace DataCheckingToolApi.DataCheckingTool
{
    public class DataCheckingToolDataOptions
    {
        public TableStructureCheck TableStructureChecking
        {
            get;
            set;
        }
    }
    public class TableStructureCheck
    {
        public List<Table> Tables
        {
            get;
            set;
        }
    }
}