using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashCategoryService
    {
        public static async Task<RequestResult<string>> PostTrashCategoryAsync(TrashSubCategory TrashCategory)
        {
            string apiUrl = Endpoints.TrashCategory.Post();
            return await APIRequestHandler.PostAsync<string, TrashSubCategory>(apiUrl, TrashCategory);
        }

        public static void PostTrashCategory(TrashSubCategory TrashCategory, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCategory.Post();
            APIRequestHandler.Post(apiUrl, TrashCategory, onResponse);
        }

        public static async Task<RequestResult<TrashSuperCategory>> NextTrashCategoryAsync()
        {
            string apiUrl = Endpoints.TrashCategory.Next();
            return await APIRequestHandler.GetAsync<TrashSuperCategory>(apiUrl);
        }

        public static void NextTrashCategory(Action<RequestResult<TrashSuperCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
