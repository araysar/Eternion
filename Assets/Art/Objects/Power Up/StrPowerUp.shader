// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "StrPowerUp"
{
	Properties
	{
		_Textura("Textura", 2D) = "white" {}
		_Color1("Color 1", Color) = (1,0,0.6873569,0)
		_Speed("Speed", Float) = 1
		_m("m", Range( 2 , 20)) = 3.764706
		_Color2("Color 2", Color) = (0.9293336,1,0,0)
		_ColorIntensity("Color Intensity", Float) = 2
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform float _Speed;
			uniform float _m;
			uniform float4 _Color1;
			uniform sampler2D _Textura;
			uniform float4 _Textura_ST;
			uniform float4 _Color2;
			uniform float _ColorIntensity;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float dotResult29 = dot( v.vertex.xyz.x , v.vertex.xyz.x );
				float dotResult30 = dot( v.vertex.xyz.z , v.vertex.xyz.z );
				float mulTime46 = _Time.y * _Speed;
				float temp_output_40_0 = ( sin( ( ( dotResult29 + dotResult30 + mulTime46 ) / 0.2 ) ) / _m );
				
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_texcoord2 = v.vertex;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( temp_output_40_0 * v.ase_normal );
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float2 uv_Textura = i.ase_texcoord1.xy * _Textura_ST.xy + _Textura_ST.zw;
				float4 tex2DNode35 = tex2D( _Textura, uv_Textura );
				float dotResult29 = dot( i.ase_texcoord2.xyz.x , i.ase_texcoord2.xyz.x );
				float dotResult30 = dot( i.ase_texcoord2.xyz.z , i.ase_texcoord2.xyz.z );
				float mulTime46 = _Time.y * _Speed;
				float temp_output_40_0 = ( sin( ( ( dotResult29 + dotResult30 + mulTime46 ) / 0.2 ) ) / _m );
				float4 lerpResult44 = lerp( ( _Color1 * tex2DNode35 ) , ( tex2DNode35 * _Color2 ) , ceil( temp_output_40_0 ));
				
				
				finalColor = ( lerpResult44 * _ColorIntensity );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;560;1082;433;892.6846;200.8807;1.395659;True;False
Node;AmplifyShaderEditor.RangedFloatNode;47;-1648.984,291.111;Inherit;False;Property;_Speed;Speed;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;28;-1684.605,123.3993;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;46;-1501.797,295.9855;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;30;-1479.381,202.5652;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;29;-1475.214,109.3954;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1252.304,160.7602;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1135.633,216.5716;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;0;False;0;False;0.2;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;33;-967.223,161.6464;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-971.4333,251.405;Inherit;False;Property;_m;m;3;0;Create;True;0;0;0;False;0;False;3.764706;3.764706;2;20;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;36;-844.9881,174.8044;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;35;-853.9498,-253.1019;Inherit;True;Property;_Textura;Textura;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;48;-776.1694,-57.989;Inherit;False;Property;_Color2;Color 2;4;0;Create;True;0;0;0;False;0;False;0.9293336,1,0,0;0.9293336,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;38;-773.8597,-426.6696;Inherit;False;Property;_Color1;Color 1;1;0;Create;True;0;0;0;False;0;False;1,0,0.6873569,0;1,0,0.2350664,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleDivideOpNode;40;-635.8113,184.5687;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-525.0908,-116.2963;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CeilOpNode;56;-509.1682,33.8951;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-508.1786,-309.6113;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-346.4676,20.25848;Inherit;False;Property;_ColorIntensity;Color Intensity;5;0;Create;True;0;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;39;-660.3445,283.3717;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;44;-333.7904,-103.386;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-351.2898,187.3274;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-133.5108,-48.51109;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;4.192147;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;20;30.94705,24.14413;Float;False;True;-1;2;ASEMaterialInspector;100;1;StrPowerUp;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;46;0;47;0
WireConnection;30;0;28;3
WireConnection;30;1;28;3
WireConnection;29;0;28;1
WireConnection;29;1;28;1
WireConnection;31;0;29;0
WireConnection;31;1;30;0
WireConnection;31;2;46;0
WireConnection;33;0;31;0
WireConnection;33;1;32;0
WireConnection;36;0;33;0
WireConnection;40;0;36;0
WireConnection;40;1;34;0
WireConnection;57;0;35;0
WireConnection;57;1;48;0
WireConnection;56;0;40;0
WireConnection;41;0;38;0
WireConnection;41;1;35;0
WireConnection;44;0;41;0
WireConnection;44;1;57;0
WireConnection;44;2;56;0
WireConnection;43;0;40;0
WireConnection;43;1;39;0
WireConnection;60;0;44;0
WireConnection;60;1;61;0
WireConnection;20;0;60;0
WireConnection;20;1;43;0
ASEEND*/
//CHKSM=9C7779D604867BB2B2B629172BFBA8336D8EEE81