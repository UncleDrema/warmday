using System;
using UnityEngine;

namespace Game.Ui
{
    public class MainUiController : MonoBehaviour
    {
        [SerializeField]
        private ResourceView waterView;
        [SerializeField]
        private ResourceView foodView;
        [SerializeField]
        private ResourceView ammoView;

        public void SetResource(ResourceType resourceType, int amount)
        {
            switch (resourceType)
            {
                case ResourceType.Water:
                    waterView.SetAmount(amount);
                    break;
                case ResourceType.Food:
                    foodView.SetAmount(amount);
                    break;
                case ResourceType.Ammo:
                    ammoView.SetAmount(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
            }
        }
    }
}