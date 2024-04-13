namespace Game
{
    public class BunkerEnter : Interactable
    {
        public override void Interact()
        {
            GameManager.Instance.EnterBunker();
        }
    }
}