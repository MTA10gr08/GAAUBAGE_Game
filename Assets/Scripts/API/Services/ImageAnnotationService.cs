using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class ImageAnnotationService
    {
        public static async Task<RequestResult<ImageAnnotation>> GetImageAnnotationAsync(Guid id)
        {
            string apiUrl = Endpoints.ImageAnnotation.Get(id);
            return await APIRequestHandler.GetAsync<ImageAnnotation>(apiUrl);
        }

        public static void GetImageImageAnnotationBox(Guid id, Action<RequestResult<ImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.ImageAnnotation.Get(id);
            APIRequestHandler.Get(apiUrl, onResponse);
        }

        public static async Task<RequestResult<ImageAnnotation>> VoteSkipImageAnnotationAsync(Guid id)
        {
            string apiUrl = Endpoints.ImageAnnotation.VoteSkip(id);
            return await APIRequestHandler.GetAsync<ImageAnnotation>(apiUrl);
        }

        public static void VoteSkipImageAnnotationBox(Guid id, Action<RequestResult<ImageAnnotation>>? onResponse = null)
        {
            string apiUrl = Endpoints.ImageAnnotation.VoteSkip(id);
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
