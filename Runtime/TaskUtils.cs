using System;
using System.Threading;
using System.Threading.Tasks;

namespace NeedrunGameUtils
{
    public class TaskUtils
    {
        public static bool IsError(Task task)
        {
            return !task.IsCompleted || task.IsFaulted || task.IsCanceled;
        }

        public static void AssertSuccess(Task task)
        {
            if (TaskUtils.IsError(task))
            {
                throw new Exception("task 실행에 실패했습니다." + task.Exception);
            }
        }

        public static Task<bool> UntilDone(TaskCompletionSource<bool> task)
        {
            return task.Task;
        }

        public static TaskCompletionSource<bool> CreateCompletionFlag()
        {
            return new TaskCompletionSource<bool>(false);
        }

        public static async Task WaitUntil(Func<bool> condition, int checkInterval = 100, int timeout = -1, CancellationTokenSource cancellationTokenSource = null)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            var startTime = DateTime.Now;

            while (!condition())
            {
                if (cancellationTokenSource != null && cancellationTokenSource.Token.IsCancellationRequested)
                    throw new OperationCanceledException("Task was canceled.");

                if (timeout > 0 && (DateTime.Now - startTime).TotalMilliseconds > timeout)
                    throw new TimeoutException("The condition was not met within the specified timeout.");

                if (cancellationTokenSource != null)
                    await Task.Delay(checkInterval, cancellationTokenSource.Token);
                else
                    await Task.Delay(checkInterval);
            }
        }
    }
}