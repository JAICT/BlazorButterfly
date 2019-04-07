using JAICT.Blazor.Recipes.Application;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.UI.Client
{
    public class CookieStorage : IStateStorage
    {
        public async Task<T> Get<T>(string key) where T : class
        {
            Debug.WriteLine("CookieStorage.Get");
            var exists =  await JSRuntime.Current.InvokeAsync<bool>("cookieExists", key);
            if (exists)
            {
                Debug.WriteLine("CookieStorage.Get, ookie exists getting cookie");
                var result = await JSRuntime.Current.InvokeAsync<string>("getCookie", key);
                return JsonConvert.DeserializeObject<T>(result);
            }
            Debug.WriteLine("CookieStorage.Get - No cookie");
            return null;
        }

        public async Task Store<T>(T state, string key) where T :class
        {
            Debug.WriteLine("CookieStorage.Store");

            var st = JsonConvert.SerializeObject(state);
            Debug.WriteLine(st);

            var result = await JSRuntime.Current.InvokeAsync<bool>("setCookie", key, st);
            if (!result)
                throw new InvalidOperationException("CookieStorage.Store failure!");

            Debug.WriteLine("CookieStorage.Store succeeded");
        }        
    }
}
