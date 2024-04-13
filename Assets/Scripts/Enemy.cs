using System;
using Unity.Mathematics;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        public GameObject deadBodyPrefab;
        public EnemyArea area;
        public Vector3 target;
        public float speed = 0.5f;
        public float playerSeeBoost = 2.0f;
        public float rotateSpeed = 1f;
        public float timeBetweenPoints = 3;
        public float playerSeeDistanceSqr = 9f;
        public float timer = 0;

        private bool _seePlayer;
        private const float NearThreshold = 0.025f;

        private void Update()
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                NextTarget();
                timer = timeBetweenPoints;
            }

            CheckForPlayer();

            MoveToPoint();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.DrawWireDisc(transform.position, Vector3.forward, Mathf.Sqrt(playerSeeDistanceSqr));
        }  
#endif

        private void MoveToPoint()
        {
            var moveDirection = (target - transform.position);
            var isAtPoint = moveDirection.sqrMagnitude < NearThreshold;
            moveDirection.Normalize();

            var d = Vector3.Dot(moveDirection, Vector3.left);
            var f = Vector3.Dot(moveDirection, Vector3.up);
            var angle = 180 / Mathf.PI * Mathf.Acos(d) + 180;
            var nextAngle = angle;
            if (f > 0)
                nextAngle *= -1;
            nextAngle -= 90;
            
            if (!isAtPoint)
            {
                var targetRot = quaternion.Euler(new Vector3(0, 0, Mathf.PI / 180f * nextAngle));
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);
                var realSpeed = speed;
                if (_seePlayer)
                    realSpeed *= playerSeeBoost;
                transform.Translate(moveDirection * (realSpeed * Time.deltaTime), Space.World);
            }
        }

        private void CheckForPlayer()
        {
            var player = GameManager.Instance.GetPlayerPosition();
            if ((transform.position - player).sqrMagnitude <= playerSeeDistanceSqr)
            {
                _seePlayer = true;
                target = player;
            }
            else
            {
                _seePlayer = false;
            }
        }

        private void NextTarget()
        {
            target = area.GetRandomPosition();
        }

        public void Kill()
        {
            GameManager.Instance.soundPlayer.PlaySound(SoundType.ZombiDeath);
            Instantiate(deadBodyPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}