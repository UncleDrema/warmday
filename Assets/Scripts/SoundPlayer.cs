using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField]
        private AudioSource source;
        
        [SerializeField]
        private AudioClip[] badGun;
        
        [SerializeField]
        private AudioClip[] shootGun;

        [SerializeField]
        private AudioClip[] playerDeath;
        
        [SerializeField]
        private AudioClip[] zombiDeath;

        [SerializeField]
        private AudioClip[] collect;

        private void PlayRandom(AudioClip[] clips)
        {
            var rand = clips[Random.Range(0, clips.Length)];
            source.PlayOneShot(rand);
        }

        [Button]
        public void PlaySound(SoundType sound)
        {
            switch (sound)
            {
                case SoundType.BadGun:
                    PlayRandom(badGun);
                    break;
                case SoundType.ShootGun:
                    PlayRandom(shootGun);
                    break;
                case SoundType.PlayerDeath:
                    PlayRandom(playerDeath);
                    break;
                case SoundType.ZombiDeath:
                    PlayRandom(zombiDeath);
                    break;
                case SoundType.Collect:
                    PlayRandom(collect);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sound), sound, null);
            }
        }
    }
}