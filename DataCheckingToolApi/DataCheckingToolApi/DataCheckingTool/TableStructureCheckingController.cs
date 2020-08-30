using DataCheckingTool.Application;
using DataCheckingTool.Application.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nm.ExcelHelper;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.AspNetCore.Mvc;

namespace DataCheckingToolApi.DataCheckingTool
{
    [Route("DataCheckingTool/TableStructureChecking")]
    [ApiController]
    public class TableStructureCheckingController : AbpController
    {
        private readonly ITableStructureCheckingService _tscService;
        private readonly IOptions<DataCheckingToolDataOptions> _dctOptions;
        private readonly IErrorDataService _errorDataService;
        private readonly IEPPlusExcelService _ePPlusExcelService;
        public TableStructureCheckingController(
            ITableStructureCheckingService tscService,
            IOptions<DataCheckingToolDataOptions> dctOptions,
            IErrorDataService errorDataService,
            IEPPlusExcelService ePPlusExcelService)
        {
            _tscService = tscService;
            _dctOptions = dctOptions;
            _errorDataService = errorDataService;
            _ePPlusExcelService = ePPlusExcelService;
        }
        [HttpGet]
        [Route("Table/Checking")]
        public CheckingResultDtos Checking()
        {
            var tables = _dctOptions.Value?.TableStructureChecking?.Tables;
            if (tables?.Count > 0)
                return _tscService.Checking(tables);
            return null;
        }
        [HttpPost]
        [Route("Table/GetFieldLengthErrorData")]
        public List<dynamic> GetFieldLengthErrorData(TableSCheckErrorDataInput input)
        {
            var tables = _dctOptions.Value?.TableStructureChecking?.Tables;
            if (tables?.Count > 0)
            {
                var field = tables.FirstOrDefault(d => d.Name == input.TableName)?
                    .Fields?.FirstOrDefault(d => d.Name == input.FieldName);
                if (field != null)
                {
                    return _errorDataService.GetFieldLengthErrorData(field);
                }
            }
            return null;
        }
        [HttpPost]
        [Route("Table/GetValueDomainErrorData")]
        public void GetValueDomainErrorData(TableSCheckErrorDataInput input)
        {
            var tables = _dctOptions.Value?.TableStructureChecking?.Tables;
            if (tables?.Count > 0)
            {
                var field = tables.FirstOrDefault(d => d.Name == input.TableName)?
                    .Fields?.FirstOrDefault(d => d.Name == input.FieldName);
                if (field != null)
                {
                    var errorList =
                        _errorDataService.GetValueDomainErrorData(field, input.PageIndex, input.PageCount);
                    _ePPlusExcelService.Export(errorList);
                }
            }
        }
    }
}