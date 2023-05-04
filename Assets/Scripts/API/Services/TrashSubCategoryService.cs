using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashSubCategoryService
    {
        public static async Task<RequestResult> PostTrashSubCategoryAsync(TrashSubCategory TrashCategory, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.TrashSubCategory.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync(apiUrl, TrashCategory);
        }

        public static void PostTrashSubCategory(TrashSubCategory TrashCategory, Guid ImageAnnotationID, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSubCategory.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, TrashCategory, onResponse);
        }

        public static async Task<RequestResult<SubImageAnnotation>> NextTrashSubCategoryAsync()
        {
            string apiUrl = Endpoints.TrashSubCategory.Next();
            return await APIRequestHandler.GetAsync<SubImageAnnotation>(apiUrl);
        }

        public static void NextTrashSubCategory(Action<RequestResult<SubImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSubCategory.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }

        public static async Task<RequestResult<TrashSubCategory>> GetTrashSubCategoryAsync(Guid TrashsubcategorieID)
        {
            string apiUrl = Endpoints.TrashSubCategory.Get(TrashsubcategorieID);
            return await APIRequestHandler.GetAsync<TrashSubCategory>(apiUrl);
        }

        public static void GetTrashSubCategory(Guid TrashsubcategorieID, Action<RequestResult<TrashSubCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSubCategory.Get(TrashsubcategorieID);
            APIRequestHandler.Get(apiUrl, onResponse);
        }

        public static async Task<RequestResult<TrashSubCategory>> GetMyTrashSubCategoryAsync(Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.TrashSubCategory.GetMine(ImageAnnotationID);
            return await APIRequestHandler.GetAsync<TrashSubCategory>(apiUrl);
        }

        public static void GetMyTrashSubCategory(Guid ImageAnnotationID, Action<RequestResult<TrashSubCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashSubCategory.GetMine(ImageAnnotationID);
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
