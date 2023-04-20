using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Models;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class ConfigurationService
    {
        public static async Task<RequestResult<Categories>> GetConfigurationAsync()
        {
            string apiUrl = Endpoints.Configuration.Get();
            return await APIRequestHandler.GetAsync<Categories>(apiUrl);
        }

        public static void GetConfiguration(Action<RequestResult<Categories>>? onResponse = null)
        {
            string apiUrl = Endpoints.Configuration.Get();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
