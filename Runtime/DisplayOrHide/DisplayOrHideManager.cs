using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeedrunGameUtils
{
    public class DisplayOrHideManager : SingletonMonoBehaviour<DisplayOrHideManager>
    {
        private void Start()
        {
            ExcludeAndInactiveConsideringDate();
            SceneManager.sceneLoaded += (scene, loadScene) => ExcludeAndInactiveConsideringDate();
        }

        void ExcludeAndInactiveConsideringDate()
        {
            DateTimeOffset dateTimeOffset = TimeUtils.GetNetworkDateTimeOffsetOrLocalAtKorea();
            int year = dateTimeOffset.Year;
            int month = dateTimeOffset.Month;
            int day = dateTimeOffset.Day;

            DisplayWhenYearMonthDayMatched[] displayersWhenYearMonthDayMatched = Resources.FindObjectsOfTypeAll<DisplayWhenYearMonthDayMatched>();
            foreach (DisplayWhenYearMonthDayMatched displayer in displayersWhenYearMonthDayMatched)
            {
                displayer.DisplayOrNot(year, month, day);
            }

            DisplayWhenMonthDayMatched[] displayersWhenMonthDayMatched = Resources.FindObjectsOfTypeAll<DisplayWhenMonthDayMatched>();
            foreach (DisplayWhenMonthDayMatched displayer in displayersWhenMonthDayMatched)
            {
                displayer.DisplayOrNot(month, day);
            }

            ExcludeWhenYearMonthDayUnmatched[] excludersWhenYearMonthDayUnmatched = Resources.FindObjectsOfTypeAll<ExcludeWhenYearMonthDayUnmatched>();
            foreach (ExcludeWhenYearMonthDayUnmatched excluder in excludersWhenYearMonthDayUnmatched)
            {
                excluder.DisplayOrNot(year, month, day);
            }

            ExcludeWhenMonthDayUnmatched[] excludersWhenMonthDayUnmatched = Resources.FindObjectsOfTypeAll<ExcludeWhenMonthDayUnmatched>();
            foreach (ExcludeWhenMonthDayUnmatched excluder in excludersWhenMonthDayUnmatched)
            {
                excluder.DisplayOrNot(month, day);
            }
        }
    }
}