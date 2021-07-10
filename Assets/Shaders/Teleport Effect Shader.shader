// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "KFC/Teleport Effect Shader"
{
    Properties
    {
        _MinY ("Min Y", Float) = -1
        _MaxY ("Max Y", Float) = 1
        _AnimTime("Animation Time", Float) = 0.5
        [Toggle] _InverseDirection("Inverse Direction", Float) = 0
        _DistortionSize("Distortion Size", Float) = 0.1
        _Albedo("Albedo", 2D) = "white" {}
        _AlbedoTint("Albedo Tint", Color) = (1, 1, 1, 1)
        _Metallic("Metallic", 2D) = "transparent" {}
        _Smoothness("Smoothness", Range(0.0, 1.0)) = 0
        _NormalMap("Normal Map", 2D) = "bump" {}
        [HDR]_GlowColor("Glow Color", Color) = (0, 0, 1, 1)
        _GlowWidth("Glow Width", Float) = 0.01
        _NoiseTex ("Noise Texture", 2D) = "white" {}
    }
        SubShader
    {
        Pass
        {
            Name "META"
            Tags {"LightMode" = "Meta"}
            Cull Back
            ZTest LEqual
            CGPROGRAM

            #include"UnityStandardMeta.cginc"

            sampler2D _GIAlbedoTex;
            fixed4 _GIAlbedoColor;
            float4 frag_meta2(v2f_meta i) : SV_Target
            {
                // We're interested in diffuse & specular colors
                // and surface roughness to produce final albedo.

                FragmentCommonData data = UNITY_SETUP_BRDF_INPUT(i.uv);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT(UnityMetaInput, o);
                fixed4 c = tex2D(_GIAlbedoTex, i.uv);
                o.Albedo = fixed3(c.rgb * _GIAlbedoColor.rgb);
                o.Emission = Emission(i.uv.xy);
                return UnityMetaFragment(o);
            }

#pragma vertex vert_meta
#pragma fragment frag_meta2
#pragma shader_feature _EMISSION
#pragma shader_feature _METALLICGLOSSMAP
#pragma shader_feature ___ _DETAIL_MULX2
                ENDCG
    }

        Pass
            {
            Tags { "LightMode" = "ShadowCaster" "Queue" = "Geometry+10"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : POSITION1;
            };

            #include "ShaderLib/Teleport.cginc"

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                teleportData td = getTeleportData(i.worldPos);
                if (td.alpha == 0)
                {
                    discard;
                }
                return float4(0, 0, 0, 1);
            }
            ENDCG
        }

        Tags { "RenderType" = "Opaque" "Queue" = "Geometry+10"}
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Standard

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)  

        #include "ShaderLib/Teleport.cginc"

        struct Input
        {
            float3 worldPos;
            float3 worldNormal; INTERNAL_DATA
            float2 uv_Albedo : TEXCOORD0;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            teleportData td = getTeleportData(IN.worldPos);
            if (td.alpha == 0)
            {
                discard;
            }
            if (td.emission)
            {
                o.Emission = _GlowColor;
            }
            o.Alpha = td.alpha;
            o.Albedo = tex2D(_Albedo, IN.uv_Albedo) * _AlbedoTint;
            fixed4 metal = tex2D(_Metallic, IN.uv_Albedo);
            o.Metallic = metal.r;
            o.Smoothness = metal.a * _Smoothness;

            float3 worldInterpolatedNormalVector = WorldNormalVector(IN, float3(0, 0, 1));

            o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_Albedo));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
