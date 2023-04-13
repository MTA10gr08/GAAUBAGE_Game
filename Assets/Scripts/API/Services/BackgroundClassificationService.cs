using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal static class BackgroundClassificationService
    {
        public static async Task<RequestResult<BackgroundClassification>> PostBackgroundClassificationAsync(BackgroundClassification backgroundClassification)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<BackgroundClassification, BackgroundClassification>(apiUrl, backgroundClassification);
        }

        public static void PostBackgroundClassification(BackgroundClassification backgroundClassification, Action<RequestResult<BackgroundClassification>>? onResponse = null)
        {
            string apiUrl = Endpoints.BackgroundClassification.Post();
            APIRequestHandler.Post(apiUrl, backgroundClassification, onResponse);
        }

        public static async Task<RequestResult<Image>> NextBackgroundClassificationAsync()
        {
            string apiUrl = Endpoints.BackgroundClassification.Next();
            return await APIRequestHandler.GetAsync<Image>(apiUrl);
        }

        public static void GetBackgroundClassification(Action<RequestResult<Image>>? onResponse = null)
        {
            string apiUrl = Endpoints.BackgroundClassification.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
