using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
    public class All1TextureOffsetOverTime : MonoBehaviour
    {
        [SerializeField] private string texturePropertyName = "_MainTex";
        [SerializeField] private Vector2 offsetSpeed;

        [SerializeField, Header("If missing will search object Sprite Renderer or UI Image")]
        private Material mat;

        private int textureShaderId;
        private Vector2 currOffset = Vector2.zero;

        void Start()
        {
            //Get material if missing
            if (mat == null)
            {
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                if (sr != null) mat = sr.material;
                else
                {
                    Image i = GetComponent<Image>();
                    if (i != null) mat = i.material;
                }
            }

            //Show error message if material or textureName property error
            //Otherwise cache shader property ID
            if (mat == null) DestroyComponentAndLogError(gameObject.name + " has no valid Material, deleting All1TextureOffsetOverTIme component");
            else
            {
                if (mat.HasProperty(texturePropertyName)) textureShaderId = Shader.PropertyToID(texturePropertyName);
                else DestroyComponentAndLogError(gameObject.name + "'s Material doesn't have a " + texturePropertyName + " property");
            }
        }

        void Update()
        {
            //Update currOffset and update shader property
            currOffset.x += offsetSpeed.x * Time.deltaTime;
            currOffset.y += offsetSpeed.y * Time.deltaTime;
            mat.SetTextureOffset(textureShaderId, currOffset);
        }

        private void DestroyComponentAndLogError(string logError)
        {
            Debug.LogError(logError);
            Destroy(this);
        }
    }
}