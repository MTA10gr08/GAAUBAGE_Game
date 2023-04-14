using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class ContextClassificationService
    {
        public static async Task<RequestResult<ContextClassification>> PostContextClassificationAsync(ContextClassification contextClassification)
        {
            string apiUrl = Endpoints.ContextClassification.Post();
            return await APIRequestHandler.PostAsync<ContextClassification, ContextClassification>(apiUrl, contextClassification);
        }

        public static void PostContextClassification(ContextClassification contextClassification, Action<RequestResult<ContextClassification>>? onResponse = null)
        {
            string apiUrl = Endpoints.ContextClassification.Post();
            APIRequestHandler.Post(apiUrl, contextClassification, onResponse);
        }

        public static async Task<RequestResult<BackgroundClassification>> NextContextClassificationAsync()
        {
            string apiUrl = Endpoints.ContextClassification.Next();
            return await APIRequestHandler.GetAsync<BackgroundClassification>(apiUrl);
        }

        public static void GetContextClassification(Action<RequestResult<BackgroundClassification>>? onResponse = null)
        {
            string apiUrl = Endpoints.ContextClassification.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
