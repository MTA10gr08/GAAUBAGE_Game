using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ApiHelper
{
    private static readonly HttpClient client = new HttpClient();
    public static readonly string APIURL = "http://65.108.68.48:1515/";
    //public static readonly string APIURL = "http://62.107.209.26:1515/";


    public static async Task<T> GetAsync<T>(string path) {
        Debug.Log(APIURL + path);
        try {
            var response = await client.GetStringAsync(APIURL + path);
            return JsonConvert.DeserializeObject<T>(response);
        } catch (Exception ex) {
            Debug.LogException(ex);
            return default(T);
        }
    }
    public static async Task<T> PostAsync<T>(string path, T Model) {
        Debug.Log(APIURL + path);
        try {
            var json = JsonConvert.SerializeObject(Model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(APIURL + path, data);
            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            } else {
                return default(T);
            }
        } catch (Exception ex) {
            Debug.LogException(ex);
            return default(T);
        }
    }

    public static async Task<T> PutAsync<T>(string path, T Model) {
        Debug.Log(APIURL + path);
        try {
            var json = JsonConvert.SerializeObject(Model);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(APIURL + path, data);
            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            } else {
                return default(T);
            }

        } catch (Exception ex) {
            Debug.LogException(ex);
            return default(T);
        }
    }
}



public class UserModel : BaseModel
{
    public string userName { get; set; }
    public List<AnnotationModel> annotations { get; set; }
    public int energySpent { get; set; }
    public List<int> imageMetas { get; set; }
}

public class ImageMetaModel : BaseModel
{
    public string? FileName { get; set; }
    public IReadOnlyList<int>? Annotations { get; set; } = new List<int>();
    public IReadOnlyList<int>? Users { get; set; } = new List<int>();
}

public class AnnotationModel : BaseModel
{
    public int imageMetaId { get; set; }
    public int userId { get; set; }
    public string annotationLabel { get; set; }
    public List<SimplePoint> segmentation { get; set; }


}
public class SimplePoint
{
    public float X { get; set; }
    public float Y { get; set; }
    public SimplePoint() { }
}

public abstract class BaseModel
{
    public int id { get; set; }
    public DateTime createdDateUtc { get; set; }
    public DateTime? updatedDateUtc { get; set; }

}