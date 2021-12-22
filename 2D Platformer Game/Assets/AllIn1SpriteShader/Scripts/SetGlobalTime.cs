using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpriteShader
{
    [ExecuteInEditMode]
    public class SetGlobalTime : MonoBehaviour
    {
        int globalTime;

        void Start()
        {
            globalTime = Shader.PropertyToID("globalUnscaledTime");
        }

        void Update()
        {
            Shader.SetGlobalFloat(globalTime, Time.time / 20f);
        }
    }
}