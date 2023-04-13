using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashBoundingBoxService
    {
        public static async Task<RequestResult<TrashBoundingBox>> PostTrashBoundingBoxAsync(TrashBoundingBox TrashBoundingBox)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<TrashBoundingBox, TrashBoundingBox>(apiUrl, TrashBoundingBox);
        }

        public static void PostTrashBoundingBox(TrashBoundingBoxService user, Action<RequestResult<TrashBoundingBox>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashBoundingBox.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
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
