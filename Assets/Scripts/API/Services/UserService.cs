using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal static class UserService
    {
        public static async Task<RequestResult<string>> PostUserAsync(User user)
        {
            string apiUrl = Endpoints.Users.Post();
            return await APIRequestHandler.PostAsync<string, User>(apiUrl, user);
        }

        public static void PostUser(User user, Action<RequestResult<string>>? onResponse = null)
        {
            string apiUrl = Endpoints.Users.Post();
            APIRequestHandler.Post(apiUrl, user, onResponse);
        }

        public static async Task<RequestResult<User>> GetUserAsync(Guid id)
        {
            string apiUrl = Endpoints.Users.Get(id);
            return await APIRequestHandler.GetAsync<User>(apiUrl);
        }

        public static void GetUser(Guid id, Action<RequestResult<User>>? onResponse = null)
        {
            string apiUrl = Endpoints.Users.Get(id);
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
