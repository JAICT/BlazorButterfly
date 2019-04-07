using JAICT.Blazor.Recipes.UI.Client.Mvvm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.ViewModel
{
    public class ViewModelManagerBase<T> where T : class
    {
        protected static EventArgs Args = new EventArgs();

        public ViewModelManagerBase(T initialModels)
        {
            Current = initialModels ?? throw new ArgumentNullException(nameof(initialModels));
        }

        public T Current { get; private set; }

        public event EventHandler Updated;

        public void Update(T newModel)
        {
            Current = newModel;
            Updated?.Invoke(this, Args);
        }
    }

    public class ViewModelManager : ViewModelManagerBase<IViewModel>, IViewModelManager
    {
        public ViewModelManager(IViewModel intialModels) : base(intialModels) { }
    }

    public class ViewModelManager<T> : ViewModelManagerBase<IViewModel>, IViewModelManager where T : class, IViewModel
    {
        public ViewModelManager(IViewModel intialModels) : base(intialModels) { }

        public T CurrentTyped {  get { return Current as T; } }
    }

    public class ViewModelCollectionManager : ViewModelManagerBase<IEnumerable<IViewModel>>, IViewModelCollectionManager
    {
        public ViewModelCollectionManager() : base(Enumerable.Empty<IViewModel>()) { }
        public ViewModelCollectionManager(IEnumerable<IViewModel> intialModels) : base(intialModels) { }
    }

}
