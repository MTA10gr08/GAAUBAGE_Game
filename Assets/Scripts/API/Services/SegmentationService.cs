using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class SegmentationService
    {
        public static async Task<RequestResult<Segmentation>> PostSegmentationAsync(Segmentation Segmentation)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<Segmentation, Segmentation>(apiUrl, Segmentation);
        }

        public static void PostSegmentation(Segmentation Segmentation, Action<RequestResult<Segmentation>>? onResponse = null)
        {
            string apiUrl = Endpoints.Segmentation.Post();
            APIRequestHandler.Post(apiUrl, Segmentation, onResponse);
        }

        public static async Task<RequestResult<TrashCategory>> NextSegmentationAsync()
        {
            string apiUrl = Endpoints.Segmentation.Next();
            return await APIRequestHandler.GetAsync<TrashCategory>(apiUrl);
        }

        public static void GetSegmentation(Action<RequestResult<TrashCategory>>? onResponse = null)
        {
            string apiUrl = Endpoints.Segmentation.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
