using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class DayNightTimer : MonoBehaviour
    {
        public Image fillImage;
        public float dayDuration;
        public float nightDuration;
        public float start = 0;
        public float fill;
        public int dayNumber = 0;

        public bool dayNotFinished = false;

        private void Start()
        {
            fill = start;
        }

        private void Update()
        {
            float duration;
            if (fill < 0.5)
                duration = dayDuration;
            else
                duration = nightDuration;
            fill += Time.deltaTime / duration * 0.5f;
            if (fill > 1)
            {
                NextDay();
            }

            if (dayNotFinished && IsNight())
            {
                fill = 0.5001f;
            }
            fillImage.fillAmount = fill;
        }

        private void NextDay()
        {
            fill = 0;
            dayNumber++;
            dayNotFinished = true;
            GameManager.Instance.DayStarted();
        }

        public bool IsNight()
        {
            return fill >= 0.5;
        }

        public float NightDepth
        {
            get
            {
                if (!IsNight())
                {
                    return 0;
                }
                var x = 2 * (fill - 0.5f);
                return 1 - Mathf.Pow(2 * x - 1, 10);
            }
        }
    }
}