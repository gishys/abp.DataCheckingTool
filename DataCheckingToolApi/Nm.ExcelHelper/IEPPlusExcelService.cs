using System;
using System.Collections.Generic;
using System.Text;

namespace Nm.ExcelHelper
{
    public interface IEPPlusExcelService
    {
        void Export(List<dynamic> exportDataList);
    }
}
