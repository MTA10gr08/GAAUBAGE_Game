using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class UserGoalService
    {
        public static async Task<RequestResult<List<UserGoal>>> GetUserGoalAsync()
        {
            string apiUrl = Endpoints.UserGoal.Get();
            return await APIRequestHandler.GetAsync<List<UserGoal>>(apiUrl);
        }

        public static void GetUserGoal(Action<RequestResult<List<UserGoal>>>? onResponse = null)
        {
            string apiUrl = Endpoints.UserGoal.Get();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
