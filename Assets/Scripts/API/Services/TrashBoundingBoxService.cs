using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashBoundingBoxService
    {
        public static async Task<RequestResult<string>> PostTrashBoundingBoxAsync(TrashBoundingBox TrashBoundingBox)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post();
            return await APIRequestHandler.PostAsync<string, TrashBoundingBox>(apiUrl, TrashBoundingBox);
        }

        public static void PostTrashBoundingBox(TrashBoundingBox TrashBoundingBox, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post();
            APIRequestHandler.Post(apiUrl, TrashBoundingBox, onResponse);
        }

        public static async Task<RequestResult<TrashCount>> NextTrashBoundingBoxAsync()
        {
            string apiUrl = Endpoints.TrashBoundingBox.Next();
            return await APIRequestHandler.GetAsync<TrashCount>(apiUrl);
        }

        public static void GetTrashBoundingBox(Action<RequestResult<TrashCount>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
