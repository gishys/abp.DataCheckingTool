using DataCheckingTool.Application;
using DataCheckingTool.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public TableStructureCheckingController(
            ITableStructureCheckingService tscService,
            IOptions<DataCheckingToolDataOptions> dctOptions,
            IErrorDataService errorDataService)
        {
            _tscService = tscService;
            _dctOptions = dctOptions;
            _errorDataService = errorDataService;
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
        public void GetFieldLengthErrorData(TableSCheckErrorDataInput input)
        {
            _errorDataService.GetFieldLengthErrorData(
                input.TableName, input.FieldName, input.PageIndex, input.PageCount);
        }
        [HttpPost]
        [Route("Table/CheckErrorData")]
        public void CheckErrorData(TableSCheckErrorDataInput input)
        {
            _errorDataService.CheckErrorData(
                input.TableName, input.FieldName, input.PageIndex, input.PageCount);
        }
        [HttpPost]
        [Route("Table/GetValueDomainErrorData")]
        public void GetValueDomainErrorData(TableSCheckErrorDataInput input)
        {
            _errorDataService.GetValueDomainErrorData(
                input.TableName, input.FieldName, input.PageIndex, input.PageCount);
        }
        [HttpPost]
        [Route("Table/GetUniqueValueErrorData")]
        public void GetUniqueValueErrorData(TableSCheckErrorDataInput input)
        {
            _errorDataService.GetUniqueValueErrorData(
                input.TableName, input.FieldName, input.PageIndex, input.PageCount);
        }
    }
}