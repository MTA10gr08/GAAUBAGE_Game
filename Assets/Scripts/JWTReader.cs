using Newtonsoft.Json;
using System;
using System.Text;

public class JWTReader
{
    public static Payload GetPayload(string JWTString)
    {
        string payloadJson = DecodePayload(JWTString);
        return JsonConvert.DeserializeObject<Payload>(payloadJson);
    }

    public class Payload
    {
        public string nameid { get; set; }
        public string role { get; set; }
        public int nbf { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
    }


    private static string DecodePayload(string jwt)
    {
        string[] parts = jwt.Split('.');
        if (parts.Length != 3)
        {
            throw new ArgumentException("Invalid JWT token.");
        }

        string payloadBase64Url = parts[1];
        string payloadJson = Base64UrlDecode(payloadBase64Url);
        return payloadJson;
    }

    private static string Base64UrlDecode(string base64Url)
    {
        string padded = base64Url.Length % 4 == 0
            ? base64Url
            : base64Url + "====".Substring(base64Url.Length % 4);
        string base64 = padded.Replace("_", "/").Replace("-", "+");
        byte[] buffer = Convert.FromBase64String(base64);
        string decoded = Encoding.UTF8.GetString(buffer);
        return decoded;
    }
}
