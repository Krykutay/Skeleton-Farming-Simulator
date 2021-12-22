using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AllIn1SpriteShader
{
    [ExecuteInEditMode]
    public class SetAtlasUvs : MonoBehaviour
    {
        [SerializeField] private bool updateEveryFrame = false;
        private Renderer render;
        private SpriteRenderer spriteRender;
        private Image uiImage;
        private bool isUI = false;

        void Start()
        {
            Setup();
        }

        private void Reset()
        {
            Setup();
        }

        private void Setup()
        {
            if (GetRendererReferencesIfNeeded()) GetAndSetUVs();
            if (!updateEveryFrame && Application.isPlaying && this != null) this.enabled = false;
        }

        void OnWillRenderObject()
        {
            if (updateEveryFrame)
            {
                GetAndSetUVs();
            }
        }

        public void GetAndSetUVs()
        {
            if (!GetRendererReferencesIfNeeded()) return;

            if (!isUI)
            {
                Rect r = spriteRender.sprite.textureRect;
                r.x /= spriteRender.sprite.texture.width;
                r.width /= spriteRender.sprite.texture.width;
                r.y /= spriteRender.sprite.texture.height;
                r.height /= spriteRender.sprite.texture.height;

                render.sharedMaterial.SetFloat("_MinXUV", r.xMin);
                render.sharedMaterial.SetFloat("_MaxXUV", r.xMax);
                render.sharedMaterial.SetFloat("_MinYUV", r.yMin);
                render.sharedMaterial.SetFloat("_MaxYUV", r.yMax);
            }
            else
            {
                Rect r = uiImage.sprite.textureRect;
                r.x /= uiImage.sprite.texture.width;
                r.width /= uiImage.sprite.texture.width;
                r.y /= uiImage.sprite.texture.height;
                r.height /= uiImage.sprite.texture.height;

                uiImage.material.SetFloat("_MinXUV", r.xMin);
                uiImage.material.SetFloat("_MaxXUV", r.xMax);
                uiImage.material.SetFloat("_MinYUV", r.yMin);
                uiImage.material.SetFloat("_MaxYUV", r.yMax);
            }
        }

        public void ResetAtlasUvs()
        {
            if (!GetRendererReferencesIfNeeded()) return;

            if (!isUI)
            {
                render.sharedMaterial.SetFloat("_MinXUV", 0f);
                render.sharedMaterial.SetFloat("_MaxXUV", 1f);
                render.sharedMaterial.SetFloat("_MinYUV", 0f);
                render.sharedMaterial.SetFloat("_MaxYUV", 1f);
            }
            else
            {
                uiImage.material.SetFloat("_MinXUV", 0f);
                uiImage.material.SetFloat("_MaxXUV", 1f);
                uiImage.material.SetFloat("_MinYUV", 0f);
                uiImage.material.SetFloat("_MaxYUV", 1f);
            }
        }

        public void UpdateEveryFrame(bool everyFrame)
        {
            updateEveryFrame = everyFrame;
        }

        private bool GetRendererReferencesIfNeeded()
        {
            if (spriteRender == null) spriteRender = GetComponent<SpriteRenderer>();
            if (spriteRender != null)
            {
                if (spriteRender.sprite == null)
                {
                    #if UNITY_EDITOR
                    EditorUtility.DisplayDialog("No sprite found", "The object: " + gameObject.name + ",has Sprite Renderer but no sprite", "Ok");
                    #endif
                    DestroyImmediate(this);
                    return false;
                }
                if (render == null) render = GetComponent<Renderer>();
                isUI = false;
            }
            else
            {
                if (uiImage == null)
                {
                    uiImage = GetComponent<Image>();
                    if (uiImage != null)
                    {
                        #if UNITY_EDITOR
                        Debug.Log("You added the SetAtlasUv component to: " + gameObject.name + " that has a UI Image\n " +
                        "This SetAtlasUV component will only work properly on UI Images if each Image has a DIFFERENT material instance (See Documentation Sprite Atlases section for more info)");
                        #endif
                    }
                    else
                    {
                        #if UNITY_EDITOR
                        EditorUtility.DisplayDialog("No Valid Renderer Component Found", "Looks like you have no Sprite Renderer or UI Image on: '"
                            + gameObject.name + "'\n This SetAtlasUV component will now get destroyed", "Ok");
                        #endif
                        DestroyImmediate(this);
                        return false;
                    }
                }
                if (render == null) render = GetComponent<Renderer>();
                isUI = true;
            }

            if (spriteRender == null && uiImage == null)
            {
                #if UNITY_EDITOR
                EditorUtility.DisplayDialog("No Sprite Renderer", "Looks like you are missing a Sprite Renderer on: '"
                    + gameObject.name + "'\n SetAtlasUvs only works with Sprite Renderers since it needs a 'sharedMaterial' property that UI images lack." +
                    " This SetAtlasUV component will now get destroyed", "Ok");
                #endif
                DestroyImmediate(this);
                return false;
            }
            return true;
        }
    }
}