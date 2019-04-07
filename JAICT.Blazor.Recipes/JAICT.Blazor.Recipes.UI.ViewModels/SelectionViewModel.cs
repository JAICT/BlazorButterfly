using JAICT.Blazor.Recipes.Entities;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.ViewModels
{

    public class SelectionViewModel : ViewModelBase 
    {
        protected IViewModelRepository ViewModelRepository { get; }
        public SelectionViewModel(IViewModelRepository viewModelRepository)
        {
            ViewModelRepository = viewModelRepository ?? throw new ArgumentNullException(nameof(viewModelRepository));
        }

        public void UpdateSelectionList(IEnumerable<SelectionItemViewModel> selection)
        {
            Debug.WriteLine("SelectionViewModel - Updating selection");
            Items.Update(selection);
        }

        public IViewModelCollectionManager Items { get; private set; } = new ViewModelCollectionManager();

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { _selectedIndex = value; Debug.WriteLine("New value bound " + value); }
        }        
    }

    public class SelectionViewModel<T> : SelectionViewModel where T : struct
    {
        public SelectionViewModel(IViewModelRepository viewModelRepository) : base(viewModelRepository)
        {

        }

        public void FillSelectionList(IEnumerable<Tuple<string, T>> itemList)
        {
            var index = 0;
            var selectionList = 
                itemList.Select((t) => 
                    ViewModelRepository.CreateViewModel<SelectionItemViewModel<T>>(
                        this, (si) => { si.Description = t.Item1; si.Value = t.Item2; si.Index = ++index; }))
                            .ToList();
            UpdateSelectionList(selectionList);
        }

        public T GetSelectedValue() 
        {
            var selectedItem = Items.Current.OfType<SelectionItemViewModel<T>>().Where(i => i.Index == SelectedIndex).SingleOrDefault();
            if (selectedItem!=null)
            {
                return selectedItem.Value;
            }
            return default;
        }
    }    
}
