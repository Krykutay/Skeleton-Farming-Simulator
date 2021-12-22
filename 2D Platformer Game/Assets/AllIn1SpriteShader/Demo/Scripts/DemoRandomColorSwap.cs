using UnityEngine;

namespace AllIn1SpriteShader
{
    public class DemoRandomColorSwap : MonoBehaviour
    {
        private Material mat;

        void Start()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                mat = GetComponent<Renderer>().material;
                if (mat != null) InvokeRepeating("NewColor", 0.0f, 0.6f);
                else
                {
                    Debug.LogError("No material found");
                    Destroy(this);
                }
            }
        }

        private void NewColor()
        {
            mat.SetColor("_ColorSwapRed", GenerateColor());
            mat.SetColor("_ColorSwapGreen", GenerateColor());
            mat.SetColor("_ColorSwapBlue", GenerateColor());
        }

        private Color GenerateColor()
        {
            return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }
}