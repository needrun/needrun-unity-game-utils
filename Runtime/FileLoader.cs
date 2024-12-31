using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;

namespace NeedrunGameUtils
{
    public class FileLoader
    {
        public static string Load(string filePath)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                // Need to extract file from apk first
                // https://gist.github.com/amowu/8121334
                UnityWebRequest unityWebRequest = UnityEngine.Networking.UnityWebRequest.Get(filePath);
                UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = unityWebRequest.SendWebRequest();
                while (unityWebRequest.result == UnityWebRequest.Result.InProgress) { }
                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
                    unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
                    unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
                    throw new Exception("Error occured while read file! error: " + unityWebRequest.result + ", filePath : " + filePath);
                return unityWebRequest.downloadHandler.text;
            }
            else
            {
                if (!File.Exists(filePath))
                {
                    throw new Exception("Can not find file! filePath : " + filePath);
                }
                return File.ReadAllText(filePath);
            }
        }
    }
}
