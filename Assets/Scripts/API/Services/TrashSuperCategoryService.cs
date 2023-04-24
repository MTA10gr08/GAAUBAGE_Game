using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashSuperCategoryService
    {
        public static async Task<RequestResult<string>> PostTrashSuperCategoryAsync(TrashSuperCategory TrashSuperCategory)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Post();
            return await APIRequestHandler.PostAsync<string, TrashSuperCategory>(apiUrl, TrashSuperCategory);
        }

        public static void PostTrashSuperCategory(TrashSuperCategory TrashSuperCategory, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Post();
            APIRequestHandler.Post(apiUrl, TrashSuperCategory, onResponse);
        }

        public static async Task<RequestResult<SubImageGroup>> NextTrashSuperCategoryAsync()
        {
            string apiUrl = Endpoints.TrashSuperCategory.Next();
            return await APIRequestHandler.GetAsync<SubImageGroup>(apiUrl);
        }

        public static void NextTrashSuperCategory(Action<RequestResult<SubImageGroup>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
