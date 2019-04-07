using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Diagnostics;
using System.Linq;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public class DynamicComponentCollection : BlazorComponent
    {
        private bool _isInitialized = false;

        [Inject]
        private IViewModelRepository ViewModelRepository { get; set; }

        [Inject]
        [Parameter]
        protected IViewTypeSelector ViewModelTypeSelector { get; set; }

        [Parameter]
        protected IViewModelCollectionManager ViewModelCollectionManager { get; set; }

        private void ViewModelManager_ModelUpdated(object sender, EventArgs e)
        {
            var newModel = ViewModelCollectionManager.Current;
            Debug.WriteLine($"DynamicComponentCollection.ViewModelManager_ModelUpdated");

            if (_isInitialized)
            {
                StateHasChanged();
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;
            foreach(var viewModel in ViewModelCollectionManager.Current)
            {
                var viewType = ViewModelTypeSelector.SelectViewType(viewModel.GetType().Name); // ViewModelRepository.FindViewType(viewModel.GetType().Name);

                builder.OpenComponent(seq, viewType);
                builder.AddAttribute(++seq, "ViewModel", viewModel);
                builder.CloseComponent();

                Debug.WriteLine($"Created component with viewModel '{viewModel.GetType().Name}' and view '{viewType.Name}'");

                seq++;
            }

            base.BuildRenderTree(builder);
        }

        protected override void OnInit()
        {
            if (ViewModelCollectionManager == null || ViewModelCollectionManager.Current == null)
            {
                ViewModelCollectionManager = new ViewModelCollectionManager(Enumerable.Empty<IViewModel>());
            }

            _isInitialized = true;
        }
    }
}
