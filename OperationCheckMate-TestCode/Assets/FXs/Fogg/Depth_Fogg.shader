// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Depth_Fogg"
{
	Properties
	{
		_Fogg_Intensity("Fogg_Intensity", Range( 0 , 1)) = 0.5
		_Fogg_max_Intensity("Fogg_max_Intensity", Range( 0 , 1)) = 0
		_Fogg_Color("Fogg_Color", Color) = (1,0,0,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Untitled3copy("Untitled-3 copy", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow exclude_path:deferred noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _Fogg_Color;
		uniform sampler2D _Untitled3copy;
		uniform float4 _Untitled3copy_ST;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _Fogg_Intensity;
		uniform float _Fogg_max_Intensity;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 panner13 = ( 1.0 * _Time.y * float2( 0.05,0.025 ) + i.uv_texcoord);
			o.Albedo = tex2D( _TextureSample0, panner13 ).rgb;
			o.Emission = _Fogg_Color.rgb;
			float2 uv_Untitled3copy = i.uv_texcoord * _Untitled3copy_ST.xy + _Untitled3copy_ST.zw;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float eyeDepth3 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( ase_screenPos ))));
			float clampResult9 = clamp( ( abs( ( eyeDepth3 - ase_screenPos.w ) ) * _Fogg_Intensity ) , 0.0 , _Fogg_max_Intensity );
			o.Alpha = ( tex2D( _Untitled3copy, uv_Untitled3copy ) * clampResult9 ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16400
2239.2;132;1264;494;2141.413;318.4479;2.590016;True;True
Node;AmplifyShaderEditor.CommentaryNode;18;-1863.847,-2.052051;Float;False;1129.744;463.782;Comment;8;4;6;1;5;9;7;3;2;False depth;1,1,1,1;0;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;2;-1813.847,114.8654;Float;False;1;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenDepthNode;3;-1634.283,47.94794;Float;False;0;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;4;-1453.753,98.07501;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1;-1507.317,253.2699;Float;False;Property;_Fogg_Intensity;Fogg_Intensity;0;0;Create;True;0;0;False;0;0.5;0.062;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;6;-1250.822,145.6918;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1358.506,347.1299;Float;False;Property;_Fogg_max_Intensity;Fogg_max_Intensity;1;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-1068.295,162.9838;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-1111.403,-235.1666;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;34;-1047.85,493.201;Float;True;Property;_Untitled3copy;Untitled-3 copy;4;0;Create;True;0;0;False;0;0235663e659a2a14c89541758c8bc229;0235663e659a2a14c89541758c8bc229;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;9;-909.7031,254.4512;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;13;-771.5378,-208.5105;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.05,0.025;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;11;-409.5991,-244.1908;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;c6579124d2946a04ba0bcc2f6591eb7e;c6579124d2946a04ba0bcc2f6591eb7e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;10;-670.2961,-23.27876;Float;False;Property;_Fogg_Color;Fogg_Color;2;0;Create;True;0;0;False;0;1,0,0,0;0.1792453,0.1767088,0.1767088,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-325.8121,285.0258;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;30.74149,53.79761;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Depth_Fogg;False;False;False;False;True;True;True;True;True;True;True;True;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;4;0;3;0
WireConnection;4;1;2;4
WireConnection;6;0;4;0
WireConnection;5;0;6;0
WireConnection;5;1;1;0
WireConnection;9;0;5;0
WireConnection;9;2;7;0
WireConnection;13;0;12;0
WireConnection;11;1;13;0
WireConnection;37;0;34;0
WireConnection;37;1;9;0
WireConnection;0;0;11;0
WireConnection;0;2;10;0
WireConnection;0;9;37;0
ASEEND*/
//CHKSM=7743F782AB994096289F9E29A57944FEA10A3905