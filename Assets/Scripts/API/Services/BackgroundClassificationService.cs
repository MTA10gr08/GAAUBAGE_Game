using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal static class BackgroundClassificationService
    {
        public static async Task<RequestResult> PostBackgroundClassificationAsync(BackgroundClassification backgroundClassification, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.BackgroundClassification.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync(apiUrl, backgroundClassification);
        }

        public static void PostBackgroundClassification(BackgroundClassification backgroundClassification, Guid ImageAnnotationID, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.BackgroundClassification.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, backgroundClassification, onResponse);
        }

        public static async Task<RequestResult<ImageAnnotation>> NextBackgroundClassificationAsync()
        {
            string apiUrl = Endpoints.BackgroundClassification.Next();
            return await APIRequestHandler.GetAsync<ImageAnnotation>(apiUrl);
        }

        public static void GetBackgroundClassification(Action<RequestResult<ImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.BackgroundClassification.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
