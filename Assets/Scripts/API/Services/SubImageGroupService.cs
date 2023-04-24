using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class SubImageGroupService
    {
        public static async Task<RequestResult> PostTrashBoundingBoxAsync(SubImageGroup TrashBoundingBox, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync(apiUrl, TrashBoundingBox);
        }

        public static void PostTrashBoundingBox(SubImageGroup TrashBoundingBox, Guid ImageAnnotationID, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, TrashBoundingBox, onResponse);
        }

        public static async Task<RequestResult<ImageAnnotation>> NextTrashBoundingBoxAsync()
        {
            string apiUrl = Endpoints.TrashBoundingBox.Next();
            return await APIRequestHandler.GetAsync<ImageAnnotation>(apiUrl);
        }

        public static void NextTrashBoundingBox(Action<RequestResult<ImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
