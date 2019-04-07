using JAICT.Blazor.Recipes.Application;
using JAICT.Blazor.Recipes.UI.Client.Mvvm;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.View;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using JAICT.Blazor.Recipes.UI.Client.Views;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace JAICT.Blazor.Recipes.UI.Client
{
    public class Startup
    {
#pragma warning disable CS0169
        // Force view assembly to load so views can be found
        // could be done in the way the viewmodel assembly is loaded
        private readonly MainView _mv;
#pragma warning restore CS0169

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISessionStateManager, SessionStateManager>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<IViewModelRepository, ViewModelRepository>();
            services.AddSingleton<IViewRepository, ViewRepository>();
            services.AddSingleton<IViewTypeSelector, ViewTypeSelector>();

            services.AddTransient<IStateStorage, CookieStorage>();

            // Force viewmodel to load assembly to search
            var vmAssembly = "JAICT.Blazor.Recipes.UI.Client.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            foreach (var viewModelType in ViewModelRepository.GetViewModelTypes(new[] { vmAssembly }))
            {
                services.AddTransient(viewModelType);
            }  
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            // Possible to add a component of your choice here.
            // Note that a full htmlless component is still difficult to support 
            // given the fact that the Router needs a cshtml injectable property
            // to set the assembly to search when routing requests.......
            // app.AddComponent<App>("app");

            app.AddComponent<Main>("app");
        }
    }
}
