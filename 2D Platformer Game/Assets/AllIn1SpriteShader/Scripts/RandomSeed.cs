using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpriteShader
{
    public class RandomSeed : MonoBehaviour
    {
        //If you want to randomize UI Images, you'll need to create different materials
        void Start()
        {
            Renderer sr = GetComponent<Renderer>();
            if (sr != null)
            {
                if (sr.material != null)
                {
                    sr.material.SetFloat("_RandomSeed", Random.Range(0, 1000f));
                }
                else Debug.LogError("Missing Renderer or Material: " + gameObject.name);
            }
            else
            {
                Image i = GetComponent<Image>();
                if (i != null)
                {
                    if (i.material != null)
                    {
                        i.material.SetFloat("_RandomSeed", Random.Range(0, 1000f));
                    }
                    else Debug.LogError("Missing Material on UI Image: " + gameObject.name);
                }
                else Debug.LogError("Missing Renderer or UI Image on: " + gameObject.name);
            }
        }
    }
}