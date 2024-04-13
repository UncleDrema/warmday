using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;
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
        private AudioClip[] playerDeathZombi;
        
        [SerializeField]
        private AudioClip[] playerDeathSun;
        
        [SerializeField]
        private AudioClip[] playerDeathNoSupplies;
        
        [SerializeField]
        private AudioClip[] playerDeathNotCollected;
        
        [SerializeField]
        private AudioClip[] zombiDeath;

        [SerializeField]
        private AudioClip[] collect;

        [SerializeField]
        private AudioClip[] win;

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
                case SoundType.PlayerDeathFromZombie:
                    PlayRandom(playerDeathZombi);
                    break;
                case SoundType.ZombiDeath:
                    PlayRandom(zombiDeath);
                    break;
                case SoundType.Collect:
                    PlayRandom(collect);
                    break;
                case SoundType.PlayerDeathFromSun:
                    PlayRandom(playerDeathSun);
                    break;
                case SoundType.PlayerDeathNotCollected:
                    PlayRandom(playerDeathNotCollected);
                    break;
                case SoundType.PlayerDeathNoSupplies:
                    PlayRandom(playerDeathNoSupplies);
                    break;
                case SoundType.Win:
                    PlayRandom(win);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sound), sound, null);
            }
        }
    }
}