Shader "Custom/ParticleShader"
{
    Properties
    {
        _MainTex("Noise", 2D) = "white" {}
        _DispTex("Texture", 2D) = "white" {}
        //Define properties for Start and End Color
        _ImageSize("Image Size", float) = 500
        _TilePos("Image Pos", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }
        //LOD 100

        Blend One OneMinusSrcAlpha
        ZWrite off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            sampler2D _MainTex;
            sampler2D _DispTex;
            uniform float4 _StartColor;
            uniform float4 _EndColor;
            uniform float _ImageSize;
            uniform float4 _TilePos;

            struct appdata
            {
                float4 vertex : POSITION;
                //Define UV data
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                //Define UV data
                float4 uv : TEXCOORD0;
            };


            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; //Correct this for particle shader

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {

                //Sample color from particle texture
                float4 col = tex2D(_DispTex, i.uv.xy) * tex2D(_MainTex, ((i.vertex.xy * _TilePos.ab) / (_ImageSize * _TilePos.z)) + _TilePos.xy);

                return (col);
            }
            ENDCG
        }
    }
}
