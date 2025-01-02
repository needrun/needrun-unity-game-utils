using System.Threading.Tasks;
using UnityEngine;

namespace NeedrunGameUtils
{
    public class LoadingCompletion
    {
        private bool loadedAtOnce = false;
        private TaskCompletionSource<bool> completion = new TaskCompletionSource<bool>(false);
        public Task loadingTask
        {
            get
            {
                return completion.Task;
            }
        }

        public bool isLoadedAtOnce
        {
            get
            {
                return loadedAtOnce;
            }
        }

        public void Complete()
        {
            completion.SetResult(true);
            loadedAtOnce = true;
        }

        public void Reset()
        {
            completion = new TaskCompletionSource<bool>(false);
            loadedAtOnce = false;
        }
    }
}
