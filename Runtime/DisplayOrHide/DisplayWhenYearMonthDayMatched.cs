using UnityEngine;

namespace NeedrunGameUtils
{
    public class DisplayWhenYearMonthDayMatched : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int startInclude;
        [SerializeField]
        private Vector3Int endExclude;

        public void DisplayOrNot(int year, int month, int day)
        {
            int nowYYYYMMDD = year * 10000 + month * 100 + day;
            int startIncludeYYYYMMDD = startInclude.x * 10000 + startInclude.y * 100 + startInclude.z;
            int endExcludeYYYYMMDD = endExclude.x * 10000 + endExclude.y * 100 + endExclude.z;

            if (startIncludeYYYYMMDD <= nowYYYYMMDD && nowYYYYMMDD < endExcludeYYYYMMDD)
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