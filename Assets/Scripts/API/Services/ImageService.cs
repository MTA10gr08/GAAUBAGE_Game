using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class ImageService
    {
        public static async Task<RequestResult<Image>> GetImageAsync(Guid id)
        {
            string apiUrl = Endpoints.Images.Get(id);
            return await APIRequestHandler.GetAsync<Image>(apiUrl);
        }

        public static void GetImageBox(Guid id, Action<RequestResult<Image>>? onResponse = null)
        {
            string apiUrl = Endpoints.Images.Get(id);
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
