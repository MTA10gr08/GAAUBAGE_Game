using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.API.Services
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
    }
}
