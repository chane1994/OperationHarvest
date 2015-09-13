// Shader created with Shader Forge Beta 0.36 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.36;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:0,culm:0,dpts:2,wrdp:True,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.03067952,fgcg:0.04517287,fgcb:0.07352942,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32311,y:32734|diff-4-OUT,spec-31-OUT,gloss-56-OUT,normal-23-RGB,emission-78-OUT;n:type:ShaderForge.SFN_Tex2d,id:2,x:33146,y:32552,ptlb:Diffuse,ptin:_Diffuse,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:3,x:33146,y:32385,ptlb:Main Color,ptin:_MainColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:4,x:32855,y:32449|A-3-RGB,B-2-RGB;n:type:ShaderForge.SFN_Cubemap,id:10,x:33126,y:32807,ptlb:Reflection,ptin:_Reflection;n:type:ShaderForge.SFN_Color,id:11,x:33126,y:32974,ptlb:Reflection Color,ptin:_ReflectionColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_SwitchProperty,id:12,x:32757,y:32848,ptlb:Use Custom RefMask,ptin:_UseCustomRefMask,on:False|A-14-OUT,B-16-OUT;n:type:ShaderForge.SFN_Multiply,id:14,x:32918,y:32817|A-10-RGB,B-11-RGB,C-2-A;n:type:ShaderForge.SFN_Multiply,id:16,x:32942,y:33078|A-10-RGB,B-11-RGB,C-17-A;n:type:ShaderForge.SFN_Tex2d,id:17,x:33314,y:33028,ptlb:Custom Reflection Mask,ptin:_CustomReflectionMask,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:23,x:32886,y:32629,ptlb:Normal Map,ptin:_NormalMap,ntxv:3,isnm:True;n:type:ShaderForge.SFN_SwitchProperty,id:29,x:32613,y:32414,ptlb:Custom Specular,ptin:_CustomSpecular,on:True|A-2-A,B-30-A;n:type:ShaderForge.SFN_Tex2d,id:30,x:32788,y:32279,ptlb:Specular,ptin:_Specular,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:31,x:32340,y:32414|A-29-OUT,B-44-RGB,C-96-OUT;n:type:ShaderForge.SFN_Color,id:44,x:32590,y:32237,ptlb:Specular Color,ptin:_SpecularColor,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Slider,id:56,x:32408,y:32568,ptlb:Shininess,ptin:_Shininess,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Add,id:78,x:32605,y:32872|A-12-OUT,B-85-OUT;n:type:ShaderForge.SFN_Tex2d,id:79,x:32733,y:33089,ptlb:Emissive,ptin:_Emissive,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SwitchProperty,id:85,x:32542,y:33041,ptlb:Use Emissive,ptin:_UseEmissive,on:False|A-86-OUT,B-79-RGB;n:type:ShaderForge.SFN_Vector1,id:86,x:32614,y:33258,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:96,x:32388,y:32322,ptlb:Specular Level,ptin:_SpecularLevel,glob:False,v1:1;proporder:3-44-96-56-11-2-23-10-12-17-29-30-85-79;pass:END;sub:END;*/

Shader "ManyWorlds/BumpedReflectionEmissive" {
    Properties {
        _MainColor ("Main Color", Color) = (0.5,0.5,0.5,1)
        _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)
        _SpecularLevel ("Specular Level", Float ) = 1
        _Shininess ("Shininess", Range(0, 1)) = 0.5
        _ReflectionColor ("Reflection Color", Color) = (0.5,0.5,0.5,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _Reflection ("Reflection", Cube) = "_Skybox" {}
        [MaterialToggle] _UseCustomRefMask ("Use Custom RefMask", Float ) = 0
        _CustomReflectionMask ("Custom Reflection Mask", 2D) = "white" {}
        [MaterialToggle] _CustomSpecular ("Custom Specular", Float ) = 1
        _Specular ("Specular", 2D) = "white" {}
        [MaterialToggle] _UseEmissive ("Use Emissive", Float ) = 0
        _Emissive ("Emissive", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _MainColor;
            uniform samplerCUBE _Reflection;
            uniform float4 _ReflectionColor;
            uniform fixed _UseCustomRefMask;
            uniform sampler2D _CustomReflectionMask; uniform float4 _CustomReflectionMask_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform fixed _CustomSpecular;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform float4 _SpecularColor;
            uniform float _Shininess;
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform fixed _UseEmissive;
            uniform float _SpecularLevel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_105 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_105.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor + UNITY_LIGHTMODEL_AMBIENT.rgb;
////// Emissive:
                float4 node_10 = texCUBE(_Reflection,viewReflectDirection);
                float4 node_2 = tex2D(_Diffuse,TRANSFORM_TEX(node_105.rg, _Diffuse));
                float3 emissive = (lerp( (node_10.rgb*_ReflectionColor.rgb*node_2.a), (node_10.rgb*_ReflectionColor.rgb*tex2D(_CustomReflectionMask,TRANSFORM_TEX(node_105.rg, _CustomReflectionMask)).a), _UseCustomRefMask )+lerp( 0.0, tex2D(_Emissive,TRANSFORM_TEX(node_105.rg, _Emissive)).rgb, _UseEmissive ));
///////// Gloss:
                float gloss = _Shininess;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float3 specularColor = (lerp( node_2.a, tex2D(_Specular,TRANSFORM_TEX(node_105.rg, _Specular)).a, _CustomSpecular )*_SpecularColor.rgb*_SpecularLevel);
                float3 specular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_MainColor.rgb*node_2.rgb);
                finalColor += specular;
                finalColor += emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float4 _MainColor;
            uniform samplerCUBE _Reflection;
            uniform float4 _ReflectionColor;
            uniform fixed _UseCustomRefMask;
            uniform sampler2D _CustomReflectionMask; uniform float4 _CustomReflectionMask_ST;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform fixed _CustomSpecular;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform float4 _SpecularColor;
            uniform float _Shininess;
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform fixed _UseEmissive;
            uniform float _SpecularLevel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 binormalDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.tangentDir = normalize( mul( _Object2World, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.binormalDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.binormalDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float2 node_106 = i.uv0;
                float3 normalLocal = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(node_106.rg, _NormalMap))).rgb;
                float3 normalDirection =  normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = dot( normalDirection, lightDirection );
                float3 diffuse = max( 0.0, NdotL) * attenColor;
///////// Gloss:
                float gloss = _Shininess;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                NdotL = max(0.0, NdotL);
                float4 node_2 = tex2D(_Diffuse,TRANSFORM_TEX(node_106.rg, _Diffuse));
                float3 specularColor = (lerp( node_2.a, tex2D(_Specular,TRANSFORM_TEX(node_106.rg, _Specular)).a, _CustomSpecular )*_SpecularColor.rgb*_SpecularLevel);
                float3 specular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow) * specularColor;
                float3 finalColor = 0;
                float3 diffuseLight = diffuse;
                finalColor += diffuseLight * (_MainColor.rgb*node_2.rgb);
                finalColor += specular;
/// Final Color:
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
