using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;
using System;
using System.Diagnostics;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public class DynamicComponent : BlazorComponent
    {
        private bool _isInitialized = false;

        /// <summary>
        /// The viewmodel manager that is associated with this component
        /// </summary>
        private IViewModelManager _viewModelManager;

        /// <summary>
        /// The view model repository used for creating new ViewModels and looking up view types
        /// </summary>
        [Inject]
        private IViewModelRepository ViewModelRepository { get; set; }

        [Inject]
        [Parameter]
        protected IViewTypeSelector ViewModelTypeSelector { get;set; }

        /// <summary>
        /// View Model Type Name
        /// </summary>
        [Parameter]
        protected string ViewModelType { get; set; }

        [Parameter]
        protected IViewModelManager ViewModelManager
        {
            get
            {
                return _viewModelManager;
            }
            set
            {
                if (_viewModelManager != null)
                {
                    // Release any previous view model managers.
                    _viewModelManager.Updated -= ViewModelManager_ModelUpdated;
                }
                _viewModelManager = value ?? throw new ArgumentNullException(nameof(value));
                _viewModelManager.Updated += ViewModelManager_ModelUpdated;
                
            }
        }

        private void ViewModelManager_ModelUpdated(object sender, EventArgs e)
        {
            Debug.WriteLine($"ViewModelManager_ModelUpdated to {ViewModelManager.Current.GetType()}");

            // Reset the viewmodel type information because we need to override this with
            // the type from the new view model
            ViewModelType = null;
            UpdateTypeInformation();
            if (_isInitialized)
            {
                // Only call state changed when initialized.
                StateHasChanged();
            }
        }

        private void UpdateTypeInformation()
        {
            // Resolve ViewModel Type
            if (string.IsNullOrWhiteSpace(ViewModelType))
                ViewModelType = ViewModelManager.Current.GetType().Name;

            ViewType = ViewModelTypeSelector.SelectViewType(ViewModelType);            

            Debug.WriteLine($"Updated Type Information to ViewModel: {ViewModelType} , View : {ViewType.Name}");
        }

        private Type ViewType { get; set; }

        protected override void OnInit()
        {
            // Update type information before rendering
            UpdateTypeInformation();

            // Create ViewModel if not yet created.
            if (ViewModelManager == null || ViewModelManager.Current == null)
            {
                Debug.WriteLine("Creating new view model for type " + ViewModelType);                
                ViewModelManager = new ViewModelManager(ViewModelRepository.CreateViewModel(ViewModelType));
            }

            _isInitialized = true;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var seq = 0;

            builder.OpenComponent(seq, ViewType);
            builder.AddAttribute(++seq, "ViewModel", ViewModelManager.Current);

            Debug.WriteLine($"BuildRenderTree - Created component with viewModel '{ViewModelType}' and view '{ViewType.Name}'");

            builder.CloseComponent();

            base.BuildRenderTree(builder);
        }
    }
}
