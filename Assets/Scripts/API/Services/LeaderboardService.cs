using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class LeaderboardService
    {
        public static async Task<RequestResult<Leaderboard>> GetLeaderboardAsync()
        {
            string apiUrl = Endpoints.Leaderboard.Get();
            return await APIRequestHandler.GetAsync<Leaderboard>(apiUrl);
        }

        public static void GetLeaderboard(Action<RequestResult<Leaderboard>>? onResponse = null)
        {
            string apiUrl = Endpoints.Leaderboard.Get();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}