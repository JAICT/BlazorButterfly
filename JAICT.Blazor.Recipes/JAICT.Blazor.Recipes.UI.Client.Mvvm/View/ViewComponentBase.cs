using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public class ViewComponentBase<T> : BlazorComponent, IView where T : class, IViewModel
    {
        [Parameter]
        protected T ViewModel { get; set;}
        
        protected override void OnInit()
        {
            Debug.WriteLine("ViewComponentBase.OnInit");
            if (ViewModel != null)
            {
                Debug.WriteLine($"Initializing {ViewModel.GetType().Name}");
                ViewModel.Initialize(WaitManager.SetBusy);
                Debug.WriteLine($"Initialization complete for {ViewModel.GetType().Name}");
            }
            else
                Debug.WriteLine("OnInit, no ViewModel");
        }

        protected async override Task OnInitAsync()
        {
            Debug.WriteLine("ViewComponentBase.OnInitAsync");
            if (ViewModel != null)
            {
                Debug.WriteLine($"Initializing {ViewModel.GetType().Name} async");
                await ViewModel.InitializeAsync(WaitManager.SetBusy);
                Debug.WriteLine($"Async initialization complete for {ViewModel.GetType().Name}");
            }
            else
                Debug.WriteLine("OnInitAsync, no ViewModel");
        }
    }
}
