using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashSubCategoryService
    {
        public static async Task<RequestResult<string>> PostTrashSubCategoryAsync(TrashSubCategory TrashCategory)
        {
            string apiUrl = Endpoints.TrashCategory.Post();
            return await APIRequestHandler.PostAsync<string, TrashSubCategory>(apiUrl, TrashCategory);
        }

        public static void PostTrashSubCategory(TrashSubCategory TrashCategory, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCategory.Post();
            APIRequestHandler.Post(apiUrl, TrashCategory, onResponse);
        }

        public static async Task<RequestResult<TrashSuperCategory>> NextTrashSubCategoryAsync()
        {
            string apiUrl = Endpoints.TrashCategory.Next();
            return await APIRequestHandler.GetAsync<TrashSuperCategory>(apiUrl);
        }

        public static void NextTrashSubCategory(Action<RequestResult<TrashSuperCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
