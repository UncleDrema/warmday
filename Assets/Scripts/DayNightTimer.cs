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
                fill = 0;
            
            fillImage.fillAmount = fill;
        }

        private bool IsNight()
        {
            return fill >= 0.5;
        }

        public float NightDepth
        {
            get
            {
                var x = 2 * (fill - 0.5f);
                return 1 - Mathf.Pow(2 * x - 1, 10);
            }
        }
    }
}