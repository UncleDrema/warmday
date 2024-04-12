using UnityEngine;

namespace Game.Pickups
{
    public class DebugPickup : Pickup
    {
        protected override void OnPickup()
        {
            Debug.Log("Pickup!");
        }
    }
}