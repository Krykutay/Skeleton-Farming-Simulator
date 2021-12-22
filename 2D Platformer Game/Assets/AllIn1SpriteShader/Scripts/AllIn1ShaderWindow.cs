#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

public class AllIn1ShaderWindow : EditorWindow
{
    private const string versionString = "3.3";
    [MenuItem("Window/AllIn1ShaderWindow")]
    public static void ShowAllIn1ShaderWindowWindow()
    {
        GetWindow<AllIn1ShaderWindow>("All In 1 Shader Window");
    }

    public Vector2 scrollPosition = Vector2.zero;

    private DefaultAsset materialTargetFolder = null;
    private GUIStyle style, bigLabel = new GUIStyle();
    private const int bigFontSize = 16;

    enum ShaderTypes
    {
        Default,
        ScaledTime,
        MaskedUI,
        Urp2dRenderer
    }
    ShaderTypes shaderTypes = ShaderTypes.Default;
    bool showUrpWarning = false;
    double warningTime = 0f;

    Texture2D targetNormalImage;
    float normalStrenght = 5f;
    int normalSmoothing = 1;
    int isComputingNormals = 0;

    public enum TextureSizes
    {
        _2 = 2,
        _4 = 4,
        _8 = 8,
        _16 = 16,
        _32 = 32,
        _64 = 64,
        _128 = 128,
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048
    }
    private TextureSizes textureSizes = TextureSizes._128;
    [SerializeField] private Gradient gradient = new Gradient();
    private FilterMode gradientFiltering = FilterMode.Bilinear;

    private void OnGUI()
    {
        style = new GUIStyle(EditorStyles.helpBox);
        style.margin = new RectOffset(0, 0, 0, 0);
        bigLabel = new GUIStyle(EditorStyles.boldLabel);
        bigLabel.fontSize = bigFontSize;

        using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPosition, GUILayout.Width(position.width), GUILayout.Height(position.height)))
        {
            scrollPosition = scrollView.scrollPosition;

            #if UNITY_2019_4_OR_NEWER
            Texture2D imageInspector = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/AllIn1SpriteShader/Textures/CustomEditorImage2.png", typeof(Texture2D));
            #else
            Texture2D imageInspector = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/AllIn1SpriteShader/Textures/CustomEditorImage.png", typeof(Texture2D));
            #endif

            if (imageInspector) GUILayout.Label(imageInspector);

            DrawLine(Color.grey, 1, 3);
            DefaultAssetShader();
            
            DrawLine(Color.grey, 1, 3);
            GUILayout.Label("Material Save Path", bigLabel);
            GUILayout.Space(20);
            GUILayout.Label("Select the folder where new Materials will be saved when the Save Material To Folder button of the asset component is pressed", EditorStyles.boldLabel);
            HandleSaveFolderEditorPref("All1ShaderMaterials", "Assets/AllIn1SpriteShader/Materials", "Material");

            DrawLine(Color.grey, 1, 3);
            GUILayout.Label("Render Material to Image Save Path", bigLabel);
            GUILayout.Space(20);
            EditorGUILayout.BeginHorizontal();
            {
                float scaleSlider = 1;
                if (PlayerPrefs.HasKey("All1ShaderRenderImagesScale")) scaleSlider = PlayerPrefs.GetFloat("All1ShaderRenderImagesScale");
                GUILayout.Label("Rendered Image Texture Scale", GUILayout.MaxWidth(190));
                scaleSlider = EditorGUILayout.Slider(scaleSlider, 0.2f, 5f, GUILayout.MaxWidth(200));
                if (GUILayout.Button("Default Value", GUILayout.MaxWidth(100))) PlayerPrefs.SetFloat("All1ShaderRenderImagesScale", 1f);
                else PlayerPrefs.SetFloat("All1ShaderRenderImagesScale", scaleSlider);
            }
            EditorGUILayout.EndVertical();
            GUILayout.Label("Select the folder where new Images will be saved when the Render Material To Image button of the asset component is pressed", EditorStyles.boldLabel);
            HandleSaveFolderEditorPref("All1ShaderRenderImages", "Assets/AllIn1SpriteShader/Textures", "Images");

            DrawLine(Color.grey, 1, 3);
            NormalMapCreator();
            
            DrawLine(Color.grey, 1, 3);
            GradientCreator();

            GUILayout.Space(10);
            DrawLine(Color.grey, 1, 3);
            GUILayout.Label("Current asset version is " + versionString, EditorStyles.boldLabel);
        }
    }

    private void DefaultAssetShader()
    {
        GUILayout.Label("Default Asset Shader", bigLabel);
        GUILayout.Space(20);
        GUILayout.Label("This is the shader variant that will be assinged by default to Sprites and UI Images when the asset component is added", EditorStyles.boldLabel);

        bool isUrp = false;
        Shader temp = Resources.Load("AllIn1Urp2dRenderer", typeof(Shader)) as Shader;
        if (temp != null) isUrp = true;

        shaderTypes = (ShaderTypes)PlayerPrefs.GetInt("allIn1DefaultShader");
        int previousShaderType = (int)shaderTypes;
        shaderTypes = (ShaderTypes)EditorGUILayout.EnumPopup(shaderTypes, GUILayout.MaxWidth(200));

        if (previousShaderType != (int)shaderTypes)
        {
            if (!isUrp && shaderTypes == ShaderTypes.Urp2dRenderer)
            {
                showUrpWarning = true;
                warningTime = EditorApplication.timeSinceStartup + 5;
            }
            else
            {
                PlayerPrefs.SetInt("allIn1DefaultShader", (int)shaderTypes);
                showUrpWarning = false;
            }
        }

        if (warningTime < EditorApplication.timeSinceStartup) showUrpWarning = false;
        if (isUrp) showUrpWarning = false;
        if (!isUrp && !showUrpWarning && shaderTypes == ShaderTypes.Urp2dRenderer)
        {
            showUrpWarning = true;
            warningTime = EditorApplication.timeSinceStartup + 5;
            shaderTypes = ShaderTypes.Default;
            PlayerPrefs.SetInt("allIn1DefaultShader", (int)shaderTypes);
        }

        if (showUrpWarning) EditorGUILayout.HelpBox(
                "You can't set the URP 2D Renderer variant since you didn't import the URP package available in the asset root folder (SEE DOCUMENTATION)",
                MessageType.Error,
                true);
    }

    private void NormalMapCreator()
    {
        GUILayout.Label("Normal Map Creator", bigLabel);

        GUILayout.Space(20);
        GUILayout.Label("Select the folder where new Normal Maps will be saved when the Create Normal Map button of the asset component is pressed (URP only)", EditorStyles.boldLabel);
        HandleSaveFolderEditorPref("All1ShaderNormals", "Assets/AllIn1SpriteShader/Textures/NormalMaps", "Normal Maps");

        GUILayout.Space(20);
        GUILayout.Label("Assign a sprite you want to create a normal map from. Choose the normal map settings and press the 'Create And Save Normal Map' button", EditorStyles.boldLabel);
        targetNormalImage = (Texture2D)EditorGUILayout.ObjectField("Target Image", targetNormalImage, typeof(Texture2D), false, GUILayout.MaxWidth(225));

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Normal Strenght:", GUILayout.MaxWidth(150));
            normalStrenght = EditorGUILayout.Slider(normalStrenght, 1f, 20f, GUILayout.MaxWidth(400));
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Normal Smoothing:", GUILayout.MaxWidth(150));
            normalSmoothing = EditorGUILayout.IntSlider(normalSmoothing, 0, 3, GUILayout.MaxWidth(400));
        }
        EditorGUILayout.EndHorizontal();

        if (isComputingNormals == 0)
        {
            if (targetNormalImage != null)
            {
                if (GUILayout.Button("Create And Save Normal Map"))
                {
                    isComputingNormals = 1;
                    return;
                }
            }
            else
            {
                GUILayout.Label("Add a Target Image to use this feature", EditorStyles.boldLabel);
            }
        }
        else
        {
            GUILayout.Label("Normal Map is currently being created, be patient", EditorStyles.boldLabel, GUILayout.Height(40));
            Repaint();
            isComputingNormals++;
            if (isComputingNormals > 5)
            {
                string assetPath = AssetDatabase.GetAssetPath(targetNormalImage);
                var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
                if (tImporter != null)
                {
                    tImporter.isReadable = true;
                    tImporter.SaveAndReimport();
                }

                Texture2D normalToSave = CreateNormalMap(targetNormalImage, normalStrenght, normalSmoothing);
                string path = EditorUtility.SaveFilePanel("Save texture as PNG", PlayerPrefs.GetString("All1ShaderNormals"), "normalMap_1.png", "png");
                if (path.Length != 0)
                {
                    byte[] pngData = normalToSave.EncodeToPNG();
                    if (pngData != null) File.WriteAllBytes(path, pngData);
                    AssetDatabase.Refresh();

                    if (path.IndexOf("Assets/") >= 0)
                    {
                        string subPath = path.Substring(path.IndexOf("Assets/"));
                        TextureImporter importer = AssetImporter.GetAtPath(subPath) as TextureImporter;
                        if (importer != null)
                        {
                            Debug.Log("Normal Map saved inside the project: " + subPath);
                            importer.filterMode = FilterMode.Bilinear;
                            importer.textureType = TextureImporterType.NormalMap;
                            importer.wrapMode = TextureWrapMode.Repeat;
                            importer.SaveAndReimport();
                            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(subPath, typeof(Texture)));
                        }
                    }
                    else Debug.Log("Normal Map saved outside the project: " + path);
                }
                isComputingNormals = 0;
            }
        }
        GUILayout.Label("*This process will freeze the editor for some seconds, larger images will take longer", EditorStyles.boldLabel);
    }

    private void HandleSaveFolderEditorPref(string keyName, string defaultPath, string logsFeatureName)
    {
        if (!PlayerPrefs.HasKey(keyName)) PlayerPrefs.SetString(keyName, defaultPath);
        materialTargetFolder = (DefaultAsset)AssetDatabase.LoadAssetAtPath(PlayerPrefs.GetString(keyName), typeof(DefaultAsset));
        if (materialTargetFolder == null)
        {
            PlayerPrefs.SetString(keyName, defaultPath);
            materialTargetFolder = (DefaultAsset)AssetDatabase.LoadAssetAtPath(PlayerPrefs.GetString(keyName), typeof(DefaultAsset));
            if (materialTargetFolder == null)
            {
                materialTargetFolder = (DefaultAsset)AssetDatabase.LoadAssetAtPath("Assets/", typeof(DefaultAsset));
                if (materialTargetFolder == null) Debug.LogError("The desired save folder doesn't exist. Go to Window -> AllIn1ShaderWindow and set a valid folder");
                else PlayerPrefs.SetString("Assets/", defaultPath);
            }
        }
        materialTargetFolder = (DefaultAsset)EditorGUILayout.ObjectField("New " + logsFeatureName + " Folder", materialTargetFolder, typeof(DefaultAsset), false);

        if (materialTargetFolder != null && IsAssetAFolder(materialTargetFolder))
        {
            string path = AssetDatabase.GetAssetPath(materialTargetFolder);
            PlayerPrefs.SetString(keyName, path);
            EditorGUILayout.HelpBox("Valid folder! " + logsFeatureName + " save path: " + path, MessageType.Info, true);
        }
        else EditorGUILayout.HelpBox("Select the new " + logsFeatureName + " Folder", MessageType.Warning, true);
    }

    private void GradientCreator()
    {
        GUILayout.Label("Gradient Creator", bigLabel);
        GUILayout.Space(20);
        GUILayout.Label("This feature can be used to create textures for the Color Ramp Effect", EditorStyles.boldLabel);

        EditorGUILayout.GradientField("Gradient", gradient, GUILayout.Height(25));

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Texture Size:", GUILayout.MaxWidth(145));
            textureSizes = (TextureSizes)EditorGUILayout.EnumPopup(textureSizes, GUILayout.MaxWidth(200));
        }
        EditorGUILayout.EndHorizontal();

        int textureSize = (int)textureSizes;
        Texture2D gradTex = new Texture2D(textureSize, 1, TextureFormat.RGBA32, false);
        for (int i = 0; i < textureSize; i++) gradTex.SetPixel(i, 0, gradient.Evaluate((float)i / (float)textureSize));
        gradTex.Apply();

        GUILayout.Space(20);
        GUILayout.Label("Select the folder where new Gradient Textures will be saved", EditorStyles.boldLabel);
        HandleSaveFolderEditorPref("All1ShaderGradients", "Assets/AllIn1SpriteShader/Textures/GradientTextures", "Gradient");

        string prefSavedPath = PlayerPrefs.GetString("All1ShaderGradients") + "/";
        if (Directory.Exists(prefSavedPath))
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label("Gradient Texture Filtering: ", GUILayout.MaxWidth(170));
                gradientFiltering = (FilterMode)EditorGUILayout.EnumPopup(gradientFiltering, GUILayout.MaxWidth(200));
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save Gradient Texture"))
            {
                string path = EditorUtility.SaveFilePanel("Save texture as PNG", prefSavedPath, "gradientTexture_1.png", "png");
                if (path.Length != 0)
                {
                    byte[] pngData = gradTex.EncodeToPNG();
                    if (pngData != null) File.WriteAllBytes(path, pngData);
                    AssetDatabase.Refresh();

                    if (path.IndexOf("Assets/") >= 0)
                    {
                        string subPath = path.Substring(path.IndexOf("Assets/"));
                        TextureImporter importer = AssetImporter.GetAtPath(subPath) as TextureImporter;
                        if (importer != null)
                        {
                            Debug.Log("Gradient saved inside the project: " + subPath);
                            importer.filterMode = gradientFiltering;
                            importer.SaveAndReimport();
                            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(subPath, typeof(Texture)));
                        }
                    }
                    else Debug.Log("Gradient saved outside the project: " + path);
                }
            }
        }
    }

    private static bool IsAssetAFolder(Object obj)
    {
        string path = "";

        if (obj == null) return false;

        path = AssetDatabase.GetAssetPath(obj.GetInstanceID());

        if (path.Length > 0)
        {
            if (Directory.Exists(path)) return true;
            else return false;
        }
        return false;
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

    private Texture2D CreateNormalMap(Texture2D t, float normalMult = 5f, int normalSmooth = 0)
    {
        Color[] pixels = new Color[t.width * t.height];
        Texture2D texNormal = new Texture2D(t.width, t.height, TextureFormat.RGB24, false, false);
        Vector3 vScale = new Vector3(0.3333f, 0.3333f, 0.3333f);

        for (int y = 0; y < t.height; y++)
        {
            for (int x = 0; x < t.width; x++)
            {
                Color tc = t.GetPixel(x - 1, y - 1);
                Vector3 cSampleNegXNegY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x, y - 1);
                Vector3 cSampleZerXNegY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x + 1, y - 1);
                Vector3 cSamplePosXNegY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x - 1, y);
                Vector3 cSampleNegXZerY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x + 1, y);
                Vector3 cSamplePosXZerY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x - 1, y + 1);
                Vector3 cSampleNegXPosY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x, y + 1);
                Vector3 cSampleZerXPosY = new Vector3(tc.r, tc.g, tc.g);
                tc = t.GetPixel(x + 1, y + 1);
                Vector3 cSamplePosXPosY = new Vector3(tc.r, tc.g, tc.g);
                float fSampleNegXNegY = Vector3.Dot(cSampleNegXNegY, vScale);
                float fSampleZerXNegY = Vector3.Dot(cSampleZerXNegY, vScale);
                float fSamplePosXNegY = Vector3.Dot(cSamplePosXNegY, vScale);
                float fSampleNegXZerY = Vector3.Dot(cSampleNegXZerY, vScale);
                float fSamplePosXZerY = Vector3.Dot(cSamplePosXZerY, vScale);
                float fSampleNegXPosY = Vector3.Dot(cSampleNegXPosY, vScale);
                float fSampleZerXPosY = Vector3.Dot(cSampleZerXPosY, vScale);
                float fSamplePosXPosY = Vector3.Dot(cSamplePosXPosY, vScale);
                float edgeX = (fSampleNegXNegY - fSamplePosXNegY) * 0.25f + (fSampleNegXZerY - fSamplePosXZerY) * 0.5f + (fSampleNegXPosY - fSamplePosXPosY) * 0.25f;
                float edgeY = (fSampleNegXNegY - fSampleNegXPosY) * 0.25f + (fSampleZerXNegY - fSampleZerXPosY) * 0.5f + (fSamplePosXNegY - fSamplePosXPosY) * 0.25f;
                Vector2 vEdge = new Vector2(edgeX, edgeY) * normalMult;
                Vector3 norm = new Vector3(vEdge.x, vEdge.y, 1.0f).normalized;
                Color c = new Color(norm.x * 0.5f + 0.5f, norm.y * 0.5f + 0.5f, norm.z * 0.5f + 0.5f, 1);
                pixels[x + y * t.width] = c;
            }
        }

        if (normalSmooth > 0f)
        {
            float step = 0.00390625f * normalSmooth;
            for (int y = 0; y < t.height; y++)
            {
                for (int x = 0; x < t.width; x++)
                {
                    float pixelsToAverage = 0.0f;
                    Color c = pixels[(x + 0) + ((y + 0) * t.width)];
                    pixelsToAverage++;
                    if (x - normalSmooth > 0)
                    {
                        if (y - normalSmooth > 0)
                        {
                            c += pixels[(x - normalSmooth) + ((y - normalSmooth) * t.width)];
                            pixelsToAverage++;
                        }
                        c += pixels[(x - normalSmooth) + ((y + 0) * t.width)];
                        pixelsToAverage++;
                        if (y + normalSmooth < t.height)
                        {
                            c += pixels[(x - normalSmooth) + ((y + normalSmooth) * t.width)];
                            pixelsToAverage++;
                        }
                    }
                    if (y - normalSmooth > 0)
                    {
                        c += pixels[(x + 0) + ((y - normalSmooth) * t.width)];
                        pixelsToAverage++;
                    }
                    if (y + normalSmooth < t.height)
                    {
                        c += pixels[(x + 0) + ((y + normalSmooth) * t.width)];
                        pixelsToAverage++;
                    }
                    if (x + normalSmooth < t.width)
                    {
                        if (y - normalSmooth > 0)
                        {
                            c += pixels[(x + normalSmooth) + ((y - normalSmooth) * t.width)];
                            pixelsToAverage++;
                        }
                        c += pixels[(x + normalSmooth) + ((y + 0) * t.width)];
                        pixelsToAverage++;
                        if (y + normalSmooth < t.height)
                        {
                            c += pixels[(x + normalSmooth) + ((y + normalSmooth) * t.width)];
                            pixelsToAverage++;
                        }
                    }
                    pixels[x + y * t.width] = c / pixelsToAverage;
                }
            }
        }

        texNormal.SetPixels(pixels);
        texNormal.Apply();
        return texNormal;
    }
}
#endif