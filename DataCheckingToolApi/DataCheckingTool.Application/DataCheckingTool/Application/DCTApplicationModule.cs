using DataCheckingTool.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Nm.ExcelHelper;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace DataCheckingTool.Application
{
    [DependsOn(typeof(DCTApplicationContractsModule))]
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(NmExcelHelperModule))]
    public class DCTApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<DCTApplicationModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<DataCheckingToolMapperProfile>(validate: true);
            });
        }
    }
}
