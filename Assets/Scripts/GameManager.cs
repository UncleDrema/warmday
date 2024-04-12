using System.Collections.Generic;
using Game.Ui;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private MainUiController ui;
        
        private readonly Dictionary<ResourceType, int> _resources = new Dictionary<ResourceType, int>();

        private void Awake()
        {
            _resources[ResourceType.Water] = 0;
            _resources[ResourceType.Food] = 0;
            _resources[ResourceType.Ammo] = 0;
            UpdateResourcesUi();
        }

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