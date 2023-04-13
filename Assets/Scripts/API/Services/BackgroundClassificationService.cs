using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal static class BackgroundClassificationService
    {
        public static async Task<RequestResult> PostBackgroundClassificationAsync(BackgroundClassification backgroundClassification)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync(apiUrl, backgroundClassification);
        }

        public static void PostBackgroundClassification(BackgroundClassification user, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.BackgroundClassification.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
        }

        public static async Task<RequestResult<Image>> NextBackgroundClassificationAsync(Guid id)
        {
            string apiUrl = Endpoints.BackgroundClassification.Next();
            return await APIRequestHandler.GetAsync<Image>(apiUrl);
        }

        public static void GetBackgroundClassification(Guid id, Action<RequestResult<Image>>? onResponse = null)
        {
            string apiUrl = Endpoints.BackgroundClassification.Get(id);
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
