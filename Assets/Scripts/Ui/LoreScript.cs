using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Ui
{
    public class LoreScript : MonoBehaviour
    {
        public AudioSource source;
        public AudioClip clip;
        public GameObject nextPage;
        public Button next;

        private void Awake()
        {
            next.onClick.AddListener(NextPage);
            Time.timeScale = 0;
        }

        private void NextPage()
        {
            source.Stop();
            if (nextPage != null)
            {
                nextPage.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                transform.parent.gameObject.SetActive(false);
            }
            
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (clip != null)
            {
                source.clip = clip;
                source.Play();
            }
        }
    }
}