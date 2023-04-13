using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class ContextClassification
    {
        public static async Task<RequestResult> PostContextClassificationAsync(ContextClassification ContextClassification)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync(apiUrl, ContextClassification);
        }

        public static void PostContextClassification(ContextClassification user, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.ContextClassification.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
        }

        public static async Task<RequestResult<BackgroundClassification>> NextContextClassificationAsync(Guid id)
        {
            string apiUrl = Endpoints.ContextClassification.Next();
            return await APIRequestHandler.GetAsync<BackgroundClassification>(apiUrl);
        }

        public static void GetContextClassification(Guid id, Action<RequestResult<BackgroundClassification>>? onResponse = null)
        {
            string apiUrl = Endpoints.ContextClassification.Get(id);
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
