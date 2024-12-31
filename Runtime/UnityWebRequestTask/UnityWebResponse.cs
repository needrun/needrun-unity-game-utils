using UnityEngine.Networking;

namespace NeedrunGameUtils
{
    public class UnityWebResponse
    {
        public UnityWebRequest.Result result;
        public long code;
        public string body;
        public string error;

        public string resultAsString
        {
            get
            {
                switch (result)
                {

                    case UnityWebRequest.Result.InProgress:
                        return "InProgress";
                    case UnityWebRequest.Result.Success:
                        return "Success";
                    case UnityWebRequest.Result.ConnectionError:
                        return "ConnectionError";
                    case UnityWebRequest.Result.ProtocolError:
                        return "ProtocolError";
                    case UnityWebRequest.Result.DataProcessingError:
                        return "DataProcessingError";
                    default:
                        return "NULL";
                }
            }
        }
    }
}
