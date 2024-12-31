using UnityEngine;

namespace NeedrunGameUtils
{
    public class DisplayWhenMonthDayMatched : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int startInclude;
        [SerializeField]
        private Vector2Int endExclude;

        public void DisplayOrNot(int month, int day)
        {
            int nowMMDD = month * 100 + day;
            int startIncludeMMDD = startInclude.x * 100 + startInclude.y;
            int endExcludeMMDD = endExclude.x * 100 + endExclude.y;

            if (startIncludeMMDD <= nowMMDD && nowMMDD < endExcludeMMDD)
            {
                // 날짜에 해당하는 경우
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}