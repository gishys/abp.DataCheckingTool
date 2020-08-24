using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace DataCheckingTool.Application.Contracts
{
    public class GlobalPara
    {
        public static string DatabaseUserName()
        {
            var builder = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource
                {
                    Path = "appsettings.json",
                    Optional = false,
                    ReloadOnChange = true
                }).Build();
            return builder["ConnectionStrings:DCToolDatabase"].Split('=')[1].Split(';')[0].ToUpper();
        }
    }
}
