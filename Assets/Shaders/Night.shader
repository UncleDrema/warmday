Shader "Sprites/Night" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _AspectRatio ("Screen Aspect Ratio", Float) = 0
        _Flashlight ("Flashlight", Vector) = (0, 0.35, 0.4, 1)
        _LightSourcePlayer ("LightSourcePlayer", Vector) = (0, 0, 0, 0)
        _Flash ("Flash", Float) = 0
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;
            uniform float _AspectRatio;
            uniform float4 _LightSourcePlayer;
            uniform float4 _Flashlight;
            uniform float _Flash;
            
            float4 frag(v2f_img i) : COLOR {
                float2 uv = i.uv;
                float2 playerPosition = float2(0.5, 0.5);
                float2 flashlightPosition = _LightSourcePlayer.xy;
                float2 ratio = float2(1, 1 / _AspectRatio);
                float2 flashRelPos = (flashlightPosition - uv) / ratio;
                float2 flashDir = normalize(flashRelPos);
                float2 relPos = (playerPosition - uv) / ratio;
                float2 dir = normalize(relPos);
                float4 c = tex2D(_MainTex, uv);
                float delta = 0;
                float ray;

                // Поворот направления фонарика
                float angle = _Flashlight.x * 3.14159 / 180.0;
                float2 flashlightDir = float2(cos(angle), sin(angle));

                // Фрагментный код для создания конуса света от фонарика
                float angleFactor = dot(flashlightDir, flashDir);
                float coneFactor = smoothstep(cos(_Flashlight.y * 0.5), 1.0, angleFactor);
                ray = length(flashRelPos);
                delta += coneFactor * smoothstep(_Flashlight.z, 0, ray) * _Flashlight.w;

                // Фрагментный код для создания фонового освещения
                ray = length(relPos);
                delta += smoothstep(_LightSourcePlayer.z, 0, ray) * _LightSourcePlayer.w;

                delta = min(delta, 1);
                c.rgb *= delta + _Flash;
                return c;
            }
            ENDCG
        }
    }
}
