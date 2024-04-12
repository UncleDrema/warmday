using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class NightCamera : MonoBehaviour
    {
        private static readonly int FlashlightKey = Shader.PropertyToID("_Flashlight");
        
        public Transform player;
        public Material nightMaterial;
        private Vector3 offset;

        private void Awake()
        {
            offset = transform.position - player.position;
            nightMaterial = new Material(nightMaterial);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            var rot = player.rotation.eulerAngles.z;
            float4 flashlight = nightMaterial.GetVector(FlashlightKey);
            flashlight.x = rot;
            nightMaterial.SetVector(FlashlightKey, flashlight);
            Graphics.Blit(source, destination, nightMaterial);
        }

        private void LateUpdate()
        {
            transform.position = player.position + offset;
        }
    }
}