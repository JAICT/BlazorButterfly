using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm
{
    public class ViewTypeSelector : IViewTypeSelector
    {
        private IViewModelRepository _viewModelRepository;

        public ViewTypeSelector(IViewModelRepository viewModelRepository)
        {
            _viewModelRepository = viewModelRepository ?? throw new ArgumentNullException(nameof(viewModelRepository));
        }

        public Type SelectViewType(string viewModelType, object selectionParameter = null)
        {
            return _viewModelRepository.FindViewType(viewModelType);
        }
    }
}
