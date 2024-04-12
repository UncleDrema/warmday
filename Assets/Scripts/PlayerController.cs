using Game.Pickups;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody2D rb;
        public float speed = 1;
        public float rotSpeed = 1;
        public float collectRadius = 3;
        
        private readonly Collider2D[] _collectResults = new Collider2D[20];

        private void Update()
        {
            MoveByWasd();
            CollectPickups();
        }

        private void CollectPickups()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // По нажатию F собираем все Pickup в радиусе, у них есть коллайдер IsTrigger
                var size = Physics2D.OverlapCircleNonAlloc(transform.position, collectRadius, _collectResults);
                for (int i = 0; i < size; i++)
                {
                    if (_collectResults[i].TryGetComponent<Interactable>(out var pickup))
                    {
                        pickup.Interact();
                    }
                }
            }
        }

        private void MoveByWasd()
        {
            var direction = Vector3.zero;
            var rotDirection = 0.0f;

            if (Input.GetKey(KeyCode.A))
            {
                rotDirection += 1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotDirection -= 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.left;
            }

            if (Input.GetKey(KeyCode.S))
            {
                direction -= Vector3.left;
            }
            
            direction.Normalize();

            rotDirection *= rotSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotDirection);
            transform.Translate(direction * (speed * Time.deltaTime));
        }
    }
}