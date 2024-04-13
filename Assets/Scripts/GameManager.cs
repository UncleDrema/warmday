using System.Collections.Generic;
using Game.Ui;
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
        private Button leaveBunkerButton;
        
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

        private void Awake()
        {
            Instance = this;
            _resources[ResourceType.Water] = 0;
            _resources[ResourceType.Food] = 0;
            _resources[ResourceType.Ammo] = 0;
            leaveBunkerButton.onClick.AddListener(LeaveBunker);
            UpdateResourcesUi();
        }

        private void Update()
        {
            nightCamera.nightDepth = timer.NightDepth;
            leaveBunkerButton.interactable = timer.IsNight();
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

        public Vector3 GetPlayerPosition()
        {
            return player.transform.position;
        }

        public void EnterBunker()
        {
            inBunker = true;
            bunkerUiPanel.SetActive(true);
            player.transform.position = new Vector3(0, 0, 10000);
        }

        public void LeaveBunker()
        {
            inBunker = false;
            player.transform.position = bunker.transform.position;
            bunkerUiPanel.SetActive(false);
        }
    }
}