using DataCheckingTool.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public TableStructureCheckingController(
            ITableStructureCheckingService tscService,
            IOptions<DataCheckingToolDataOptions> dctOptions)
        {
            _tscService = tscService;
            _dctOptions = dctOptions;
        }
        [HttpGet]
        [Route("Table/CheckIsExist")]
        public CheckingResultDto<TableCheckingResultDto> CheckIsExist()
        {
            var tableNames = _dctOptions.Value?.TableStructureChecking?.Tables;
            if (tableNames?.Count > 0)
                return _tscService.CheckingTable(tableNames.Select(d => d.Name).ToList());
            return null;
        }
    }
}