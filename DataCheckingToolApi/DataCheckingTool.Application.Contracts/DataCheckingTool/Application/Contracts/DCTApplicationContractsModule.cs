using DataCheckingTool.Domain.Shared;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace DataCheckingTool.Application.Contracts
{
    [DependsOn(typeof(AbpDddApplicationModule))]
    [DependsOn(typeof(DCTDomainShareModule))]
    public class DCTApplicationContractsModule : AbpModule
    {
    }
}
