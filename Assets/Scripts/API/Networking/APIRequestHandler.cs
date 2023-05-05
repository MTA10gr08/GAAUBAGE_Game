using System;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
using Unity.VisualScripting;

#nullable enable
namespace GAAUBAGE_Game.API.Networking
{
    public class RequestResult<T> : RequestResult where T : class
    {
        public T? Value;
    }
    public class RequestResult
    {
        public long ResponseCode;
        public UnityWebRequest.Result ResultCode;

        public override string ToString() => ResponseCode.ToString() + ResultCode.ToString();
    }

    public static class APIRequestHandler
    {
        public static string JWT = string.Empty;
        public static int Timeout = 15;

        public static async Task<RequestResult<T>> GetAsync<T>(string Endpoint)
            where T : class
        {
            using var request = CreateRequest(Endpoint);

            await request.SendWebRequest();

            var requestResult = new RequestResult<T>
            {
                Value = JsonConvert.DeserializeObject<T>(request.downloadHandler.text),
                ResponseCode = request.responseCode,
                ResultCode = request.result
            };

            return requestResult;
        }

        public static void Get<T>(string Endpoint, Action<RequestResult<T>>? onResponse = null)
            where T : class
        {
            using var request = CreateRequest(Endpoint);
            var requestAsync = request.SendWebRequest();
            if (onResponse != null)
            {
                requestAsync.completed += (_) =>
                {
                    var objectReceived = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                    var requestResult = new RequestResult<T>
                    {
                        Value = JsonConvert.DeserializeObject<T>(request.downloadHandler.text),
                        ResponseCode = request.responseCode,
                        ResultCode = request.result
                    };
                    onResponse.Invoke(requestResult);
                };
            }
        }

        public static async Task<RequestResult> PostAsync(string Endpoint)
        {
            using var request = CreateRequest(Endpoint, UnityWebRequest.kHttpVerbPOST);

            await request.SendWebRequest();

            var requestResult = new RequestResult
            {
                ResponseCode = request.responseCode,
                ResultCode = request.result
            };

            return requestResult;
        }

        public static void Post(string Endpoint, Action<RequestResult>? onResponse = null)
        {
            using var request = CreateRequest(Endpoint, UnityWebRequest.kHttpVerbPOST);
            var requestAsync = request.SendWebRequest();
            if (onResponse != null)
            {
                requestAsync.completed += (_) =>
                {
                    var requestResult = new RequestResult
                    {
                        ResponseCode = request.responseCode,
                        ResultCode = request.result
                    };
                    onResponse.Invoke(requestResult);
                };
            }
        }

        public static async Task<RequestResult> PostAsync<T>(string Endpoint, T Data)
            where T : class
        {
            using var request = CreateRequest(Endpoint, Data);

            await request.SendWebRequest();

            var requestResult = new RequestResult
            {
                ResponseCode = request.responseCode,
                ResultCode = request.result
            };

            return requestResult;
        }

        public static void Post<T>(string Endpoint, T Data, Action<RequestResult>? onResponse = null)
            where T : class
        {
            using var request = CreateRequest(Endpoint, Data);
            var requestAsync = request.SendWebRequest();
            if (onResponse != null)
            {
                requestAsync.completed += (_) =>
                {
                    var requestResult = new RequestResult
                    {
                        ResponseCode = request.responseCode,
                        ResultCode = request.result
                    };
                    onResponse.Invoke(requestResult);
                };
            }
        }

        public static async Task<RequestResult<D>> PostAsync<D, T>(string Endpoint, T Data)
            where D : class
            where T : class
        {
            using var request = CreateRequest(Endpoint, Data);

            await request.SendWebRequest();

            var requestResult = new RequestResult<D>
            {
                Value = JsonConvert.DeserializeObject<D>(request.downloadHandler.text),
                ResponseCode = request.responseCode,
                ResultCode = request.result
            };

            return requestResult;
        }

        public static void Post<D, T>(string Endpoint, T Data, Action<RequestResult<D>>? onResponse = null)
            where D : class
            where T : class
        {
            using var request = CreateRequest(Endpoint, Data);

            var requestAsync = request.SendWebRequest();
            if (onResponse != null)
            {
                requestAsync.completed += (_) =>
                {
                    var objectReceived = JsonConvert.DeserializeObject<D>(request.downloadHandler.text);
                    var requestResult = new RequestResult<D>
                    {
                        Value = JsonConvert.DeserializeObject<D>(request.downloadHandler.text),
                        ResponseCode = request.responseCode,
                        ResultCode = request.result
                    };
                    onResponse.Invoke(requestResult);
                };
            }
        }

        public static async Task<RequestResult<D>> PutAsync<D, T>(string Endpoint, T Data)
            where D : class
            where T : class
        {
            using var request = CreateRequest(Endpoint, Data, UnityWebRequest.kHttpVerbPUT);

            await request.SendWebRequest();

            var requestResult = new RequestResult<D>
            {
                Value = JsonConvert.DeserializeObject<D>(request.downloadHandler.text),
                ResponseCode = request.responseCode,
                ResultCode = request.result
            };

            return requestResult;
        }

        private static UnityWebRequest CreateRequest<T>(string Endpoint, T Data, string method = UnityWebRequest.kHttpVerbPOST) where T : class
        {
            var request = CreateRequest(Endpoint, method);

            string jsonToSend = JsonConvert.SerializeObject(Data);
            byte[] bytesToSend = new UTF8Encoding().GetBytes(jsonToSend);
            request.uploadHandler = new UploadHandlerRaw(bytesToSend);

            return request;
        }
        private static UnityWebRequest CreateRequest(string Endpoint, string method = UnityWebRequest.kHttpVerbGET)
        {
            UnityWebRequest request = new UnityWebRequest(Endpoint);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + JWT);
            request.timeout = Timeout;
            request.method = method;
            request.downloadHandler = new DownloadHandlerBuffer();

            return request;
        }

        public static void Post2<D, T>(string Endpoint, T Data, Action<RequestResult<D>>? onResponse = null)
            where D : class
            where T : class
        {
            using var request = CreateRequest(Endpoint, Data);
            var requestAsync = request.SendWebRequest();
            if (onResponse != null)
            {
                requestAsync.completed += (_) =>
                {
                    var objectReceived = JsonConvert.DeserializeObject<D>(request.downloadHandler.text);
                    var requestResult = new RequestResult<D>
                    {
                        Value = JsonConvert.DeserializeObject<D>(request.downloadHandler.text),
                        ResponseCode = request.responseCode,
                        ResultCode = request.result
                    };
                    onResponse.Invoke(requestResult);
                };
            }
        }
    }
}