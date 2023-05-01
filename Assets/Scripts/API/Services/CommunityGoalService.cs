using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class CommunityGoalService
    {
        public static async Task<RequestResult<CommunityGoal>> GetCommunityGoalAsync()
        {
            string apiUrl = Endpoints.ComunityGoal.Get();
            return await APIRequestHandler.GetAsync<CommunityGoal>(apiUrl);
        }

        public static void GetCommunityGoal(Action<RequestResult<CommunityGoal>>? onResponse = null)
        {
            string apiUrl = Endpoints.ComunityGoal.Get();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
