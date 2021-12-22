using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(AllIn1Shader)), CanEditMultipleObjects]
public class AllIn1ShaderScriptEditor : Editor
{
    bool showUrpWarning = false;
    double warningTime = 0f;
    SerializedProperty m_NormalStrength, m_NormalSmoothing;

    private void OnEnable()
    {
        m_NormalStrength = serializedObject.FindProperty("normalStrenght");
        m_NormalSmoothing = serializedObject.FindProperty("normalSmoothing");
    }

    public override void OnInspectorGUI()
    {
        #if UNITY_2019_4_OR_NEWER
        Texture2D imageInspector = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/AllIn1SpriteShader/Textures/CustomEditorImage2.png", typeof(Texture2D));
        #else
        Texture2D imageInspector = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/AllIn1SpriteShader/Textures/CustomEditorImage.png", typeof(Texture2D));
        #endif

        if (imageInspector)
        {
            Rect rect;
            float imageHeight = imageInspector.height;
            float imageWidth = imageInspector.width;
            float aspectRatio = imageHeight / imageWidth;
            rect = GUILayoutUtility.GetRect(imageHeight, aspectRatio * Screen.width * 0.7f);
            EditorGUI.DrawTextureTransparent(rect, imageInspector);
        }

        AllIn1Shader myScript = (AllIn1Shader)target;

        SetCurrentShaderType(myScript);

        if (GUILayout.Button("Deactivate All Effects"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).ClearAllKeywords();
            }
        }


        if (GUILayout.Button("New Clean Material"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).TryCreateNew();
            }
        }


        if (GUILayout.Button("Create New Material With Same Properties (SEE DOC)"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).MakeCopy();
            }
        }

        if (GUILayout.Button("Save Material To Folder (SEE DOC)"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).SaveMaterial();
            }
        }

        if (GUILayout.Button("Apply Material To All Children"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).ApplyMaterialToHierarchy();
            }
        }

        if (myScript.shaderTypes != AllIn1Shader.ShaderTypes.Urp2dRenderer)
        {
            if (GUILayout.Button("Render Material To Image"))
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    (targets[i] as AllIn1Shader).RenderToImage();
                }
            }
        }

        bool isUrp = false;
        Shader temp = Resources.Load("AllIn1Urp2dRenderer", typeof(Shader)) as Shader;
        if (temp != null) isUrp = true;
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Change Shader Variant:", GUILayout.MaxWidth(140));
            int previousShaderType = (int)myScript.shaderTypes;
            myScript.shaderTypes = (AllIn1Shader.ShaderTypes)EditorGUILayout.EnumPopup(myScript.shaderTypes);
            if (previousShaderType != (int)myScript.shaderTypes)
            {
                for (int i = 0; i < targets.Length; i++) (targets[i] as AllIn1Shader).CheckIfValidTarget();
                if (myScript == null) return;
                if (isUrp || myScript.shaderTypes != AllIn1Shader.ShaderTypes.Urp2dRenderer)
                {
                    Debug.Log(myScript.gameObject.name + " shader variant has been changed to: " + myScript.shaderTypes);
                    myScript.SetSceneDirty();

                    Renderer sr = myScript.GetComponent<Renderer>();
                    if (sr != null)
                    {
                        if (sr.sharedMaterial != null)
                        {
                            if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.Default) sr.sharedMaterial.shader = Resources.Load("AllIn1SpriteShader", typeof(Shader)) as Shader;
                            else if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.ScaledTime) sr.sharedMaterial.shader = Resources.Load("AllIn1SpriteShaderScaledTime", typeof(Shader)) as Shader;
                            else if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.MaskedUI) sr.sharedMaterial.shader = Resources.Load("AllIn1SpriteShaderUiMask", typeof(Shader)) as Shader;
                            else if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.Urp2dRenderer) sr.sharedMaterial.shader = Resources.Load("AllIn1Urp2dRenderer", typeof(Shader)) as Shader;
                            else SetCurrentShaderType(myScript);
                        }
                    }
                    else
                    {
                        Graphic img = myScript.GetComponent<Graphic>();
                        if (img != null && img.material != null)
                        {
                            if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.Default) img.material.shader = Resources.Load("AllIn1SpriteShader", typeof(Shader)) as Shader;
                            else if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.ScaledTime) img.material.shader = Resources.Load("AllIn1SpriteShaderScaledTime", typeof(Shader)) as Shader;
                            else if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.MaskedUI) img.material.shader = Resources.Load("AllIn1SpriteShaderUiMask", typeof(Shader)) as Shader;
                            else if (myScript.shaderTypes == AllIn1Shader.ShaderTypes.Urp2dRenderer) img.material.shader = Resources.Load("AllIn1Urp2dRenderer", typeof(Shader)) as Shader;
                            else SetCurrentShaderType(myScript);
                        }
                    }
                }
                else if(!isUrp && myScript.shaderTypes == AllIn1Shader.ShaderTypes.Urp2dRenderer)
                {
                    myScript.shaderTypes = (AllIn1Shader.ShaderTypes) previousShaderType;
                    showUrpWarning = true;
                    warningTime = EditorApplication.timeSinceStartup + 5;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        if (warningTime < EditorApplication.timeSinceStartup) showUrpWarning = false;
        if (isUrp) showUrpWarning = false;
        if (showUrpWarning) EditorGUILayout.HelpBox(
                "You can't set the URP 2D Renderer variant since you didn't import the URP package available in the asset root folder (SEE DOCUMENTATION)",
                MessageType.Error,
                true);

        if (isUrp && myScript.shaderTypes == AllIn1Shader.ShaderTypes.Urp2dRenderer)
        {
            EditorGUILayout.Space();
            DrawLine(Color.grey, 1, 3);
            EditorGUILayout.Space();

            if (GUILayout.Button("Create And Add Normal Map"))
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    (targets[i] as AllIn1Shader).CreateAndAssignNormalMap();
                }
            }
            serializedObject.Update();
            EditorGUILayout.PropertyField(m_NormalStrength, new GUIContent("Normal Strength"), GUILayout.Height(20));
            EditorGUILayout.PropertyField(m_NormalSmoothing, new GUIContent("Normal Blur"), GUILayout.Height(20));
            if (myScript.computingNormal)
            {
                EditorGUILayout.LabelField("Normal Map is currently being created, be patient", EditorStyles.boldLabel, GUILayout.Height(40));
            }
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
        }

        DrawLine(Color.grey, 1, 3);
        EditorGUILayout.Space();

        if (GUILayout.Button("Sprite Atlas Auto Setup"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).ToggleSetAtlasUvs(true);
            }
        }
        if (GUILayout.Button("Remove Sprite Atlas Configuration"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).ToggleSetAtlasUvs(false);
            }
        }

        EditorGUILayout.Space();
        DrawLine(Color.grey, 1, 3);

        if (GUILayout.Button("REMOVE COMPONENT AND MATERIAL"))
        {
            for (int i = 0; i < targets.Length; i++)
            {
                (targets[i] as AllIn1Shader).CleanMaterial();
            }
            for (int i = targets.Length - 1; i >= 0; i--)
            {
                DestroyImmediate(targets[i] as AllIn1Shader);
            }
        }
    }

    private void SetCurrentShaderType(AllIn1Shader myScript)
    {
        string shaderName = "";
        Renderer sr = myScript.GetComponent<Renderer>();
        if (sr != null)
        {
            shaderName = sr.sharedMaterial.shader.name;
        }
        else
        {
            Graphic img = myScript.GetComponent<Graphic>();
            if (img != null) shaderName = img.material.shader.name;
        }
        shaderName = shaderName.Replace("AllIn1SpriteShader/", "");

        if (shaderName.Equals("AllIn1SpriteShader")) myScript.shaderTypes = AllIn1Shader.ShaderTypes.Default;
        else if (shaderName.Equals("AllIn1SpriteShaderScaledTime")) myScript.shaderTypes = AllIn1Shader.ShaderTypes.ScaledTime;
        else if (shaderName.Equals("AllIn1SpriteShaderUiMask")) myScript.shaderTypes = AllIn1Shader.ShaderTypes.MaskedUI;
        else if (shaderName.Equals("AllIn1Urp2dRenderer")) myScript.shaderTypes = AllIn1Shader.ShaderTypes.Urp2dRenderer;
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