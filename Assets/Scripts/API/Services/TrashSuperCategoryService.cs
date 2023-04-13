using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashSuperCategoryService
    {
        public static async Task<RequestResult<TrashSuperCategory>> PostTrashSuperCategoryAsync(TrashSuperCategory TrashSuperCategory)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<TrashSuperCategory, TrashSuperCategory>(apiUrl, TrashSuperCategory);
        }

        public static void PostTrashSuperCategory(TrashSuperCategoryService user, Action<RequestResult<TrashSuperCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
        }

        public static async Task<RequestResult<TrashBoundingBox>> NextTrashSuperCategoryAsync()
        {
            string apiUrl = Endpoints.TrashSuperCategory.Next();
            return await APIRequestHandler.GetAsync<TrashBoundingBox>(apiUrl);
        }

        public static void GetTrashSuperCategory(Action<RequestResult<TrashBoundingBox>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
