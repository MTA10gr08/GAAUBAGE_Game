using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace GAAUBAGE_Game.API.Networking
{
    public static class UnityWebRequestAwaitExtensions
    {
        public static TaskAwaiter<UnityWebRequest> GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
        {
            TaskCompletionSource<UnityWebRequest> tcs = new TaskCompletionSource<UnityWebRequest>();
            asyncOp.completed += obj => { tcs.SetResult(((UnityWebRequestAsyncOperation)obj).webRequest); };
            return tcs.Task.GetAwaiter();
        }
    }
}