using System;
using System.Collections.Generic;
using System.Linq;
using Game.Ui;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private DeathController deathControllerUi;
        
        [SerializeField]
        private TextMeshProUGUI daysLeft;

        [SerializeField]
        private Button leaveBunkerButton;

        [SerializeField]
        private GameObject dangerPanel;
        
        [SerializeField]
        public GameObject bunkerUiPanel;
        
        [SerializeField]
        private NightCamera nightCamera;
        
        [SerializeField]
        private MainUiController ui;

        [SerializeField]
        private PlayerController player;

        [SerializeField]
        private DayNightTimer timer;

        [SerializeField]
        public SoundPlayer soundPlayer;

        [SerializeField]
        public BunkerEnter bunker;

        public bool inBunker = true;
        
        private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();
        public bool foundGun = false;
        public Image gunImage;

        public bool[] foundParts = new bool[5];
        public Image[] imageParts = new Image[5];

        public int totalDays = 7;

        public void FindGun()
        {
            foundGun = true;
            gunImage.color = Color.white;
            player.SetFoundGun(foundGun);
        }

        public void FindPart(int partIndex)
        {
            foundParts[partIndex] = true;
            imageParts[partIndex].color = Color.white;
        }

        private void Awake()
        {
            Instance = this;
            _resources[ResourceType.Water] = 1;
            _resources[ResourceType.Food] = 1;
            _resources[ResourceType.Ammo] = 0;
            leaveBunkerButton.onClick.AddListener(LeaveBunker);
            UpdateResourcesUi();
            timer.dayNotFinished = true;
            deathControllerUi.Hide();
        }

        private void Update()
        {
            nightCamera.nightDepth = timer.NightDepth;
            dangerPanel.SetActive(!timer.IsNight());
        }

        [Button]
        public void AddResource(ResourceType resourceType, int amount)
        {
            _resources[resourceType] += amount;
            UpdateResourcesUi();
        }

        public int GetResource(ResourceType resourceType)
        {
            return _resources[resourceType];
        }

        private void UpdateResourcesUi()
        {
            foreach (var (res, amount) in _resources)
            {
                ui.SetResource(res, amount);
            }
        }

        public PlayerController GetPlayer()
        {
            return player;
        }

        public void EnterBunker()
        {
            inBunker = true;
            bunkerUiPanel.SetActive(true);
        }

        public void LeaveBunker()
        {
            inBunker = false;
            player.transform.position = bunker.transform.position;
            bunkerUiPanel.SetActive(false);
            timer.dayNotFinished = false;
            CheckOnSun();
        }

        public void Fail(FailType failType)
        {
            switch (failType)
            {
                case FailType.EatenByZombi:
                    soundPlayer.PlaySound(SoundType.PlayerDeathFromZombie);
                    break;
                case FailType.NotCollected:
                    soundPlayer.PlaySound(SoundType.PlayerDeathNotCollected);
                    break;
                case FailType.Sun:
                    soundPlayer.PlaySound(SoundType.PlayerDeathFromSun);
                    break;
                case FailType.NoSupplies:
                    soundPlayer.PlaySound(SoundType.PlayerDeathNoSupplies);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(failType), failType, null);
            }
            
            deathControllerUi.Show(failType);
        }

        public void DayStarted()
        {
            var dayNumber = timer.dayNumber;
            var left = totalDays - dayNumber;
            daysLeft.text = left.ToString();

            CheckOnSun();
            SpendSupplies();
            
            if (left == 0)
                CheckWin();
        }

        private void CheckWin()
        {
            if (foundParts.All(b => b))
            {
                Win();
            }
            else
            {
                player.Die(FailType.NotCollected);
            }
        }

        private void Win()
        {
            soundPlayer.PlaySound(SoundType.Win);
        }

        private void SpendSupplies()
        {
            if (GetResource(ResourceType.Food) <= 0 || GetResource(ResourceType.Water) <= 0)
            {
                player.Die(FailType.NoSupplies);
            }
            else
            {
                AddResource(ResourceType.Food, -1);
                AddResource(ResourceType.Water, -1);
            }
        }

        private void CheckOnSun()
        {
            if (!inBunker && !timer.IsNight())
            {
                player.Die(FailType.Sun);
            }
        }
    }
}