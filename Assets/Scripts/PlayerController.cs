using System;
using Game.Pickups;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject noGunModel;
        public GameObject gunModel;
        public Transform gun;
        public Rigidbody2D rb;
        public float speed = 1;
        public float rotSpeed = 1;
        public float collectRadius = 3;
        public Vector2 shootBox = new Vector2(0.75f, 8);
        public bool isDead = false;
        
        private Transform myTransform;
        private readonly Collider2D[] _collectResults = new Collider2D[20];

        private void Awake()
        {
            myTransform = transform;
        }

        public void SetFoundGun(bool found)
        {
            gunModel.SetActive(found);
            noGunModel.SetActive(!found);
        }

        private void Update()
        {
            if (CanControl())
            {
                MoveByWasd();
                CollectPickups();
                if (GameManager.Instance.foundGun)
                {
                    TryShoot();
                }
            }
        }

        public bool CanControl()
        {
            return !GameManager.Instance.inBunker && !isDead;
        }

        private void TryShoot()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var rot = myTransform.rotation;
                var gunForward = rot * Vector3.up;
                var gunLeft = rot * Vector3.left;
                Vector2 gunPosition = gun.position + gunForward * (shootBox.y * 0.5f);
                DebugDrawBox(gunPosition, shootBox, transform.rotation.eulerAngles.z, Color.blue, 1);
                if (GameManager.Instance.GetResource(ResourceType.Ammo) < 1)
                {
                    FailShoot();
                }
                else
                {
                    Shoot(gunPosition, transform.rotation.eulerAngles.z);
                    GameManager.Instance.AddResource(ResourceType.Ammo, -1);
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.GetComponent<Enemy>())
            {
                Die(FailType.EatenByZombi);
            }
        }

        private void Die(FailType failType)
        {
            if (!isDead)
            {
                isDead = true;
                GameManager.Instance.Fail(failType);
            }
        }

        private void Shoot(Vector2 gunPos, float angle)
        {
            GameManager.Instance.soundPlayer.PlaySound(SoundType.ShootGun);
            var targets = Physics2D.OverlapBoxAll(gunPos, shootBox, angle);
            float minDist = float.MaxValue;
            Enemy minEnemy = null;
            foreach (var col in targets)
            {
                if (col.TryGetComponent(out Enemy enemy))
                {
                    float dist = Vector3.Distance(myTransform.position, enemy.transform.position);
                    if (dist < minDist)
                    {
                        minEnemy = enemy;
                        minDist = dist;
                    }
                }
            }

            if (minEnemy != null)
            {
                minEnemy.Kill();
            }
        }
        
        void DebugDrawBox( Vector2 point, Vector2 size, float angle, Color color, float duration) {

            var orientation = Quaternion.Euler(0, 0, angle);

            // Basis vectors, half the size in each direction from the center.
            Vector2 right = orientation * Vector2.right * size.x/2f;
            Vector2 up = orientation * Vector2.up * size.y/2f;

            // Four box corners.
            var topLeft = point + up - right;
            var topRight = point + up + right;
            var bottomRight = point - up + right;
            var bottomLeft = point - up - right;

            // Now we've reduced the problem to drawing lines.
            Debug.DrawLine(topLeft, topRight, color, duration);
            Debug.DrawLine(topRight, bottomRight, color, duration);
            Debug.DrawLine(bottomRight, bottomLeft, color, duration);
            Debug.DrawLine(bottomLeft, topLeft, color, duration);
        }

        private void FailShoot()
        {
            GameManager.Instance.soundPlayer.PlaySound(SoundType.BadGun);
        }

        private void CollectPickups()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                // По нажатию F собираем все Pickup в радиусе, у них есть коллайдер IsTrigger
                var size = Physics2D.OverlapCircleNonAlloc(transform.position, collectRadius, _collectResults);
                bool collectedOne = false;
                for (int i = 0; i < size; i++)
                {
                    if (_collectResults[i].TryGetComponent<Interactable>(out var interactable))
                    {
                        if (interactable is Pickup)
                            collectedOne = true;
                        interactable.Interact();
                    }
                }

                if (collectedOne)
                {
                    GameManager.Instance.soundPlayer.PlaySound(SoundType.Collect);
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
                direction += Vector3.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                direction -= Vector3.up;
            }
            
            direction.Normalize();

            rotDirection *= rotSpeed * Time.deltaTime;
            myTransform.Rotate(0, 0, rotDirection);
            myTransform.Translate(direction * (speed * Time.deltaTime));
        }
    }
}