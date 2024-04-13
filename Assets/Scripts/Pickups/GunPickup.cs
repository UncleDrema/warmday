namespace Game.Pickups
{
    public class GunPickup : Pickup
    {
        protected override void OnPickup()
        {
            GameManager.Instance.AddResource(ResourceType.Ammo, 3);
            GameManager.Instance.FindGun();
        }
    }
}