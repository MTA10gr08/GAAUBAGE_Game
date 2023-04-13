using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashCategoryService
    {
        public static async Task<RequestResult<TrashCategory>> PostTrashCategoryAsync(TrashCategory TrashCategory)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<TrashCategory, TrashCategory>(apiUrl, TrashCategory);
        }

        public static void PostTrashCategory(TrashCategoryService user, Action<RequestResult<TrashCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCategory.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
        }

        public static async Task<RequestResult<TrashSuperCategory>> NextTrashCategoryAsync()
        {
            string apiUrl = Endpoints.TrashCategory.Next();
            return await APIRequestHandler.GetAsync<TrashSuperCategory>(apiUrl);
        }

        public static void GetTrashCategory(Action<RequestResult<TrashSuperCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
