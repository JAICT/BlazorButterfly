using System.Threading.Tasks;

namespace JAICT.Blazor.Recipes.Application
{
    public interface IStateStorage
    {
        Task Store<T>(T state, string key) where T : class;

        Task<T> Get<T>(string key) where T : class;
    }
}
