using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Linq;

[CanEditMultipleObjects]
public class AllIn1SpriteShaderStencil : ShaderGUI
{
    private Material targetMat;
    private BlendMode srcMode, dstMode;
    private CompareFunction zTestMode = CompareFunction.LessEqual;

    private GUIStyle style, bigLabelStyle, smallLabelStyle;
    private const int bigFontSize = 16, smallFontSize = 11;
    private string[] oldKeyWords;
    private int effectCount = 1;
    private Material originalMaterialCopy;
    private MaterialEditor matEditor;
    private MaterialProperty[] matProperties;

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        matEditor = materialEditor;
        matProperties = properties;
        targetMat = materialEditor.target as Material;
        effectCount = 1;
        oldKeyWords = targetMat.shaderKeywords;
        style = new GUIStyle(EditorStyles.helpBox);
        style.margin = new RectOffset(0, 0, 0, 0);
        bigLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        bigLabelStyle.fontSize = bigFontSize;
        smallLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        smallLabelStyle.fontSize = smallFontSize;

        GUILayout.Label("General Properties", bigLabelStyle);
        DrawProperty(0);
        DrawProperty(1);
        DrawProperty(2);

        //StencilSettings(materialEditor, properties);

        //Not needed since Unity batches sprites on its own
        //EditorGUILayout.Separator();
        //materialEditor.EnableInstancingField();
        //Debug.Log(materialEditor.IsInstancingEnabled() + "  " + Application.isBatchMode);

        EditorGUILayout.Separator();
        Blending("Custom Blending", "CUSTOMBLENDING_ON");
        SpriteAtlas("Sprite inside an atlas?", "ATLAS_ON"); ;

        DrawLine(Color.grey, 1, 3);
        GUILayout.Label("Color Effects", bigLabelStyle);

        Glow("Glow", "GLOW_ON");
        GenericEffect("Fade", "FADE_ON", 7, 13);
        Outline("Outline", "OUTBASE_ON");
        GenericEffect("Alpha Outline", "ALPHAOUTLINE_ON", 159, 163, true, "A more performant but less flexible outline");
        InnerOutline("Inner Outline", "INNEROUTLINE_ON", 66, 69);
        Gradient("Gradient & Radial Gradient", "GRADIENT_ON");
        GenericEffect("Color Swap", "COLORSWAP_ON", 36, 42, true, "You will need a mask texture (see Documentation)", new int[] { 154 });
        GenericEffect("Hue Shift", "HSV_ON", 43, 45);
        ColorChange("Change 1 Color", "CHANGECOLOR_ON");
        ColorRamp("Color Ramp", "COLORRAMP_ON");
        GenericEffect("Hit Effect", "HITEFFECT_ON", 46, 48);
        GenericEffect("Negative", "NEGATIVE_ON", 49, 49);
        GenericEffect("Pixelate", "PIXELATE_ON", 50, 50, true, "Looks bad with distorition effects");
        GreyScale("GreyScale", "GREYSCALE_ON");
        Posterize("Posterize", "POSTERIZE_ON");
        Blur("Blur", "BLUR_ON");
        GenericEffect("Motion Blur", "MOTIONBLUR_ON", 62, 63);
        GenericEffect("Ghost", "GHOST_ON", 64, 65, true, "This effect will not affect the outline", new int[] { 157 });
        GenericEffect("Hologram", "HOLOGRAM_ON", 73, 77, true, null, new int[] { 140, 158 });
        GenericEffect("Chromatic Aberration", "CHROMABERR_ON", 78, 79);
        GenericEffect("Glitch", "GLITCH_ON", 80, 80, true, null, new int[] { 139 });
        GenericEffect("Flicker", "FLICKER_ON", 81, 83);
        GenericEffect("Shadow", "SHADOW_ON", 84, 87);
        GenericEffect("Shine", "SHINE_ON", 133, 138);
        GenericEffect("Contrast & Brightness", "CONTRAST_ON", 152, 153);
        Overlay("Overlay Texture", "OVERLAY_ON");
        GenericEffect("Alpha Cutoff", "ALPHACUTOFF_ON", 70, 70);
        GenericEffect("Alpha Round", "ALPHAROUND_ON", 144, 144);

        DrawLine(Color.grey, 1, 3);
        GUILayout.Label("UV Effects", bigLabelStyle);

        GenericEffect("Hand Drawn", "DOODLE_ON", 88, 89);
        Grass("Grass Movement / Wind", "WIND_ON");
        GenericEffect("Wave", "WAVEUV_ON", 94, 98);
        GenericEffect("Round Wave", "ROUNDWAVEUV_ON", 127, 128);
        GenericEffect("Rect Size (Enable wireframe to see result)", "RECTSIZE_ON", 99, 99, true, "Only on single sprites spritesheets NOT supported");
        GenericEffect("Offset", "OFFSETUV_ON", 100, 101);
        GenericEffect("Clipping / Fill Amount", "CLIPPING_ON", 102, 105);
        GenericEffect("Texture Scroll", "TEXTURESCROLL_ON", 106, 107, true, "Set Texture Wrap Mode to Repeat");
        GenericEffect("Zoom", "ZOOMUV_ON", 108, 108);
        GenericEffect("Distortion", "DISTORT_ON", 109, 112);
        GenericEffect("Twist", "TWISTUV_ON", 113, 116);
        GenericEffect("Rotate", "ROTATEUV_ON", 117, 117, true, "_Tip_ Use Clipping effect to avoid possible undesired parts");
        GenericEffect("Polar Coordinates (Tile texture for good results)", "POLARUV_ON", -1, -1);
        GenericEffect("Fish Eye", "FISHEYE_ON", 118, 118);
        GenericEffect("Pinch", "PINCH_ON", 119, 119);
        GenericEffect("Shake", "SHAKEUV_ON", 120, 122);

        DrawLine(Color.grey, 1, 3);
        materialEditor.RenderQueueField();
    }

    private void Blending(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        MaterialProperty srcM = ShaderGUI.FindProperty("_MySrcMode", matProperties);
        MaterialProperty dstM = ShaderGUI.FindProperty("_MyDstMode", matProperties);
        if (srcM.floatValue == 0 && dstM.floatValue == 0)
        {
            srcM.floatValue = 5;
            dstM.floatValue = 10;
        }
        bool ini = toggle;
        toggle = EditorGUILayout.BeginToggleGroup(inspector, toggle);
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            EditorGUILayout.BeginVertical(style);
            {
                GUILayout.Label("Look for 'ShaderLab: Blending' if you don't know what this is", smallLabelStyle);
                if (GUILayout.Button("Back To Default Blending"))
                {
                    srcM.floatValue = 5;
                    dstM.floatValue = 10;
                    targetMat.DisableKeyword("PREMULTIPLYALPHA_ON");
                }
                srcMode = (BlendMode)srcM.floatValue;
                dstMode = (BlendMode)dstM.floatValue;
                srcMode = (BlendMode)EditorGUILayout.EnumPopup("SrcMode", srcMode);
                dstMode = (BlendMode)EditorGUILayout.EnumPopup("DstMode", dstMode);
                srcM.floatValue = (float)(srcMode);
                dstM.floatValue = (float)(dstMode);

                ini = oldKeyWords.Contains("PREMULTIPLYALPHA_ON");
                toggle = EditorGUILayout.Toggle("Premultiply Alpha?", ini);
                if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                if (toggle) targetMat.EnableKeyword("PREMULTIPLYALPHA_ON");
                else targetMat.DisableKeyword("PREMULTIPLYALPHA_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else
        {
            srcM.floatValue = 5;
            dstM.floatValue = 10;
            targetMat.DisableKeyword(keyword);
        }
        EditorGUILayout.EndToggleGroup();
    }

    private void Billboard(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            EditorGUILayout.BeginVertical(style);
            {
                GUILayout.Label("Don't use this feature on UI elements!", smallLabelStyle);
                DrawProperty(129, true);
                MaterialProperty billboardY = matProperties[129];
                if (billboardY.floatValue == 1) targetMat.EnableKeyword("BILBOARDY_ON");
                else targetMat.DisableKeyword("BILBOARDY_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void ZWrite(string inspector)
    {
        MaterialProperty zWrite = ShaderGUI.FindProperty("_ZWrite", matProperties);
        bool toggle = zWrite.floatValue > 0.9f ? true : false;
        bool ini = toggle;
        toggle = EditorGUILayout.BeginToggleGroup(inspector, toggle);
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            EditorGUILayout.BeginVertical(style);
            {
                GUILayout.Label("Usually used to sort Billboarded sprites", smallLabelStyle);
                GUILayout.Label("Use with Alpha Cutoff effect for optimum results", smallLabelStyle);
                zWrite.floatValue = 1.0f;
            }
            EditorGUILayout.EndVertical();
        }
        else zWrite.floatValue = 0.0f;
        EditorGUILayout.EndToggleGroup();
    }

    private void ZTest(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        MaterialProperty zTestM = ShaderGUI.FindProperty("_ZTestMode", matProperties);
        if (zTestM.floatValue == 0) zTestM.floatValue = 4;
        bool ini = toggle;
        toggle = EditorGUILayout.BeginToggleGroup(inspector, toggle);
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            EditorGUILayout.BeginVertical(style);
            {
                GUILayout.Label("Look for 'ShaderLab culling and depth testing' \nif you don't know what this is", smallLabelStyle);
                zTestMode = (CompareFunction)zTestM.floatValue;
                zTestMode = (CompareFunction)EditorGUILayout.EnumPopup("zTestMode", zTestMode);
                zTestM.floatValue = (float)(zTestMode);
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void SpriteAtlas(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;
        toggle = EditorGUILayout.BeginToggleGroup(inspector, toggle);
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            EditorGUILayout.BeginVertical(style);
            {
                GUILayout.Label("Make sure SpriteAtlasUV component is added \n " +
                    "*Check documentation if unsure what this does or how it works", smallLabelStyle);
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void Outline(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + ".Outline";
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("OUTBASE_ON");
            EditorGUILayout.BeginVertical(style);
            {
                DrawProperty(14);
                DrawProperty(15);
                DrawProperty(16);
                DrawProperty(17, true);
                MaterialProperty outline8dir = matProperties[17];
                if (outline8dir.floatValue == 1) targetMat.EnableKeyword("OUTBASE8DIR_ON");
                else targetMat.DisableKeyword("OUTBASE8DIR_ON");

                DrawLine(Color.grey, 1, 3);
                DrawProperty(19, true);
                MaterialProperty outlinePixel = matProperties[19];
                if (outlinePixel.floatValue == 1)
                {
                    targetMat.EnableKeyword("OUTBASEPIXELPERF_ON");
                    DrawProperty(20);
                }
                else
                {
                    targetMat.DisableKeyword("OUTBASEPIXELPERF_ON");
                    DrawProperty(18);
                }

                DrawLine(Color.grey, 1, 3);
                DrawProperty(21, true);
                MaterialProperty outlineTex = matProperties[21];
                if (outlineTex.floatValue == 1)
                {
                    targetMat.EnableKeyword("OUTTEX_ON");
                    DrawProperty(22);
                    DrawProperty(23);
                    DrawProperty(24);
                    DrawProperty(25, true);
                    MaterialProperty outlineTexGrey = matProperties[25];
                    if (outlineTexGrey.floatValue == 1) targetMat.EnableKeyword("OUTGREYTEXTURE_ON");
                    else targetMat.DisableKeyword("OUTGREYTEXTURE_ON");
                }
                else targetMat.DisableKeyword("OUTTEX_ON");

                DrawLine(Color.grey, 1, 3);
                DrawProperty(26, true);
                MaterialProperty outlineDistort = matProperties[26];
                if (outlineDistort.floatValue == 1)
                {
                    targetMat.EnableKeyword("OUTDIST_ON");
                    DrawProperty(27);
                    DrawProperty(28);
                    DrawProperty(29);
                    DrawProperty(30);
                }
                else targetMat.DisableKeyword("OUTDIST_ON");

                DrawLine(Color.grey, 1, 3);
                DrawProperty(71, true);
                MaterialProperty onlyOutline = matProperties[71];
                if (onlyOutline.floatValue == 1) targetMat.EnableKeyword("ONLYOUTLINE_ON");
                else targetMat.DisableKeyword("ONLYOUTLINE_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("OUTBASE_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void GenericEffect(string inspector, string keyword, int first, int last, bool effectCounter = true, string preMessage = null, int[] extraProperties = null)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        if (effectCounter)
        {
            effectNameLabel.text = effectCount + "." + inspector;
            toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);
            effectCount++;
        }
        else
        {
            effectNameLabel.text = inspector;
            toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);
        }

        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            if (first > 0)
            {
                EditorGUILayout.BeginVertical(style);
                {
                    if (preMessage != null) GUILayout.Label(preMessage, smallLabelStyle);
                    for (int i = first; i <= last; i++) DrawProperty(i);
                    if (extraProperties != null) foreach (int i in extraProperties) DrawProperty(i);
                }
                EditorGUILayout.EndVertical();
            }
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void Glow(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("GLOW_ON");
            EditorGUILayout.BeginVertical(style);
            {
                DrawProperty(3);
                DrawProperty(4);
                DrawProperty(5, true);
                MaterialProperty useGlowTex = matProperties[5];
                if (useGlowTex.floatValue == 1)
                {
                    targetMat.EnableKeyword("GLOWTEX_ON");
                    GUILayout.Label("Texture does not support Tiling", smallLabelStyle);
                    DrawProperty(6);
                }
                else targetMat.DisableKeyword("GLOWTEX_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("GLOW_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void ColorRamp(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("COLORRAMP_ON");
            EditorGUILayout.BeginVertical(style);
            {
                bool useEditableGradient = false;
                if (AssetDatabase.Contains(targetMat))
                {
                    useEditableGradient = oldKeyWords.Contains("GRADIENTCOLORRAMP_ON");
                    bool gradientTex = useEditableGradient;
                    gradientTex = GUILayout.Toggle(gradientTex, new GUIContent("Use Editable Gradient?"));
                    if (useEditableGradient != gradientTex)
                    {
                        if (!Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                        if (gradientTex)
                        {
                            useEditableGradient = true;
                            targetMat.EnableKeyword("GRADIENTCOLORRAMP_ON");
                        }
                        else targetMat.DisableKeyword("GRADIENTCOLORRAMP_ON");
                    }
                    if (useEditableGradient) matEditor.ShaderProperty(matProperties[164], matProperties[164].displayName);
                }
                else GUILayout.Label("*Save to folder to allow for dynamic Gradient property", smallLabelStyle);
                if (!useEditableGradient)
                {
                    GUILayout.Label("Texture does not support Tiling", smallLabelStyle);
                    DrawProperty(51);
                }

                DrawProperty(52);
                DrawProperty(53, true);
                MaterialProperty colorRampOut = matProperties[53];
                if (colorRampOut.floatValue == 1) targetMat.EnableKeyword("COLORRAMPOUTLINE_ON");
                else targetMat.DisableKeyword("COLORRAMPOUTLINE_ON");
                DrawProperty(155);
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("COLORRAMP_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void ColorChange(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("CHANGECOLOR_ON");
            EditorGUILayout.BeginVertical(style);
            {
                for (int i = 123; i < 127; i++) DrawProperty(i);

                EditorGUILayout.Separator();
                ini = oldKeyWords.Contains("CHANGECOLOR2_ON");
                bool toggle2 = ini;
                toggle2 = EditorGUILayout.Toggle("Use Color 2", ini);
                if (ini != toggle2 && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                if (toggle2)
                {
                    targetMat.EnableKeyword("CHANGECOLOR2_ON");
                    for (int i = 146; i < 149; i++) DrawProperty(i);
                }
                else targetMat.DisableKeyword("CHANGECOLOR2_ON");

                EditorGUILayout.Separator();
                ini = oldKeyWords.Contains("CHANGECOLOR3_ON");
                toggle2 = ini;
                toggle2 = EditorGUILayout.Toggle("Use Color 3", toggle2);
                if (ini != toggle2 && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                if (toggle2)
                {
                    targetMat.EnableKeyword("CHANGECOLOR3_ON");
                    for (int i = 149; i < 152; i++) DrawProperty(i);
                }
                else targetMat.DisableKeyword("CHANGECOLOR3_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("CHANGECOLOR_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void GreyScale(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("GREYSCALE_ON");
            EditorGUILayout.BeginVertical(style);
            {
                DrawProperty(54);
                DrawProperty(56);
                DrawProperty(55, true);
                MaterialProperty greyScaleOut = matProperties[55];
                if (greyScaleOut.floatValue == 1) targetMat.EnableKeyword("GREYSCALEOUTLINE_ON");
                else targetMat.DisableKeyword("GREYSCALEOUTLINE_ON");
                DrawProperty(156);
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("GREYSCALE_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void Posterize(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("POSTERIZE_ON");
            EditorGUILayout.BeginVertical(style);
            {
                DrawProperty(57);
                DrawProperty(58);
                DrawProperty(59, true);
                MaterialProperty posterizeOut = matProperties[59];
                if (posterizeOut.floatValue == 1) targetMat.EnableKeyword("POSTERIZEOUTLINE_ON");
                else targetMat.DisableKeyword("POSTERIZEOUTLINE_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("POSTERIZE_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void Blur(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("BLUR_ON");
            EditorGUILayout.BeginVertical(style);
            {
                GUILayout.Label("This effect will not affect the outline", smallLabelStyle);
                DrawProperty(60);
                DrawProperty(61, true);
                MaterialProperty blurIsHd = matProperties[61];
                if (blurIsHd.floatValue == 1) targetMat.EnableKeyword("BLURISHD_ON");
                else targetMat.DisableKeyword("BLURISHD_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("BLUR_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void Grass(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword("WIND_ON");
            EditorGUILayout.BeginVertical(style);
            {
                DrawProperty(90);
                DrawProperty(91);
                DrawProperty(145);
                DrawProperty(92);
                DrawProperty(93, true);
                MaterialProperty grassManual = matProperties[92];
                if (grassManual.floatValue == 1) targetMat.EnableKeyword("MANUALWIND_ON");
                else targetMat.DisableKeyword("MANUALWIND_ON");
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword("WIND_ON");
        EditorGUILayout.EndToggleGroup();
    }

    private void InnerOutline(string inspector, string keyword, int first, int last)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            if (first > 0)
            {
                EditorGUILayout.BeginVertical(style);
                {
                    for (int i = first; i <= last; i++) DrawProperty(i);

                    EditorGUILayout.Separator();
                    DrawProperty(72, true);
                    MaterialProperty onlyInOutline = matProperties[72];
                    if (onlyInOutline.floatValue == 1) targetMat.EnableKeyword("ONLYINNEROUTLINE_ON");
                    else targetMat.DisableKeyword("ONLYINNEROUTLINE_ON");
                }
                EditorGUILayout.EndVertical();
            }
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void Gradient(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);

            EditorGUILayout.BeginVertical(style);
            {
                DrawProperty(143, true);
                MaterialProperty gradIsRadial = matProperties[143];
                if (gradIsRadial.floatValue == 1)
                {
                    targetMat.EnableKeyword("RADIALGRADIENT_ON");
                    DrawProperty(31);
                    DrawProperty(32);
                    DrawProperty(34);
                    DrawProperty(141);
                }
                else
                {
                    targetMat.DisableKeyword("RADIALGRADIENT_ON");
                    bool simpleGradient = oldKeyWords.Contains("GRADIENT2COL_ON");
                    bool simpleGradToggle = EditorGUILayout.Toggle("2 Color Gradient?", simpleGradient);
                    if (simpleGradient && !simpleGradToggle) targetMat.DisableKeyword("GRADIENT2COL_ON");
                    else if (!simpleGradient && simpleGradToggle) targetMat.EnableKeyword("GRADIENT2COL_ON");
                    DrawProperty(31);
                    DrawProperty(32);
                    if (!simpleGradToggle) DrawProperty(33);
                    DrawProperty(34);
                    if (!simpleGradToggle) DrawProperty(35);
                    if (!simpleGradToggle) DrawProperty(141);
                    DrawProperty(142);
                }
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void Overlay(string inspector, string keyword)
    {
        bool toggle = oldKeyWords.Contains(keyword);
        bool ini = toggle;

        GUIContent effectNameLabel = new GUIContent();
        effectNameLabel.tooltip = keyword + " (C#)";
        effectNameLabel.text = effectCount + "." + inspector;
        toggle = EditorGUILayout.BeginToggleGroup(effectNameLabel, toggle);

        effectCount++;
        if (ini != toggle && !Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        if (toggle)
        {
            targetMat.EnableKeyword(keyword);
            EditorGUILayout.BeginVertical(style);
            {
                bool multModeOn = oldKeyWords.Contains("OVERLAYMULT_ON");
                bool isMultMode = multModeOn;
                isMultMode = GUILayout.Toggle(isMultMode, new GUIContent("Is overlay multiplicative?"));
                if (multModeOn != isMultMode)
                {
                    if (!Application.isPlaying) EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    if (isMultMode)
                    {
                        multModeOn = true;
                        targetMat.EnableKeyword("OVERLAYMULT_ON");
                    }
                    else targetMat.DisableKeyword("OVERLAYMULT_ON");
                }
                if (multModeOn) GUILayout.Label("Overlay is set to multiplicative mode", smallLabelStyle);
                else GUILayout.Label("Overlay is set to additive mode", smallLabelStyle);

                for (int i = 165; i <= 168; i++) DrawProperty(i);
            }
            EditorGUILayout.EndVertical();
        }
        else targetMat.DisableKeyword(keyword);
        EditorGUILayout.EndToggleGroup();
    }

    private void DrawProperty(int index, bool noReset = false)
    {
        MaterialProperty targetProperty = matProperties[index];

        EditorGUILayout.BeginHorizontal();
        {
            GUIContent propertyLabel = new GUIContent();
            propertyLabel.text = targetProperty.displayName;
            propertyLabel.tooltip = targetProperty.name + " (C#)";

            matEditor.ShaderProperty(targetProperty, propertyLabel);

            if (!noReset)
            {
                GUIContent resetButtonLabel = new GUIContent();
                resetButtonLabel.text = "R";
                resetButtonLabel.tooltip = "Resets to default value";
                if (GUILayout.Button(resetButtonLabel, GUILayout.Width(20))) ResetProperty(targetProperty);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    private void ResetProperty(MaterialProperty targetProperty)
    {
        if (originalMaterialCopy == null) originalMaterialCopy = new Material(targetMat.shader);
        if (targetProperty.type == MaterialProperty.PropType.Float || targetProperty.type == MaterialProperty.PropType.Range)
        {
            targetProperty.floatValue = originalMaterialCopy.GetFloat(targetProperty.name);
        }
        else if (targetProperty.type == MaterialProperty.PropType.Vector)
        {
            targetProperty.vectorValue = originalMaterialCopy.GetVector(targetProperty.name);
        }
        else if (targetProperty.type == MaterialProperty.PropType.Color)
        {
            targetProperty.colorValue = originalMaterialCopy.GetColor(targetProperty.name);
        }
        else if (targetProperty.type == MaterialProperty.PropType.Texture)
        {
            targetProperty.textureValue = originalMaterialCopy.GetTexture(targetProperty.name);
        }
    }

    private void DrawLine(Color color, int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += (padding / 2);
        r.x -= 2;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }
}