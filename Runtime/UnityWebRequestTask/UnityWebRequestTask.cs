using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System;

namespace NeedrunGameUtils
{
    public class UnityWebRequestTask
    {
        private readonly TaskCompletionSource<UnityWebResponse> completion = new TaskCompletionSource<UnityWebResponse>();
        private MonoBehaviour worker;
        private UnityWebRequest request;

        private UnityWebRequestTask(MonoBehaviour worker)
        {
            this.worker = worker;
        }

        public static UnityWebRequestTask Get(MonoBehaviour worker, string url)
        {
            return new UnityWebRequestTask(worker).Get(url);
        }

        public static UnityWebRequestTask Post(MonoBehaviour worker, string url, WWWForm form)
        {
            return new UnityWebRequestTask(worker).Post(url, form);
        }

        public static UnityWebRequestTask Put(MonoBehaviour worker, string url, WWWForm form)
        {
            return new UnityWebRequestTask(worker).Put(url, form);
        }

        public static UnityWebRequestTask Delete(MonoBehaviour worker, string url)
        {
            return new UnityWebRequestTask(worker).Delete(url);
        }

        private UnityWebRequestTask Get(string url)
        {
            try
            {
                request = UnityWebRequest.Get(url);
            }
            catch (Exception e)
            {
                // Create can only be called from the main thread.
                Debug.LogError(e.Message);
            }
            return this;
        }

        private UnityWebRequestTask Post(string url, WWWForm form)
        {
            try
            {
                request = UnityWebRequest.Post(url, form);
            }
            catch (Exception e)
            {
                // Create can only be called from the main thread.
                Debug.LogError(e.Message);
            }
            return this;
        }

        private UnityWebRequestTask Put(string url, WWWForm form)
        {
            try
            {
                byte[] rawData = form.data;
                request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPUT)
                {
                    uploadHandler = new UploadHandlerRaw(rawData),
                    downloadHandler = new DownloadHandlerBuffer()
                };
                request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            }
            catch (Exception e)
            {
                // Create can only be called from the main thread.
                Debug.LogError(e.Message);
            }
            return this;
        }


        private UnityWebRequestTask Delete(string url)
        {
            try
            {
                request = UnityWebRequest.Delete(url);
            }
            catch (Exception e)
            {
                // Create can only be called from the main thread.
                Debug.LogError(e.Message);
            }
            return this;
        }


        public UnityWebRequestTask Header(string key, string value)
        {
            request.SetRequestHeader(key, value);
            return this;
        }

        public Task<UnityWebResponse> Send()
        {
            worker.StartCoroutine(SendWebRequest(request));
            return completion.Task;
        }

        private IEnumerator SendWebRequest(UnityWebRequest unityWebRequest)
        {
            yield return unityWebRequest.SendWebRequest();
            UnityWebResponse unityWebResponse = new UnityWebResponse
            {
                result = unityWebRequest.result,
                code = unityWebRequest.responseCode,
                body = unityWebRequest.downloadHandler == null ? "" : unityWebRequest.downloadHandler.text,
                error = unityWebRequest.error,
            };
            completion.SetResult(unityWebResponse);
            Debug.Log($"api responsed\nresult: {unityWebResponse.result}\ncode: {unityWebResponse.code}\nbody: {unityWebResponse.body}\nerror: {unityWebResponse.error}");
            unityWebRequest.Dispose();
        }
    }
}