using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class SegmentationService
    {
        public static async Task<RequestResult> PostSegmentationAsync(Models.Segmentation Segmentation, Guid ImageAnnotationID)
        {
            string apiUrl = Endpoints.Segmentation.Post(ImageAnnotationID);
            return await APIRequestHandler.PostAsync(apiUrl, Segmentation);
        }

        public static void PostSegmentation(Models.Segmentation Segmentation, Guid ImageAnnotationID, Action<RequestResult>? onResponse = null)
        {
            string apiUrl = Endpoints.Segmentation.Post(ImageAnnotationID);
            APIRequestHandler.Post(apiUrl, Segmentation, onResponse);
        }

        public static async Task<RequestResult<SubImageAnnotation>> NextSegmentationAsync()
        {
            string apiUrl = Endpoints.Segmentation.Next();
            return await APIRequestHandler.GetAsync<SubImageAnnotation>(apiUrl);
        }

        public static void NextSegmentation(Action<RequestResult<SubImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.Segmentation.Next();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
