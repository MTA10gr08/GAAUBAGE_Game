using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace GAAUBAGE_Game.API.Services
{
    internal class DatabaseInfoService
    {
        public static async Task<RequestResult<DatabaseInfo>> GetUserGoalAsync()
        {
            string apiUrl = Endpoints.DatabaseInfo.Get();
            return await APIRequestHandler.GetAsync<DatabaseInfo>(apiUrl);
        }

        public static void GetUserGoal(Action<RequestResult<DatabaseInfo>>? onResponse = null)
        {
            string apiUrl = Endpoints.DatabaseInfo.Get();
            APIRequestHandler.Get(apiUrl, onResponse);
        }
    }
}
