using System;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 1;
        public float rotSpeed = 1;

        private void Update()
        {
            MoveByWasd();
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