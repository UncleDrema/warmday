using UnityEngine;

namespace Game.Pickups
{
    public abstract class Pickup : Interactable
    {
        public override void Interact()
        {
            Collect();
        }

        public void Collect()
        {
            OnPickup();
            Destroy(gameObject);
        }
        
        protected abstract void OnPickup();
    }
}