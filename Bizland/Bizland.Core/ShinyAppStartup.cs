using Bizland.Core.Delegates;
using Microsoft.Extensions.DependencyInjection;
using Prism.Unity;
using Shiny;
using Shiny.Prism;
using System;
using System.Collections.Generic;
using System.Text;
using static Bizland.Core.Delegates.GpsListener;

namespace Bizland.Core
{
    public class ShinyAppStartup : PrismStartup
    {
        public ShinyAppStartup() : base(PrismContainerExtension.Current)
        {

        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGpsListener, GpsListener>();
            services.UseGps<LocationDelegate>();
        }
    }
}
