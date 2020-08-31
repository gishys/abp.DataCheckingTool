using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace Nm.ExcelHelper
{
    public class EPPlusExcelService : IEPPlusExcelService, ITransientDependency
    {
        public void Export(IDictionary<string, List<dynamic>> dataList)
        {
            var path = @"D:\Test\Test.xlsx";
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(fileInfo))
            {
                foreach (var data in dataList)
                {
                    var sheet = package.Workbook.Worksheets.Add(data.Key);
                    int rowIndex = 1;
                    foreach (var entity in data.Value)
                    {
                        int columnIndex = 1;
                        foreach (var property in entity)
                        {
                            if (rowIndex == 1)
                            {
                                sheet.Cells[rowIndex, columnIndex].Value = property.Key;
                            }
                            sheet.Cells[rowIndex + 1, columnIndex].Value = property.Value;
                            columnIndex++;
                        }
                        rowIndex++;
                    }
                }
                package.Save();
            }
        }
    }
}
