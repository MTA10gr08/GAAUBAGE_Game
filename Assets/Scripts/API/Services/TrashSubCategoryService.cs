using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashSubCategoryService
    {
        public static async Task<RequestResult<string>> PostTrashSubCategoryAsync(TrashSubCategory TrashCategory, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.TrashSubCategory.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync<string, TrashSubCategory>(apiUrl, TrashCategory);
        }

        public static void PostTrashSubCategory(TrashSubCategory TrashCategory, Guid ImageAnnotationID, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSubCategory.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, TrashCategory, onResponse);
        }

        public static async Task<RequestResult<TrashSuperCategory>> NextTrashSubCategoryAsync()
        {
            string apiUrl = Endpoints.TrashSubCategory.Next();
            return await APIRequestHandler.GetAsync<TrashSuperCategory>(apiUrl);
        }

        public static void NextTrashSubCategory(Action<RequestResult<TrashSuperCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSubCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
