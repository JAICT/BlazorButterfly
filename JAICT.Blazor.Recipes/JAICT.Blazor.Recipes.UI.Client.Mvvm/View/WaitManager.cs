using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client.Mvvm.View
{
    public static class WaitManager
    {
        public static async Task SetBusy(bool busy)
        {
            if (busy)
                await StartWait();
            else
                await EndWait();
        }
        public static async Task StartWait()
        {
            await JSRuntime.Current.InvokeAsync<bool>("startwait");
        }

        public static async Task EndWait()
        {
            await JSRuntime.Current.InvokeAsync<bool>("endwait");
        }
    }
}
