using UnityEngine;

namespace Game.Pickups
{
    public class ResourcePickup : Pickup
    {
        [SerializeField]
        private ResourceType resourceType;
        [SerializeField]
        private int amount;


        protected override void OnPickup()
        {
            GameManager.Instance.AddResource(resourceType, amount);
        }
    }
}