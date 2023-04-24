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

    /// <summary>
    /// Represents the payload of a JSON Web Token (JWT).
    /// </summary>
    public class Payload
    {
        /// <summary>
        /// Name Identifier claim, which is a unique identifier for the user.
        /// </summary>
        public string nameid { get; set; }

        /// <summary>
        /// Role claim, which represents the role(s) assigned to the user.
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// Not Before claim, which is a UNIX timestamp indicating when the token should start being considered valid.
        /// </summary>
        public int nbf { get; set; }

        /// <summary>
        /// Expiration Time claim, which is a UNIX timestamp indicating when the token should stop being considered valid.
        /// </summary>
        public int exp { get; set; }

        /// <summary>
        /// Issued At claim, which is a UNIX timestamp indicating when the token was issued.
        /// </summary>
        public int iat { get; set; }

        /// <summary>
        /// Issuer claim, which is a string that identifies the issuer of the token.
        /// </summary>
        public string iss { get; set; }

        /// <summary>
        /// Audience claim, which is a string that identifies the intended recipient of the token.
        /// </summary>
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
