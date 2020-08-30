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
        public void Export(List<dynamic> exportDataList)
        {
            var path = @"D:\Test\Test.xlsx";
            FileInfo fileInfo = new FileInfo(path);
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(fileInfo))
            {
                var sheet = package.Workbook.Worksheets.Add("TestSheet");
                int rowIndex = 1;
                foreach (var entity in exportDataList)
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
                package.Save();
            }
        }
    }
}
