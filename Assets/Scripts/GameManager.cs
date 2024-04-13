using System;
using System.Collections.Generic;
using Game.Ui;
using TriInspector;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField]
        private NightCamera camera;
        
        [SerializeField]
        private MainUiController ui;

        [SerializeField]
        private PlayerController player;

        [SerializeField]
        private DayNightTimer timer;

        [SerializeField]
        public SoundPlayer soundPlayer;
        
        private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        private void Awake()
        {
            Instance = this;
            _resources[ResourceType.Water] = 0;
            _resources[ResourceType.Food] = 0;
            _resources[ResourceType.Ammo] = 0;
            UpdateResourcesUi();
        }

        private void Update()
        {
            camera.nightDepth = timer.NightDepth;
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
    }
}