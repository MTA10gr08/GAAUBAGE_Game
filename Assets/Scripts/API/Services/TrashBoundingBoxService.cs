using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashBoundingBoxService
    {
        public static async Task<RequestResult> PostTrashBoundingBoxAsync(TrashBoundingBox TrashBoundingBox, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync(apiUrl, TrashBoundingBox);
        }

        public static void PostTrashBoundingBox(TrashBoundingBox TrashBoundingBox, Guid ImageAnnotationID, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, TrashBoundingBox, onResponse);
        }

        public static async Task<RequestResult<TrashCount>> NextTrashBoundingBoxAsync()
        {
            string apiUrl = Endpoints.TrashBoundingBox.Next();
            return await APIRequestHandler.GetAsync<TrashCount>(apiUrl);
        }

        public static void NextTrashBoundingBox(Action<RequestResult<TrashCount>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
