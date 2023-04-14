using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashCountService
    {
        public static async Task<RequestResult<string>> PostTrashCountAsync(TrashCount trashCount)
        {
            string apiUrl = Endpoints.TrashCount.Post();
            return await APIRequestHandler.PostAsync<string, TrashCount>(apiUrl, trashCount);
        }

        public static void PostTrashCount(TrashCount trashCount, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCount.Post();
            APIRequestHandler.Post(apiUrl, trashCount, onResponse);
        }

        public static async Task<RequestResult<ContextClassification>> NextTrashCountAsync()
        {
            string apiUrl = Endpoints.TrashCount.Next();
            return await APIRequestHandler.GetAsync<ContextClassification>(apiUrl);
        }

        public static void GetTrashCount(Action<RequestResult<ContextClassification>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCount.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
