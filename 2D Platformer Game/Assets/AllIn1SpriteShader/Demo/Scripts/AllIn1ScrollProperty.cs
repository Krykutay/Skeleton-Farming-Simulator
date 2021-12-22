using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
    public class AllIn1ScrollProperty : MonoBehaviour
    {
        [SerializeField] private string numericPropertyName = "_RotateUvAmount";
        [SerializeField] private float scrollSpeed;

        [Space, SerializeField] private bool applyModulo;
        [SerializeField] private float modulo = 1f;

        [Space, SerializeField, Header("If missing will search object Sprite Renderer or UI Image")]
        private Material mat;

        private int propertyShaderID;
        private float currValue;

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

            //Show error message if material or numericPropertyName property error
            //Otherwise cache shader property ID
            if (mat == null) DestroyComponentAndLogError(gameObject.name + " has no valid Material, deleting All1TextureOffsetOverTIme component");
            else
            {
                if (mat.HasProperty(numericPropertyName)) propertyShaderID = Shader.PropertyToID(numericPropertyName);
                else DestroyComponentAndLogError(gameObject.name + "'s Material doesn't have a " + numericPropertyName + " property");

                currValue = mat.GetFloat(propertyShaderID);
            }
        }

        void Update()
        {
            //Update currOffset and update shader property
            currValue += scrollSpeed * Time.deltaTime;
            if (applyModulo) currValue %= modulo;
            mat.SetFloat(propertyShaderID, currValue);
        }

        private void DestroyComponentAndLogError(string logError)
        {
            Debug.LogError(logError);
            Destroy(this);
        }
    }
}