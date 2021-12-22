Shader "AllIn1SpriteShader/AllIn1SpriteShaderScaledTime"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}	//0
		_Color("Main Color", Color) = (1,1,1,1)		//1
		_Alpha("General Alpha",  Range(0,1)) = 1	//2

		_GlowColor("Glow Color", Color) = (1,1,1,1) //3
		_Glow("Glow Intensity", Range(0,100)) = 10 //4
		[Space]
		[Space]
		[Toggle()] _GlowTexUsed("Glow Texture used?", float) = 0 //5
		_GlowTex("Glow Texture", 2D) = "white" {} //6

		_FadeTex("Fade Texture", 2D) = "white" {} //7
		_FadeAmount("Fade Amount",  Range(-0.1,1)) = -0.1 //8
		_FadeBurnWidth("Fade Burn Width",  Range(0,1)) = 0.025 //9
		_FadeBurnTransition("Burn Transition",  Range(0.01,0.5)) = 0.075 //10
		_FadeBurnColor("Fade Burn Color", Color) = (1,1,0,1) //11
		_FadeBurnTex("Fade Burn Texture", 2D) = "white" {} //12
		_FadeBurnGlow("Fade Burn Glow",  Range(1,250)) = 2//13

		_OutlineColor("Outline Base Color", Color) = (1,1,1,1) //14
		_OutlineAlpha("Outline Base Alpha",  Range(0,1)) = 1 //15
		_OutlineGlow("Outline Base Glow", Range(1,100)) = 1.5 //16
		[Toggle()] _Outline8Directions("Outline High Resolution?", float) = 0 //17
		_OutlineWidth("Outline Base Width", Range(0,0.2)) = 0.004 //18
		[Toggle()] _OutlineIsPixel("Outline is Pixel Perfect?", float) = 0 //19
		_OutlinePixelWidth("Outline Base Pixel Width", Int) = 1 //20
		
		[Space]
		[Toggle()] _OutlineTexToggle("Outline uses texture?", float) = 0 //21
		_OutlineTex("Outline Texture", 2D) = "white" {} //22
		_OutlineTexXSpeed("Texture scroll speed X", Range(-50,50)) = 10 //23
		_OutlineTexYSpeed("Texture scroll speed Y", Range(-50,50)) = 0 //24
		[Toggle()] _OutlineTexGrey("Texture is Greyscaled?", float) = 0 //25

		[Space]
		[Toggle()] _OutlineDistortToggle("Outline uses distortion?", float) = 0 //26
		_OutlineDistortTex("Outline Distortion Texture", 2D) = "white" {} //27
		_OutlineDistortAmount("Outline Distortion Amount", Range(0,2)) = 0.5 //28
		_OutlineDistortTexXSpeed("Distortion scroll speed X", Range(-50,50)) = 5 //29
		_OutlineDistortTexYSpeed("Distortion scroll speed Y", Range(-50,50)) = 5 //30

		_GradBlend("Gradient Blend", Range(0,1)) = 1 //31
		_GradTopLeftCol("Top Color", Color) = (1,0,0,1) //32
		_GradTopRightCol("Top Color 2", Color) = (1,0,0,1) //33
		_GradBotLeftCol("Bot Color", Color) = (0,0,1,1) //34
		_GradBotRightCol("Bot Color 2", Color) = (0,0,1,1) //35

		_ColorSwapTex("Color Swap Texture", 2D) = "black" {} //36
		[HDR] _ColorSwapRed("Red Channel", Color) = (1,1,1,1) //37
		_ColorSwapRedLuminosity("Red luminosity",  Range(-1,1)) = 0.5 //38
		[HDR] _ColorSwapGreen("Green Channel", Color) = (1,1,1,1) //39
		_ColorSwapGreenLuminosity("Green luminosity",  Range(-1,1)) = 0.5 //40
		[HDR] _ColorSwapBlue("Blue Channel", Color) = (1,1,1,1) //41
		_ColorSwapBlueLuminosity("Blue luminosity",  Range(-1,1)) = 0.5 //42

		_HsvShift("Hue Shift", Range(0, 360)) = 180 //43
		_HsvSaturation("Saturation", Range(0, 2)) = 1 //44
		_HsvBright("Brightness", Range(0, 2)) = 1 //45

		_HitEffectColor("Hit Effect Color", Color) = (1,1,1,1) //46
		_HitEffectGlow("Glow Intensity", Range(1,100)) = 5 //47
		[Space]
		_HitEffectBlend("Hit Effect Blend", Range(0,1)) = 1 //48

		_NegativeAmount("Negative Amount", Range(0, 1)) = 1 //49

		_PixelateSize("Pixelate size", Range(4,512)) = 32 //50

		_ColorRampTex("Color ramp Texture", 2D) = "white" {} //51
		_ColorRampLuminosity("Color ramp luminosity",  Range(-1,1)) = 0 //52
		[Toggle()] _ColorRampOutline("Affects everything?", float) = 0 //53

		_GreyscaleLuminosity("Greyscale luminosity",  Range(-1,1)) = 0 //54
		[Toggle()] _GreyscaleOutline("Affects everything?", float) = 0 //55
		_GreyscaleTintColor("Greyscale Tint Color", Color) = (1,1,1,1) //56

		_PosterizeNumColors("Number of Colors",  Range(0,100)) = 8 //57
		_PosterizeGamma("Posterize Amount",  Range(0.1,10)) = 0.75 //58
		[Toggle()] _PosterizeOutline("Affects everything?", float) = 0 //59

		_BlurIntensity("Blur Intensity",  Range(0,100)) = 10 //60
		[Toggle()] _BlurHD("Blur is Low Res?", float) = 0 //61

		_MotionBlurAngle("Motion Blur Angle", Range(-1, 1)) = 0.1 //62
		_MotionBlurDist("Motion Blur Distance", Range(-3, 3)) = 1.25 //63

		_GhostColorBoost("Ghost Color Boost",  Range(0,5)) = 1 //64
		_GhostTransparency("Ghost Transparency",  Range(0,1)) = 0 //65

		_InnerOutlineColor("Inner Outline Color", Color) = (1,0,0,1) //66
		_InnerOutlineThickness("Outline Thickness",  Range(0,3)) = 1 //67
		_InnerOutlineAlpha("Inner Outline Alpha",  Range(0,1)) = 1 //68
		_InnerOutlineGlow("Inner Outline Glow",  Range(1,250)) = 4 //69

		_AlphaCutoffValue("Alpha cutoff value", Range(0, 1)) = 0.25 //70

		[Toggle()] _OnlyOutline("Only render outline?", float) = 0 //71
		[Toggle()] _OnlyInnerOutline("Only render inner outline?", float) = 0 //72

		_HologramStripesAmount("Stripes Amount", Range(0, 1)) = 0.1 //73
		_HologramUnmodAmount("Unchanged Amount", Range(0, 1)) = 0.0 //74
		_HologramStripesSpeed("Stripes Speed", Range(-20, 20)) = 4.5 //75
		_HologramMinAlpha("Min Alpha", Range(0, 1)) = 0.1 //76
		_HologramMaxAlpha("Max Alpha", Range(0, 100)) = 0.75 //77

		_ChromAberrAmount("ChromAberr Amount", Range(0, 1)) = 1 //78
		_ChromAberrAlpha("ChromAberr Alpha", Range(0, 1)) = 0.4 //79

		_GlitchAmount("Glitch Amount", Range(0, 20)) = 3 //80

		_FlickerPercent("Flicker Percent", Range(0, 1)) = 0.05 //81
		_FlickerFreq("Flicker Frequency", Range(0, 5)) = 0.2 //82
		_FlickerAlpha("Flicker Alpha", Range(0, 1)) = 0 //83

		_ShadowX("Shadow X Axis", Range(-0.5, 0.5)) = 0.1 //84
		_ShadowY("Shadow Y Axis", Range(-0.5, 0.5)) = -0.05 //85
		_ShadowAlpha("Shadow Alpha", Range(0, 1)) = 0.5 //86
		_ShadowColor("Shadow Color", Color) = (0, 0, 0, 1) //87

		_HandDrawnAmount("Hand Drawn Amount", Range(0, 20)) = 10 //88
		_HandDrawnSpeed("Hand Drawn Speed", Range(1, 15)) = 5 //89

		_GrassSpeed("Speed", Range(0,50)) = 2 //90
		_GrassWind("Bend amount", Range(0,50)) = 20 //91
		[Space]
		[Toggle()] _GrassManualToggle("Manually animated?", float) = 0 //92
		_GrassManualAnim("Manual Anim Value", Range(-1,1)) = 1 //93

		_WaveAmount("Wave Amount", Range(0, 25)) = 7 //94
		_WaveSpeed("Wave Speed", Range(0, 25)) = 10 //95
		_WaveStrength("Wave Strength", Range(0, 25)) = 7.5 //96
		_WaveX("Wave X Axis", Range(0, 1)) = 0 //97
		_WaveY("Wave Y Axis", Range(0, 1)) = 0.5 //98

		_RectSize("Rect Size", Range(1, 4)) = 1 //99

		_OffsetUvX("X axis", Range(-1, 1)) = 0 //100
		_OffsetUvY("Y axis", Range(-1, 1)) = 0 //101

		_ClipUvLeft("Clipping Left", Range(0, 1)) = 0 //102
		_ClipUvRight("Clipping Right", Range(0, 1)) = 0 //103
		_ClipUvUp("Clipping Up", Range(0, 1)) = 0 //104
		_ClipUvDown("Clipping Down", Range(0, 1)) = 0 //105

		_TextureScrollXSpeed("Speed X Axis", Range(-5, 5)) = 1 //106
		_TextureScrollYSpeed("Speed Y Axis", Range(-5, 5)) = 0 //107

		_ZoomUvAmount("Zoom Amount", Range(0.1, 5)) = 0.5 //108

		_DistortTex("Distortion Texture", 2D) = "white" {} //109
		_DistortAmount("Distortion Amount", Range(0,2)) = 0.5 //110
		_DistortTexXSpeed("Scroll speed X", Range(-50,50)) = 5 //111
		_DistortTexYSpeed("Scroll speed Y", Range(-50,50)) = 5 //112

		_TwistUvAmount("Twist Amount", Range(0, 3.1416)) = 1 //113
		_TwistUvPosX("Twist Pos X Axis", Range(0, 1)) = 0.5 //114
		_TwistUvPosY("Twist Pos Y Axis", Range(0, 1)) = 0.5 //115
		_TwistUvRadius("Twist Radius", Range(0, 3)) = 0.75 //116

		_RotateUvAmount("Rotate Angle(radians)", Range(0, 6.2831)) = 0 //117

		_FishEyeUvAmount("Fish Eye Amount", Range(0, 0.5)) = 0.35 //118

		_PinchUvAmount("Pinch Amount", Range(0, 0.5)) = 0.35 //119

		_ShakeUvSpeed("Shake Speed", Range(0, 20)) = 2.5 //120
		_ShakeUvX("X Multiplier", Range(0, 5)) = 1.5 //121
		_ShakeUvY("Y Multiplier", Range(0, 5)) = 1 //122

		_ColorChangeTolerance("Tolerance", Range(0, 1)) = 0.25 //123
		_ColorChangeTarget("Color to change", Color) = (1, 0, 0, 1) //124
		[HDR] _ColorChangeNewCol("New Color", Color) = (1, 1, 0, 1) //125
		_ColorChangeLuminosity("New Color Luminosity", Range(0, 1)) = 0.0 //126

		_RoundWaveStrength("Wave Strength", Range(0, 1)) = 0.7 //127
		_RoundWaveSpeed("Wave Speed", Range(0, 5)) = 2 //128

		[Toggle()] _BillboardY("Billboard on both axis?", float) = 0 //129
		_ZWrite ("Depth Write", Float) = 0.0 // 130

		_MySrcMode ("SrcMode", Float) = 5 // 131
		_MyDstMode ("DstMode", Float) = 10 // 132

        _ShineColor("Shine Color", Color) = (1,1,1,1) // 133
        _ShineLocation("Shine Location", Range(0,1)) = 0.5 // 134
        _ShineRotate("Rotate Angle(radians)", Range(0, 6.2831)) = 0 //135
        _ShineWidth("Shine Width", Range(0.05,1)) = 0.1 // 136
        _ShineGlow("Shine Glow", Range(0,100)) = 1 // 137
		_ShineMask("Shine Mask", 2D) = "white" {} // 138

		_GlitchSize("Glitch Size", Range(0.25, 5)) = 1 //139
		_HologramStripeColor("Stripes Color", Color) = (0,1,1,1) //140
		_GradBoostX("Boost X axis", Range(0.1, 5)) = 1.2 //141
		_GradBoostY("Boost Y axis", Range(0.1, 5)) = 1.2 //142
		[Toggle()] _GradIsRadial("Radial Gradient?", float) = 0 //143
		_AlphaRoundThreshold("Round Threshold", Range(0.005, 1.0)) = 0.5 //144
		_GrassRadialBend("Radial Bend", Range(0.0, 5.0)) = 0.1 //145

		_ColorChangeTolerance2("Tolerance 2", Range(0, 1)) = 0.25 //146
		_ColorChangeTarget2("Color to change 2", Color) = (1, 0, 0, 1) //147
		[HDR] _ColorChangeNewCol2("New Color 2", Color) = (1, 1, 0, 1) //148
		_ColorChangeTolerance3("Tolerance 3", Range(0, 1)) = 0.25 //149
		_ColorChangeTarget3("Color to change 3", Color) = (1, 0, 0, 1) //150
		[HDR] _ColorChangeNewCol3("New Color 3", Color) = (1, 1, 0, 1) //151

		_Contrast ("Contrast", Range(0, 6)) = 1 // 152
		_Brightness ("Brightness", Range(-1, 1)) = 0 // 153

		_ColorSwapBlend ("Color Swap Blend", Range(0, 1)) = 1 // 154
		_ColorRampBlend ("Color Ramp Blend", Range(0, 1)) = 1 // 155
		_GreyscaleBlend ("Greyscale Blend", Range(0, 1)) = 1 // 156
		_GhostBlend ("Ghost Blend", Range(0, 1)) = 1 // 157
		_HologramBlend ("Hologram Blend", Range(0, 1)) = 1 // 158

		_AlphaOutlineColor("Color", Color) = (1, 1, 1, 1) //159
		_AlphaOutlineGlow("Outline Glow", Range(1,100)) = 5 //160
		_AlphaOutlinePower("Power", Range(0, 5)) = 1 // 161
		_AlphaOutlineMinAlpha("Min Alpha", Range(0, 1)) = 0 // 162
		_AlphaOutlineBlend("Blend", Range(0, 1)) = 1 // 163

		[AllIn1ShaderGradient] _ColorRampTexGradient("Color ramp Gradient", 2D) = "white" {} //164

		_OverlayTex("Overlay Texture", 2D) = "white" {} //165
		_OverlayColor("Overlay Color", Color) = (1, 1, 1, 1) //166
		_OverlayGlow("Overlay Glow", Range(0,25)) = 1 // 167
		_OverlayBlend("Overlay Blend", Range(0, 1)) = 1 // 168

		_ZTestMode ("Z Test Mode", Float) = 4

		[HideInInspector] _MinXUV("_MinXUV", Range(0, 1)) = 0.0
		[HideInInspector] _MaxXUV("_MaxXUV", Range(0, 1)) = 1.0
		[HideInInspector] _MinYUV("_MinYUV", Range(0, 1)) = 0.0
		[HideInInspector] _MaxYUV("_MaxYUV", Range(0, 1)) = 1.0
		[HideInInspector] _RandomSeed("_MaxYUV", Range(0, 10000)) = 0.0
    }

    SubShader
    {
		Tags { "Queue" = "Transparent" "CanUseSpriteAtlas" = "True" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
		Blend [_MySrcMode] [_MyDstMode]
		Cull Off
		ZWrite [_ZWrite]
		ZTest [_ZTestMode]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

			#pragma shader_feature GLOW_ON
			#pragma shader_feature FADE_ON
			#pragma shader_feature OUTBASE_ON
			#pragma shader_feature ONLYOUTLINE_ON
			#pragma shader_feature GRADIENT_ON
			#pragma shader_feature GRADIENT2COL_ON
			#pragma shader_feature RADIALGRADIENT_ON
			#pragma shader_feature COLORSWAP_ON
			#pragma shader_feature HSV_ON
			#pragma shader_feature CHANGECOLOR_ON
			#pragma shader_feature CHANGECOLOR2_ON
			#pragma shader_feature CHANGECOLOR3_ON
			#pragma shader_feature COLORRAMP_ON
			#pragma shader_feature GRADIENTCOLORRAMP_ON
			#pragma shader_feature HITEFFECT_ON
			#pragma shader_feature NEGATIVE_ON
			#pragma shader_feature PIXELATE_ON
			#pragma shader_feature GREYSCALE_ON
			#pragma shader_feature POSTERIZE_ON
			#pragma shader_feature BLUR_ON
			#pragma shader_feature MOTIONBLUR_ON
			#pragma shader_feature GHOST_ON
			#pragma shader_feature ALPHAOUTLINE_ON
			#pragma shader_feature INNEROUTLINE_ON
			#pragma shader_feature ONLYINNEROUTLINE_ON
			#pragma shader_feature HOLOGRAM_ON
			#pragma shader_feature CHROMABERR_ON
			#pragma shader_feature GLITCH_ON
			#pragma shader_feature FLICKER_ON
			#pragma shader_feature SHADOW_ON
			#pragma shader_feature SHINE_ON
			#pragma shader_feature CONTRAST_ON
			#pragma shader_feature OVERLAY_ON
			#pragma shader_feature OVERLAYMULT_ON
			#pragma shader_feature ALPHACUTOFF_ON
			#pragma shader_feature ALPHAROUND_ON
			#pragma shader_feature DOODLE_ON
			#pragma shader_feature WIND_ON
			#pragma shader_feature WAVEUV_ON
			#pragma shader_feature ROUNDWAVEUV_ON
			#pragma shader_feature RECTSIZE_ON
			#pragma shader_feature OFFSETUV_ON
			#pragma shader_feature CLIPPING_ON
			#pragma shader_feature TEXTURESCROLL_ON
			#pragma shader_feature ZOOMUV_ON
			#pragma shader_feature DISTORT_ON
			#pragma shader_feature TWISTUV_ON
			#pragma shader_feature ROTATEUV_ON
			#pragma shader_feature POLARUV_ON
			#pragma shader_feature FISHEYE_ON
			#pragma shader_feature PINCH_ON
			#pragma shader_feature SHAKEUV_ON

			#pragma shader_feature GLOWTEX_ON
			#pragma shader_feature OUTTEX_ON
			#pragma shader_feature OUTDIST_ON
			#pragma shader_feature OUTBASE8DIR_ON
			#pragma shader_feature OUTBASEPIXELPERF_ON
			#pragma shader_feature OUTGREYTEXTURE_ON
			#pragma shader_feature COLORRAMPOUTLINE_ON
			#pragma shader_feature GREYSCALEOUTLINE_ON
			#pragma shader_feature POSTERIZEOUTLINE_ON
			#pragma shader_feature BLURISHD_ON
			#pragma shader_feature MANUALWIND_ON
			#pragma shader_feature ATLAS_ON
			#pragma shader_feature CUSTOMBLENDING_ON
			#pragma shader_feature PREMULTIPLYALPHA_ON

			#pragma shader_feature BILBOARD_ON
			#pragma shader_feature BILBOARDY_ON
			#pragma shader_feature CUSTOMZTEST_ON
			#pragma shader_feature FOG_ON

            #include "UnityCG.cginc"
			#include "AllIn1OneShaderFunctions.cginc"

			#if FOG_ON
			#pragma multi_compile_fog
			#endif

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				half4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				half4 color : COLOR;
				#if OUTTEX_ON
				half2 uvOutTex : TEXCOORD1;
				#endif
				#if OUTDIST_ON
				half2 uvOutDistTex : TEXCOORD2;
				#endif
				#if DISTORT_ON
				half2 uvDistTex : TEXCOORD3;
				#endif
				#if FOG_ON
				UNITY_FOG_COORDS(4)
				#endif
            };

			float globalUnscaledTime;

            sampler2D _MainTex;
            half4 _MainTex_ST, _MainTex_TexelSize, _Color;
			half _Alpha, _RandomSeed;

			#if ATLAS_ON
			half _MinXUV, _MaxXUV, _MinYUV, _MaxYUV;
			#endif

			#if RECTSIZE_ON
			half _RectSize;
			#endif

			#if OFFSETUV_ON
			half _OffsetUvX, _OffsetUvY;
			#endif

			#if CLIPPING_ON
			half _ClipUvLeft, _ClipUvRight, _ClipUvUp, _ClipUvDown;
			#endif

			#if TWISTUV_ON
			half _TwistUvAmount, _TwistUvPosX, _TwistUvPosY, _TwistUvRadius;
			#endif

			#if ROTATEUV_ON
			half _RotateUvAmount;
			#endif

			#if FISHEYE_ON
			half _FishEyeUvAmount;
			#endif

			#if PINCH_ON
			half _PinchUvAmount;
			#endif

			#if DOODLE_ON
			half _HandDrawnAmount, _HandDrawnSpeed;
			#endif

			#if SHAKEUV_ON
			half _ShakeUvSpeed, _ShakeUvX, _ShakeUvY;
			#endif

			#if WAVEUV_ON
			half _WaveAmount, _WaveSpeed, _WaveStrength, _WaveX, _WaveY;
			#endif

			#if ROUNDWAVEUV_ON
			half _RoundWaveStrength, _RoundWaveSpeed;
			#endif

			#if ZOOMUV_ON
			half _ZoomUvAmount;
			#endif

			#if FADE_ON
			sampler2D _FadeTex, _FadeBurnTex;
			half4 _FadeBurnColor, _FadeTex_ST, _FadeBurnTex_ST;
			half _FadeAmount, _FadeBurnWidth, _FadeBurnTransition,_FadeBurnGlow;
			#endif

			#if TEXTURESCROLL_ON
			half _TextureScrollXSpeed, _TextureScrollYSpeed;
			#endif

			#if GLOW_ON
			sampler2D _GlowTex;
			half4 _GlowColor;
			half _Glow;
			#endif

			#if OUTBASE_ON
			half4 _OutlineColor;
			half _OutlineAlpha, _OutlineGlow, _OutlineWidth;
			int _OutlinePixelWidth;
			#endif

			#if OUTTEX_ON
			sampler2D _OutlineTex;
			half4 _OutlineTex_ST;
			half _OutlineTexXSpeed, _OutlineTexYSpeed;
			#endif

			#if OUTDIST_ON
			sampler2D _OutlineDistortTex;
			half4 _OutlineDistortTex_ST;
			half _OutlineDistortTexXSpeed, _OutlineDistortTexYSpeed, _OutlineDistortAmount;
			#endif

			#if DISTORT_ON
			sampler2D _DistortTex;
			half4 _DistortTex_ST;
			half _DistortTexXSpeed, _DistortTexYSpeed, _DistortAmount;
			#endif

			#if WIND_ON
			half _GrassSpeed, _GrassWind, _GrassManualAnim, _GrassRadialBend;
			#endif

			#if GRADIENT_ON
			half _GradBlend, _GradBoostX, _GradBoostY;
			half4 _GradTopRightCol, _GradTopLeftCol, _GradBotRightCol, _GradBotLeftCol;
			#endif

			#if COLORSWAP_ON
			sampler2D _ColorSwapTex;
			half4 _ColorSwapRed, _ColorSwapGreen, _ColorSwapBlue;
			half _ColorSwapRedLuminosity, _ColorSwapGreenLuminosity, _ColorSwapBlueLuminosity, _ColorSwapBlend;
			#endif

			#if HSV_ON
			half _HsvShift, _HsvSaturation, _HsvBright;
			#endif

			#if HITEFFECT_ON
			half4 _HitEffectColor;
			half _HitEffectGlow, _HitEffectBlend;
			#endif

			#if PIXELATE_ON
			half _PixelateSize;
			#endif

			#if NEGATIVE_ON
			half _NegativeAmount;
			#endif

			#if COLORRAMP_ON
			sampler2D _ColorRampTex, _ColorRampTexGradient;
			half _ColorRampLuminosity, _ColorRampBlend;
			#endif

			#if GREYSCALE_ON
			half _GreyscaleLuminosity, _GreyscaleBlend;
			half4 _GreyscaleTintColor;
			#endif

			#if POSTERIZE_ON
			half _PosterizeNumColors, _PosterizeGamma;
			#endif

			#if BLUR_ON
			half _BlurIntensity;
			#endif

			#if MOTIONBLUR_ON
			half _MotionBlurAngle, _MotionBlurDist;
			#endif

			#if GHOST_ON
			half _GhostColorBoost, _GhostTransparency, _GhostBlend;
			#endif

			#if ALPHAOUTLINE_ON
			half _AlphaOutlineGlow, _AlphaOutlinePower, _AlphaOutlineMinAlpha, _AlphaOutlineBlend;
			half4 _AlphaOutlineColor;
			#endif

			#if INNEROUTLINE_ON
			half _InnerOutlineThickness, _InnerOutlineAlpha, _InnerOutlineGlow;
			half4 _InnerOutlineColor;
			#endif

			#if HOLOGRAM_ON
			half _HologramStripesAmount, _HologramMinAlpha, _HologramUnmodAmount, _HologramStripesSpeed, _HologramMaxAlpha, _HologramBlend;
			half4 _HologramStripeColor;
			#endif

			#if CHROMABERR_ON
			half _ChromAberrAmount, _ChromAberrAlpha;
			#endif

			#if GLITCH_ON
			half _GlitchAmount, _GlitchSize;
			#endif

			#if FLICKER_ON
			half _FlickerFreq, _FlickerPercent, _FlickerAlpha;
			#endif

			#if SHADOW_ON
			half _ShadowX, _ShadowY, _ShadowAlpha;
			half4 _ShadowColor;
			#endif

			#if SHINE_ON
			sampler2D _ShineMask;
			half4 _ShineColor;
			half _ShineLocation, _ShineRotate, _ShineWidth, _ShineGlow;
			#endif
			
			#if ALPHACUTOFF_ON
			half _AlphaCutoffValue;
			#endif

			#if ALPHAROUND_ON
			half _AlphaRoundThreshold;
			#endif

			#if CHANGECOLOR_ON
			half4 _ColorChangeNewCol, _ColorChangeTarget;
			half _ColorChangeTolerance, _ColorChangeLuminosity;
			#endif
			#if CHANGECOLOR2_ON
			half4 _ColorChangeNewCol2, _ColorChangeTarget2;
			half _ColorChangeTolerance2;
			#endif
			#if CHANGECOLOR3_ON
			half4 _ColorChangeNewCol3, _ColorChangeTarget3;
			half _ColorChangeTolerance3;
			#endif

			#if CONTRAST_ON
			half _Contrast, _Brightness;
			#endif

			#if OVERLAY_ON
			sampler2D _OverlayTex;
			half4 _OverlayTex_ST, _OverlayColor;
			half _OverlayGlow, _OverlayBlend;
			#endif

            v2f vert (appdata v)
            {
				#if RECTSIZE_ON
				v.vertex.xyz += (v.vertex.xyz * (_RectSize - 1.0));
				#endif

                v2f o;
				#if BILBOARD_ON
				half3 camRight = mul((half3x3)unity_CameraToWorld, half3(1,0,0));
				half3 camUp = half3(0,1,0);
				#if BILBOARDY_ON
				camUp = mul((half3x3)unity_CameraToWorld, half3(0,1,0));
				#endif
				half3 localPos = v.vertex.x * camRight + v.vertex.y * camUp;
				o.vertex = UnityObjectToClipPos(half4(localPos, 1));
				#else
				o.vertex = UnityObjectToClipPos(v.vertex);
				#endif
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;

				half2 center = half2(0.5, 0.5);
				#if ATLAS_ON
				center = half2((_MaxXUV + _MinXUV) / 2.0, (_MaxYUV + _MinYUV) / 2.0);
				#endif

				#if POLARUV_ON
				o.uv = v.uv - center;
				#endif

				#if ROTATEUV_ON
				half2 uvC = v.uv;
				half cosAngle = cos(_RotateUvAmount);
				half sinAngle = sin(_RotateUvAmount);
				half2x2 rot = half2x2(cosAngle, -sinAngle, sinAngle, cosAngle);
				uvC -= center;
				o.uv = mul(rot, uvC);
				o.uv += center;
				#endif

				#if OUTTEX_ON
				o.uvOutTex = TRANSFORM_TEX(v.uv, _OutlineTex);
				#endif

				#if OUTDIST_ON
				o.uvOutDistTex = TRANSFORM_TEX(v.uv, _OutlineDistortTex);
				#endif

				#if DISTORT_ON
				o.uvDistTex = TRANSFORM_TEX(v.uv, _DistortTex);
				#endif

				#if FOG_ON
				UNITY_TRANSFER_FOG(o,o.vertex);
				#endif

                return o;
            }

			half3 GetPixel(in int offsetX, in int offsetY, half2 uv, sampler2D tex)
			{
				return tex2D(tex, (uv + half2(offsetX * _MainTex_TexelSize.x, offsetY * _MainTex_TexelSize.y))).rgb;
			}

            half4 frag (v2f i) : SV_Target
            {
				float2 uvRect = i.uv;
				half2 center = half2(0.5, 0.5);
				#if ATLAS_ON
				center = half2((_MaxXUV + _MinXUV) / 2.0, (_MaxYUV + _MinYUV) / 2.0);
				uvRect = half2((i.uv.x - _MinXUV) / (_MaxXUV - _MinXUV), (i.uv.y - _MinYUV) / (_MaxYUV - _MinYUV));
				#endif
				half2 centerTiled = half2(center.x *  _MainTex_ST.x, center.y *  _MainTex_ST.y);

				#if CLIPPING_ON
				half2 tiledUv = half2(i.uv.x / _MainTex_ST.x, i.uv.y / _MainTex_ST.y);
				#if ATLAS_ON
				tiledUv = half2((tiledUv.x - _MinXUV) / (_MaxXUV - _MinXUV), (tiledUv.y - _MinYUV) / (_MaxYUV - _MinYUV));
				#endif
				clip((1 - _ClipUvUp) - tiledUv.y);
				clip(tiledUv.y - _ClipUvDown);
				clip((1 - _ClipUvRight) - tiledUv.x);
				clip(tiledUv.x - _ClipUvLeft);
				#endif

				#if TEXTURESCROLL_ON && ATLAS_ON
				i.uv = half2(_MinXUV + ((_MaxXUV - _MinXUV) * (abs((((globalUnscaledTime * 20) + _RandomSeed) * _TextureScrollXSpeed) + uvRect.x) % 1)),
				_MinYUV + ((_MaxYUV - _MinYUV) * (abs((((globalUnscaledTime * 20) + _RandomSeed) * _TextureScrollYSpeed) + uvRect.y) % 1)));
				#endif

				#if OFFSETUV_ON
				#if ATLAS_ON
				i.uv = half2(_MinXUV + ((_MaxXUV - _MinXUV) * (abs((_OffsetUvX + uvRect.x) % 1))),
				_MinYUV + ((_MaxYUV - _MinYUV) * (abs(_OffsetUvY + uvRect.y) % 1)));
				#else
				i.uv += half2(_OffsetUvX, _OffsetUvY);
				#endif
				#endif

				#if POLARUV_ON
				i.uv = half2(atan2(i.uv.y, i.uv.x) / (2.0f * 3.141592653589f), length(i.uv));
			    i.uv *= _MainTex_ST.xy;
				#endif

				#if TWISTUV_ON
				#if ATLAS_ON
				_TwistUvPosX = ((_MaxXUV - _MinXUV) * _TwistUvPosX) + _MinXUV;
				_TwistUvPosY = ((_MaxYUV - _MinYUV) * _TwistUvPosY) + _MinYUV;
				#endif
				half2 tempUv = i.uv - half2(_TwistUvPosX *  _MainTex_ST.x, _TwistUvPosY *  _MainTex_ST.y);
				_TwistUvRadius *= (_MainTex_ST.x + _MainTex_ST.y) / 2;
				half percent = (_TwistUvRadius - length(tempUv)) / _TwistUvRadius;
				half theta = percent * percent * (2.0 * sin(_TwistUvAmount)) * 8.0;
				half s = sin(theta);
				half c = cos(theta);
				half beta = max(sign(_TwistUvRadius - length(tempUv)), 0.0);
				tempUv = half2(dot(tempUv, half2(c, -s)), dot(tempUv, half2(s, c))) * beta +	tempUv * (1 - beta);
				tempUv += half2(_TwistUvPosX *  _MainTex_ST.x, _TwistUvPosY *  _MainTex_ST.y);
				i.uv = tempUv;
				#endif

				#if FISHEYE_ON
				half bind = length(centerTiled);
				half2 dF = i.uv - centerTiled;
				half dFlen = length(dF);
				half fishInt = (3.14159265359 / bind) * (_FishEyeUvAmount + 0.001);
				i.uv = centerTiled + (dF / (max(0.0001, dFlen))) * tan(dFlen * fishInt) * bind / tan(bind * fishInt);
				#endif

				#if PINCH_ON
				half2 dP = i.uv - centerTiled;
				half pinchInt = (3.141592 / length(centerTiled)) * (-_PinchUvAmount + 0.001);
				i.uv = centerTiled + normalize(dP) * atan(length(dP) * -pinchInt * 10.0) * 0.5 / atan(-pinchInt * 5);
				#endif

				#if ZOOMUV_ON
				i.uv -= centerTiled;
				i.uv = i.uv * _ZoomUvAmount;
				i.uv += centerTiled;
				#endif

				#if DOODLE_ON
				half2 uvCopy = uvRect;
				_HandDrawnSpeed = (floor(((globalUnscaledTime * 20) + _RandomSeed) * _HandDrawnSpeed) / _HandDrawnSpeed) * _HandDrawnSpeed;
				uvCopy.x = sin((uvCopy.x * _HandDrawnAmount + _HandDrawnSpeed) * 4);
				uvCopy.y = cos((uvCopy.y * _HandDrawnAmount + _HandDrawnSpeed) * 4);
				i.uv = lerp(i.uv, i.uv + uvCopy, 0.0005 * _HandDrawnAmount);
				#endif

				#if SHAKEUV_ON
				half xShake = sin((globalUnscaledTime + _RandomSeed) * _ShakeUvSpeed * 500 * _ShakeUvX);
				half yShake = sin((globalUnscaledTime + _RandomSeed) * _ShakeUvSpeed * 500 * _ShakeUvY);
				i.uv += half2(xShake * 0.01, yShake * 0.01);
				#endif

				#if RECTSIZE_ON
				i.uv = i.uv.xy * (_RectSize).xx + (((-_RectSize * 0.5) + 0.5)).xx;
				#endif

				#if DISTORT_ON
				i.uvDistTex.x += ((globalUnscaledTime + _RandomSeed) * _DistortTexXSpeed) % 1;
				i.uvDistTex.y += ((globalUnscaledTime + _RandomSeed) * _DistortTexYSpeed) % 1;
				#if ATLAS_ON
				i.uvDistTex = half2((i.uvDistTex.x - _MinXUV) / (_MaxXUV - _MinXUV), (i.uvDistTex.y - _MinYUV) / (_MaxYUV - _MinYUV));
				#endif
				half distortAmnt = (tex2D(_DistortTex, i.uvDistTex).r - 0.5) * 0.2 * _DistortAmount;
				i.uv.x += distortAmnt;
				i.uv.y += distortAmnt;
				#endif

				#if WAVEUV_ON
				half2 uvWave = half2(_WaveX *  _MainTex_ST.x, _WaveY *  _MainTex_ST.y) - i.uv;
				#if ATLAS_ON
				uvWave = half2(_WaveX, _WaveY) - uvRect;
				#endif
				uvWave.x *= _ScreenParams.x / _ScreenParams.y;
				half angWave = (sqrt(dot(uvWave, uvWave)) * _WaveAmount) - ((((globalUnscaledTime * 20) + _RandomSeed) *  _WaveSpeed) % 360.0);
				i.uv = i.uv + normalize(uvWave) * sin(angWave) * (_WaveStrength / 1000.0);
				#endif

				#if ROUNDWAVEUV_ON
				half xWave = ((0.5 * _MainTex_ST.x) - uvRect.x);
				half yWave = ((0.5 * _MainTex_ST.y) - uvRect.y) * (_MainTex_TexelSize.w / _MainTex_TexelSize.z);
				half ripple = -sqrt(xWave*xWave + yWave* yWave);
				i.uv += sin((ripple + ((globalUnscaledTime * 20) + _RandomSeed) * (_RoundWaveSpeed/10.0)) / 0.015) * (_RoundWaveStrength/10.0);
				#endif

				#if WIND_ON
				half windOffset = sin((globalUnscaledTime + _RandomSeed) * _GrassSpeed * 10);
				half2 windCenter = half2(0.5, 0.1);
				#if ATLAS_ON
				windCenter.x = ((_MaxXUV - _MinXUV) * windCenter.x) + _MinXUV;
				windCenter.y = ((_MaxYUV - _MinYUV) * windCenter.y) + _MinYUV;
				#endif
				#if !MANUALWIND_ON
				i.uv.x = fmod(abs(lerp(i.uv.x, i.uv.x + (_GrassWind * 0.01 * windOffset), uvRect.y)), 1);
				#else
				i.uv.x = fmod(abs(lerp(i.uv.x, i.uv.x + (_GrassWind * 0.01 * _GrassManualAnim), uvRect.y)), 1);
				windOffset = _GrassManualAnim;
				#endif
				half2 delta = i.uv - windCenter;
				half delta2 = dot(delta.xy, delta.xy);
				half2 delta_offset = delta2 * windOffset;
				i.uv = i.uv + half2(delta.y, -delta.x) * delta_offset * _GrassRadialBend;
				#endif

				#if TEXTURESCROLL_ON && !ATLAS_ON
				i.uv.x += (((globalUnscaledTime * 20) + _RandomSeed) * _TextureScrollXSpeed) % 1;
				i.uv.y += (((globalUnscaledTime * 20) + _RandomSeed) * _TextureScrollYSpeed) % 1;
				#endif

				#if PIXELATE_ON
				i.uv = floor(i.uv * _PixelateSize) / _PixelateSize;
				#endif

				half4 col = tex2D(_MainTex, i.uv) * i.color;
				half originalAlpha = col.a;
				#if PREMULTIPLYALPHA_ON
				col.rgb *= col.a;
				#endif

				#if GLITCH_ON
				half2 uvGlitch = uvRect;
				uvGlitch.y -= 0.5;
				half lineNoise = pow(rand2(floor(uvGlitch * half2(24., 19.) * _GlitchSize) * 4.0, _RandomSeed), 3.0) * _GlitchAmount
					* pow(rand2(floor(uvGlitch * half2(38., 14.) * _GlitchSize) * 4.0, _RandomSeed), 3.0);
				col = tex2D(_MainTex, i.uv + half2(lineNoise * 0.02 * rand2(half2(2.0, 1), _RandomSeed), 0)) * i.color;
				#endif

				#if CHROMABERR_ON
				half4 r = tex2D(_MainTex, i.uv + half2(_ChromAberrAmount/10, 0)) * i.color;
				half4 b = tex2D(_MainTex, i.uv + half2(-_ChromAberrAmount/10, 0)) * i.color;
				col = half4(r.r * r.a, col.g, b.b * b.a, max(max(r.a, b.a) * _ChromAberrAlpha, col.a));
				#endif

				#if BLUR_ON
				#if !BLURISHD_ON
				col = BlurHD(i.uv, _MainTex, _BlurIntensity) * i.color;
				#else
				col = Blur(i.uv, _MainTex, _BlurIntensity) * i.color;
				#endif
				#endif

				#if MOTIONBLUR_ON
				_MotionBlurAngle = _MotionBlurAngle * 3.1415926;
				#define rot(n) mul(n, half2x2(cos(_MotionBlurAngle), -sin(_MotionBlurAngle), sin(_MotionBlurAngle), cos(_MotionBlurAngle)))
				_MotionBlurDist = _MotionBlurDist * 0.005;
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(-_MotionBlurDist, -_MotionBlurDist)));
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(-_MotionBlurDist * 2, -_MotionBlurDist * 2)));
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(-_MotionBlurDist * 3, -_MotionBlurDist * 3)));
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(-_MotionBlurDist * 4, -_MotionBlurDist * 4)));
				col.rgb += tex2D(_MainTex, i.uv);
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(_MotionBlurDist, _MotionBlurDist)));
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(_MotionBlurDist * 2, _MotionBlurDist * 2)));
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(_MotionBlurDist * 3, _MotionBlurDist * 3)));
				col.rgb += tex2D(_MainTex, i.uv + rot(half2(_MotionBlurDist * 4, _MotionBlurDist * 4)));
				col.rgb = col.rgb / 9;
				#endif

				#if NEGATIVE_ON
				col.rgb = lerp(col.rgb, 1 - col.rgb, _NegativeAmount);
				#endif

				half luminance = 0;
				#if GREYSCALE_ON && !GREYSCALEOUTLINE_ON
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				luminance = saturate(luminance + _GreyscaleLuminosity);
				col.rgb = lerp(col.rgb, half3(luminance, luminance, luminance) * _GreyscaleTintColor, _GreyscaleBlend);
				#endif

				#if GHOST_ON
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				half4 ghostResult;
				ghostResult.a = saturate(luminance - _GhostTransparency) * col.a;
				ghostResult.rgb = col.rgb * (luminance + _GhostColorBoost);
				col = lerp(col, ghostResult, _GhostBlend);
				#endif

				#if INNEROUTLINE_ON
				half3 innerT = abs(GetPixel(0, _InnerOutlineThickness, i.uv, _MainTex) - GetPixel(0, -_InnerOutlineThickness, i.uv, _MainTex));
				innerT += abs(GetPixel(_InnerOutlineThickness, 0, i.uv, _MainTex) - GetPixel(-_InnerOutlineThickness, 0, i.uv, _MainTex));
				#if !ONLYINNEROUTLINE_ON
				innerT = (innerT / 2.0) * col.a * _InnerOutlineAlpha;
				col.rgb += length(innerT) * _InnerOutlineColor.rgb * _InnerOutlineGlow;
				#else
				innerT *= col.a * _InnerOutlineAlpha;
				col.rgb = length(innerT) * _InnerOutlineColor.rgb * _InnerOutlineGlow;
				col.a = step(0.3, col.r+col.g+col.b);
				#endif
				#endif

				#if HITEFFECT_ON
				col.rgb = lerp(col.rgb, _HitEffectColor.rgb * _HitEffectGlow, _HitEffectBlend);
				#endif

				#if GRADIENT_ON
				half2 tiledUvGrad = half2(uvRect.x / _MainTex_ST.x, uvRect.y / _MainTex_ST.y);
				#if GRADIENT2COL_ON
				_GradTopRightCol = _GradTopLeftCol;
				_GradBotRightCol = _GradBotLeftCol;
				#endif
				#if RADIALGRADIENT_ON
				half radialDist = 1 - length(tiledUvGrad - half2(0.5, 0.5));
				radialDist *= (_MainTex_TexelSize.w / _MainTex_TexelSize.z);
				radialDist = saturate(_GradBoostX * radialDist);
				half4 gradientResult = lerp(_GradTopLeftCol, _GradBotLeftCol, radialDist);
				#else
				half gradXLerpFactor = saturate(pow(tiledUvGrad.x, _GradBoostX));
				half4 gradientResult = lerp(lerp(_GradBotLeftCol, _GradBotRightCol, gradXLerpFactor),
				lerp(_GradTopLeftCol, _GradTopRightCol, gradXLerpFactor), saturate(pow(tiledUvGrad.y, _GradBoostY)));
				#endif
				gradientResult = lerp(col, gradientResult, _GradBlend);
				col.rgb = gradientResult.rgb * col.a;
				col.a *= gradientResult.a;
				#endif

				#if CONTRAST_ON
				col.rgb = (col.rgb - float3(0.5, 0.5, 0.5)) * _Contrast + float3(0.5, 0.5, 0.5);
				col.rgb += _Brightness;
				#endif

				#if COLORSWAP_ON
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				half4 swapMask = tex2D(_ColorSwapTex, i.uv);
				swapMask.rgb *= swapMask.a;
				half3 redSwap = _ColorSwapRed * swapMask.r * col.a * saturate(luminance + _ColorSwapRedLuminosity);
				half3 greenSwap = _ColorSwapGreen * swapMask.g * col.a * saturate(luminance + _ColorSwapGreenLuminosity);
				half3 blueSwap = _ColorSwapBlue * swapMask.b * col.a * saturate(luminance + _ColorSwapBlueLuminosity);
				swapMask.rgb = col.rgb * saturate(1 - swapMask.r - swapMask.g - swapMask.b);
				col.rgb = lerp(col.rgb, swapMask.rgb + redSwap + greenSwap + blueSwap, _ColorSwapBlend);
				#endif

				#if COLORRAMP_ON && !COLORRAMPOUTLINE_ON 
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				luminance = saturate(luminance + _ColorRampLuminosity);
				#if GRADIENTCOLORRAMP_ON
				col.rgb = lerp(col.rgb, tex2D(_ColorRampTexGradient, half2(luminance, 0)).rgb, _ColorRampBlend);
				#else
				col.rgb = lerp(col.rgb, tex2D(_ColorRampTex, half2(luminance, 0)).rgb, _ColorRampBlend);
				#endif
				#endif

				#if CHANGECOLOR_ON
				float3 currChangeColor = col.rgb;
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				luminance = saturate(luminance + _ColorChangeLuminosity);
				half3 dif = abs(currChangeColor - _ColorChangeTarget.rgb);
				col.rgb = lerp(col.rgb, half3(luminance, luminance, luminance) * _ColorChangeNewCol.rgb,
					max(sign(1 - saturate(dif.x + dif.y + dif.z) - _ColorChangeTolerance), 0.0));
				#if CHANGECOLOR2_ON
				dif = abs(currChangeColor - _ColorChangeTarget2.rgb);
				col.rgb = lerp(col.rgb, half3(luminance, luminance, luminance) * _ColorChangeNewCol2.rgb,
					max(sign(1 - saturate(dif.x + dif.y + dif.z) - _ColorChangeTolerance2), 0.0));
				#endif
				#if CHANGECOLOR3_ON
				dif = abs(currChangeColor - _ColorChangeTarget3.rgb);
				col.rgb = lerp(col.rgb, half3(luminance, luminance, luminance) * _ColorChangeNewCol3.rgb,
					max(sign(1 - saturate(dif.x + dif.y + dif.z) - _ColorChangeTolerance3), 0.0));
				#endif
				#endif

				#if POSTERIZE_ON && !POSTERIZEOUTLINE_ON
				col.rgb = pow(col.rgb, _PosterizeGamma) * _PosterizeNumColors;
				col.rgb = floor(col.rgb) / _PosterizeNumColors;
				col.rgb = pow(col.rgb, 1.0 / _PosterizeGamma);
				#endif

				#if HSV_ON
				half3 resultHsv = half3(col.rgb);
				half cosHsv = _HsvBright * _HsvSaturation * cos(_HsvShift * 3.14159265 / 180);
				half sinHsv = _HsvBright * _HsvSaturation * sin(_HsvShift * 3.14159265 / 180);
				resultHsv.x = (.299 * _HsvBright + .701 * cosHsv + .168 * sinHsv) * col.x
					+ (.587 * _HsvBright - .587 * cosHsv + .330 * sinHsv) * col.y
					+ (.114 * _HsvBright - .114 * cosHsv - .497 * sinHsv) * col.z;
				resultHsv.y = (.299 * _HsvBright - .299 * cosHsv - .328 * sinHsv) *col.x
					+ (.587 * _HsvBright + .413 * cosHsv + .035 * sinHsv) * col.y
					+ (.114 * _HsvBright - .114 * cosHsv + .292 * sinHsv) * col.z;
				resultHsv.z = (.299 * _HsvBright - .3 * cosHsv + 1.25 * sinHsv) * col.x
					+ (.587 * _HsvBright - .588 * cosHsv - 1.05 * sinHsv) * col.y
					+ (.114 * _HsvBright + .886 * cosHsv - .203 * sinHsv) * col.z;
				col.rgb = resultHsv;
				#endif

				#if OVERLAY_ON
				half4 overlayCol = tex2D(_OverlayTex, TRANSFORM_TEX(i.uv, _OverlayTex));
				overlayCol.rgb *= _OverlayColor.rgb * _OverlayGlow;
				#if !OVERLAYMULT_ON
				overlayCol.rgb *= overlayCol.a * _OverlayColor.rgb * _OverlayColor.a * _OverlayBlend;
				col.rgb += overlayCol;
				#else
				overlayCol.a *= _OverlayColor.a;
				col = lerp(col, col * overlayCol, _OverlayBlend);
				#endif
				#endif

				//OUTLINE-------------------------------------------------------------
				#if OUTBASE_ON
					#ifdef OUTBASEPIXELPERF_ON
					half2 destUv = half2(_OutlinePixelWidth * _MainTex_TexelSize.x, _OutlinePixelWidth * _MainTex_TexelSize.y);
					#else
					half2 destUv = half2(_OutlineWidth * _MainTex_TexelSize.x * 200, _OutlineWidth * _MainTex_TexelSize.y * 200);
					#endif

					#if OUTDIST_ON
					i.uvOutDistTex.x += ((globalUnscaledTime + _RandomSeed) * _OutlineDistortTexXSpeed) % 1;
					i.uvOutDistTex.y += ((globalUnscaledTime + _RandomSeed) * _OutlineDistortTexYSpeed) % 1;
					#if ATLAS_ON
					i.uvOutDistTex = half2((i.uvOutDistTex.x - _MinXUV) / (_MaxXUV - _MinXUV), (i.uvOutDistTex.y - _MinYUV) / (_MaxYUV - _MinYUV));
					#endif
					half outDistortAmnt = (tex2D(_OutlineDistortTex, i.uvOutDistTex).r - 0.5) * 0.2 * _OutlineDistortAmount;
					destUv.x += outDistortAmnt;
					destUv.y += outDistortAmnt;
					#endif

					half spriteLeft = tex2D(_MainTex, i.uv + half2(destUv.x, 0)).a;
					half spriteRight = tex2D(_MainTex, i.uv - half2(destUv.x, 0)).a;
					half spriteBottom = tex2D(_MainTex, i.uv + half2(0, destUv.y)).a;
					half spriteTop = tex2D(_MainTex, i.uv - half2(0, destUv.y)).a;
					half result = spriteLeft + spriteRight + spriteBottom + spriteTop;

					#if OUTBASE8DIR_ON
					half spriteTopLeft = tex2D(_MainTex, i.uv + half2(destUv.x, destUv.y)).a;
					half spriteTopRight = tex2D(_MainTex, i.uv + half2(-destUv.x, destUv.y)).a;
					half spriteBotLeft = tex2D(_MainTex, i.uv + half2(destUv.x, -destUv.y)).a;
					half spriteBotRight = tex2D(_MainTex, i.uv + half2(-destUv.x, -destUv.y)).a;
					result = result + spriteTopLeft + spriteTopRight + spriteBotLeft + spriteBotRight;
					#endif
					
					result = step(0.05, saturate(result));

					#if OUTTEX_ON
					i.uvOutTex.x += ((globalUnscaledTime + _RandomSeed) * _OutlineTexXSpeed) % 1;
					i.uvOutTex.y += ((globalUnscaledTime + _RandomSeed) * _OutlineTexYSpeed) % 1;
					#if ATLAS_ON
					i.uvOutTex = half2((i.uvOutTex.x - _MinXUV) / (_MaxXUV - _MinXUV), (i.uvOutTex.y - _MinYUV) / (_MaxYUV - _MinYUV));
					#endif
					half4 tempOutColor = tex2D(_OutlineTex, i.uvOutTex);
					#if OUTGREYTEXTURE_ON
					luminance = 0.3 * tempOutColor.r + 0.59 * tempOutColor.g + 0.11 * tempOutColor.b;
					tempOutColor = half4(luminance, luminance, luminance, 1);
					#endif
					tempOutColor *= _OutlineColor;
					_OutlineColor = tempOutColor;
					#endif

					result *= (1 - originalAlpha) * _OutlineAlpha;

					half4 outline = _OutlineColor;
					outline.rgb *= _OutlineGlow;
					outline.a = result;
					#if ONLYOUTLINE_ON
					col = outline;
					#else
					col = lerp(col, outline, result);
					#endif
				#endif
				//-----------------------------------------------------------------------------

				#if FADE_ON
				half2 tiledUvFade1= TRANSFORM_TEX(i.uv, _FadeTex);
				half2 tiledUvFade2 = TRANSFORM_TEX(i.uv, _FadeBurnTex);
				#if ATLAS_ON
				tiledUvFade1 = half2((tiledUvFade1.x - _MinXUV) / (_MaxXUV - _MinXUV), (tiledUvFade1.y - _MinYUV) / (_MaxYUV - _MinYUV));
				tiledUvFade2 = half2((tiledUvFade2.x - _MinXUV) / (_MaxXUV - _MinXUV), (tiledUvFade2.y - _MinYUV) / (_MaxYUV - _MinYUV));
				#endif
				half fadeTemp = tex2D(_FadeTex, tiledUvFade1).r;
				half fade = smoothstep(_FadeAmount + 0.01, _FadeAmount + _FadeBurnTransition, fadeTemp);
				half fadeBurn = saturate(smoothstep(_FadeAmount - _FadeBurnWidth, _FadeAmount - _FadeBurnWidth + 0.1, fadeTemp) * _FadeAmount);
				col.a *= fade;
				_FadeBurnColor.rgb *= _FadeBurnGlow;
				col += fadeBurn * tex2D(_FadeBurnTex, tiledUvFade2) * _FadeBurnColor * originalAlpha * (1 - col.a);
				#endif
				
				#if SHADOW_ON
				half shadowA = tex2D(_MainTex, i.uv + half2(_ShadowX, _ShadowY)).a;
				half preMultShadowMask = 1 - (saturate(shadowA - col.a) * (1 - col.a));
				col.rgb *= 1 - ((shadowA - col.a) * (1 - col.a));
				col.rgb += (_ShadowColor * shadowA) * (1 - col.a);
				col.a = max(shadowA * _ShadowAlpha * i.color.a, col.a);
				#endif

				#if GLOW_ON
				half4 emission;
				#if GLOWTEX_ON
				emission = tex2D(_GlowTex, i.uv);
				#else
				emission = col;
				#endif
				emission.rgb *= emission.a * col.a * _Glow * _GlowColor;
				col.rgb += emission.rgb;
				#endif

				#if COLORRAMP_ON && COLORRAMPOUTLINE_ON 
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				luminance = saturate(luminance + _ColorRampLuminosity);
				#if GRADIENTCOLORRAMP_ON
				col.rgb = lerp(col.rgb, tex2D(_ColorRampTexGradient, half2(luminance, 0)).rgb, _ColorRampBlend);
				#else
				col.rgb = lerp(col.rgb, tex2D(_ColorRampTex, half2(luminance, 0)).rgb, _ColorRampBlend);
				#endif
				#endif

				#if GREYSCALE_ON && GREYSCALEOUTLINE_ON
				luminance = 0.3 * col.r + 0.59 * col.g + 0.11 * col.b;
				luminance = saturate(luminance + _GreyscaleLuminosity);
				col.rgb = lerp(col.rgb, half3(luminance, luminance, luminance) * _GreyscaleTintColor, _GreyscaleBlend);
				#endif

				#if POSTERIZE_ON && POSTERIZEOUTLINE_ON
				col.rgb = pow(col.rgb, _PosterizeGamma) * _PosterizeNumColors;
				col.rgb = floor(col.rgb) / _PosterizeNumColors;
				col.rgb = pow(col.rgb, 1.0 / _PosterizeGamma);
				#endif

				#if SHINE_ON
				half2 uvShine = uvRect;
				half cosAngle = cos(_ShineRotate);
				half sinAngle = sin(_ShineRotate);
				half2x2 rot = half2x2(cosAngle, -sinAngle, sinAngle, cosAngle);
				uvShine -= half2(0.5, 0.5);
				uvShine = mul(rot, uvShine);
				uvShine += half2(0.5, 0.5);
				half shineMask = tex2D(_ShineMask, i.uv).a;
				half currentDistanceProjection = (uvShine.x + uvShine.y) / 2;
				half whitePower = 1 - (abs(currentDistanceProjection - _ShineLocation) / _ShineWidth);
				col.rgb +=  col.a * whitePower * _ShineGlow * max(sign(currentDistanceProjection - (_ShineLocation - _ShineWidth)), 0.0)
				* max(sign((_ShineLocation + _ShineWidth) - currentDistanceProjection), 0.0) * _ShineColor * shineMask;
				#endif

				#if HOLOGRAM_ON
				half totalHologram = _HologramStripesAmount + _HologramUnmodAmount;
				half hologramYCoord = ((uvRect.y + (((globalUnscaledTime + _RandomSeed) % 1) * _HologramStripesSpeed)) % totalHologram) / totalHologram;
				hologramYCoord = abs(hologramYCoord);
				half alpha = RemapFloat(saturate(hologramYCoord - (_HologramUnmodAmount/totalHologram)), 0.0, 1.0, _HologramMinAlpha, saturate(_HologramMaxAlpha));
				half hologramMask = max(sign((_HologramUnmodAmount/totalHologram) - hologramYCoord), 0.0);
				half4 hologramResult = col;
				hologramResult.a *= lerp(alpha, 1, hologramMask);
				hologramResult.rgb *= max(1, _HologramMaxAlpha * max(sign(hologramYCoord - (_HologramUnmodAmount/totalHologram)), 0.0));
				hologramMask = 1 - step(0.01,hologramMask);
				hologramResult.rgb += hologramMask * _HologramStripeColor * col.a;
				col = lerp(col, hologramResult, _HologramBlend);
				#endif

				#if FLICKER_ON
				col.a *= saturate(col.a * step(frac(0.05 + ((globalUnscaledTime * 60) + _RandomSeed) * _FlickerFreq), 1 - _FlickerPercent) + _FlickerAlpha);
				#endif

				col.a *= _Alpha;

				#if ALPHACUTOFF_ON
				clip((1 - _AlphaCutoffValue) - (1 - col.a) - 0.01);
				#endif

				#if ALPHAROUND_ON
				col.a = step(_AlphaRoundThreshold, col.a);
				#endif

				#if ALPHAOUTLINE_ON
				half alphaOutlineRes = pow(1 - col.a, max(_AlphaOutlinePower, 0.0001)) * step(_AlphaOutlineMinAlpha, col.a) * _AlphaOutlineBlend;
				col.rgb = lerp(col.rgb, _AlphaOutlineColor.rgb * _AlphaOutlineGlow, alphaOutlineRes);
				col.a = lerp(col.a, 1, alphaOutlineRes > 1);
				#endif

				col *= _Color;

				#if FOG_ON
				UNITY_APPLY_FOG(i.fogCoord, col);
				#endif

                return col;
            }
            ENDCG
        }
    }
	CustomEditor "AllIn1SpriteShader"
	Fallback "Sprites/Default"
}