using GAAUBAGE_Game.API.Models;
using GAAUBAGE_Game.API.Networking;
using GAAUBAGE_Game.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class CurrentUser
{
    public static string Alias { get; set; }
    public static string Tag { get; set; }
    public static string JWT { get; set; }
    public static Guid ID { get; set; }
    public static DateTime Created { get; set; }
    public static async Task<bool> initialize()
    {
        JWT = PlayerPrefs.GetString("JWT");
        if (string.IsNullOrEmpty(JWT)) return false;
        APIRequestHandler.JWT = JWT;
        var response = await UserService.GetCurrentUserAsync();
        if (response.ResultCode != UnityWebRequest.Result.Success) return false;
        var user = response.Value;
        Alias = user.Alias;
        Tag = user.Tag;
        ID = user.ID;
        Created = user.Created;
        return true;
    }
    public static async Task<bool> setup(string alias)
    {
        var response = await UserService.PostUserAsync(new User { Alias = alias });
        if (response.ResultCode != UnityWebRequest.Result.Success) return false;
        PlayerPrefs.SetString("JWT", response.Value);
        return true;
    }
}
