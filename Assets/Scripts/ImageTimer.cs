using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ImageTimer : MonoBehaviour
    {
        public float MaxTime;
        
        private Image img;
        private float currentTime;
        
        void Start()
        {
            img = GetComponent<Image>();
            currentTime = 0;
        }
    
        void Update()
        {
            currentTime += Time.deltaTime;
    
            if (currentTime >= MaxTime)
            {
                currentTime = 0;
            }
    
            img.fillAmount = currentTime / MaxTime;
        }
    }
}
