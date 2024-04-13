namespace Game.Pickups
{
    public class PartPickup : Pickup
    {
        public int partIndex;
        
        protected override void OnPickup()
        {
            GameManager.Instance.FindPart(partIndex);
        }
    }
}