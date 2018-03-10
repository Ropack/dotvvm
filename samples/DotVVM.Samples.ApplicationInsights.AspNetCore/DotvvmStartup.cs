using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Tracing.ApplicationInsights;
using DotVVM.Tracing.ApplicationInsights.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DotVVM.Samples.ApplicationInsights.AspNetCore
{
    public class DotvvmStartup : IDotvvmStartup, IDotvvmServiceConfigurator
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            ConfigureRoutes(config, applicationPath);
            ConfigureControls(config, applicationPath);
            ConfigureResources(config, applicationPath);
        }

        private void ConfigureRoutes(DotvvmConfiguration config, string applicationPath)
        {
            config.RouteTable.Add("Default", "", "Views/default.dothtml");

            config.RouteTable.Add("InitException", "Test/InitException", "Views/Test/initException.dothtml");
            config.RouteTable.Add("CommandException", "Test/CommandException", "Views/Test/commandException.dothtml");
            config.RouteTable.Add("Correct", "Test/Correct", "Views/Test/correct.dothtml");
            config.RouteTable.Add("CorrectCommand", "Test/CorrectCommand", "Views/Test/correctCommand.dothtml");
            // Uncomment the following line to auto-register all dothtml files in the Views folder
            // config.RouteTable.AutoDiscoverRoutes(new DefaultRouteStrategy(config));    
        }

        private void ConfigureControls(DotvvmConfiguration config, string applicationPath)
        {
            // register code-only controls and markup controls
        }

        private void ConfigureResources(DotvvmConfiguration config, string applicationPath)
        {
            // register custom resources and adjust paths to the built-in resources
        }

        public void ConfigureServices(IDotvvmServiceCollection serviceCollection)
        {
            serviceCollection
                .AddDefaultTempStorages("Temp")
                .AddApplicationInsightsTracing();

        }
    }
}
