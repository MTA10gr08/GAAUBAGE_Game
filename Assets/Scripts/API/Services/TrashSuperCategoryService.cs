using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashSuperCategoryService
    {
        public static async Task<RequestResult<string>> PostTrashSuperCategoryAsync(TrashSuperCategory TrashSuperCategory, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync<string, TrashSuperCategory>(apiUrl, TrashSuperCategory);
        }

        public static void PostTrashSuperCategory(TrashSuperCategory TrashSuperCategory, Guid ImageAnnotationID, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, TrashSuperCategory, onResponse);
        }

        public static async Task<RequestResult<SubImageAnnotationGroup>> NextTrashSuperCategoryAsync()
        {
            string apiUrl = Endpoints.TrashSuperCategory.Next();
            return await APIRequestHandler.GetAsync<SubImageAnnotationGroup>(apiUrl);
        }

        public static void NextTrashSuperCategory(Action<RequestResult<SubImageAnnotationGroup>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSuperCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
