using System;
using System.Collections.Generic;
using System.Text;

namespace Nm.ExcelHelper
{
    public interface IEPPlusExcelService
    {
        void Export(IDictionary<string, List<dynamic>> dataList);
    }
}
