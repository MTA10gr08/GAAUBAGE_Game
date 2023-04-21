using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class ContextClassificationService
    {
        public static async Task<RequestResult> PostContextClassificationAsync(ContextClassification contextClassification, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.ContextClassification.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync(apiUrl, contextClassification);
        }

        public static void PostContextClassification(ContextClassification contextClassification, Guid ImageAnnotationID, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.ContextClassification.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, contextClassification, onResponse);
        }

        public static async Task<RequestResult<ImageAnnotation>> NextContextClassificationAsync()
        {
            string apiUrl = Endpoints.ContextClassification.Next();
            return await APIRequestHandler.GetAsync<ImageAnnotation>(apiUrl);
        }

        public static void GetContextClassification(Action<RequestResult<ImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.ContextClassification.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
