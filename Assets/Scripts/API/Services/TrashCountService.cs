using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class TrashCountService
    {
        public static async Task<RequestResult<TrashCount>> PostTrashCountAsync(TrashCount trashCount)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<TrashCount, TrashCount>(apiUrl, trashCount);
        }

        public static void PostTrashCount(TrashCountService user, Action<RequestResult<TrashCount>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCount.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
        }

        public static async Task<RequestResult<ContextCassification>> NextTrashCountAsync()
        {
            string apiUrl = Endpoints.TrashCount.Next();
            return await APIRequestHandler.GetAsync<ContextCassification>(apiUrl);
        }

        public static void GetTrashCount(Action<RequestResult<ContextCassification>>? onResponse = null)
        {
            string apiUrl = Endpoints.TrashCount.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
