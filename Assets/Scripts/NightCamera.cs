using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class NightCamera : MonoBehaviour
    {
        private static readonly int FlashlightKey = Shader.PropertyToID("_Flashlight");
        private static readonly int PlayerLightKey = Shader.PropertyToID("_LightSourcePlayer");
        
        public Transform player;
        public Transform flashlight;
        public Material nightMaterial;
        private Camera _camera;
        private Vector3 offset;
        private float4 savedFlashlight;
        private float4 savedPlayer;

        [SerializeField]
        private Vector4 DayNightParams;
        
        [Range(0, 1)]
        public float nightDepth = 1f;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            offset = transform.position - player.position;
            savedFlashlight = nightMaterial.GetVector(FlashlightKey);
            savedPlayer = nightMaterial.GetVector(PlayerLightKey);
        }

        private void OnApplicationQuit()
        {
            nightMaterial.SetVector(FlashlightKey, savedFlashlight);
            nightMaterial.SetVector(PlayerLightKey, savedPlayer);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            UpdatePositionAndDayNight();
            UpdateRotation();
            Graphics.Blit(source, destination, nightMaterial);
        }

        private void UpdatePositionAndDayNight()
        {
            Vector2 screen = _camera.WorldToViewportPoint(flashlight.position);
            float4 playerLight = nightMaterial.GetVector(PlayerLightKey);
            playerLight.xy = screen;
            playerLight.zw = new float2(
                Mathf.Lerp(DayNightParams.x, DayNightParams.y, nightDepth),
                Mathf.Lerp(DayNightParams.z, DayNightParams.w, nightDepth)
                );
            nightMaterial.SetVector(PlayerLightKey, playerLight);
        }

        private void UpdateRotation()
        {
            var rot = player.rotation.eulerAngles.z - 90;
            float4 flashlightPar = nightMaterial.GetVector(FlashlightKey);
            flashlightPar.x = rot;
            nightMaterial.SetVector(FlashlightKey, flashlightPar);
        }

        private void LateUpdate()
        {
            transform.position = player.position + offset;
        }
    }
}