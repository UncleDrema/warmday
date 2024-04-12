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
        private MainUiController ui;
        
        private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        private void Awake()
        {
            Instance = this;
            _resources[ResourceType.Water] = 0;
            _resources[ResourceType.Food] = 0;
            _resources[ResourceType.Ammo] = 0;
            UpdateResourcesUi();
        }

        [Button]
        public void AddResource(ResourceType resourceType, int amount)
        {
            _resources[resourceType] += amount;
            UpdateResourcesUi();
        }

        private void UpdateResourcesUi()
        {
            foreach (var (res, amount) in _resources)
            {
                ui.SetResource(res, amount);
            }
        }
    }
}