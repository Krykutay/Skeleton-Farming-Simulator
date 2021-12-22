using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllIn1SpriteShader
{
    public class Demo2AutoScroll : MonoBehaviour
    {
        private Transform[] children;
        public float totalTime;
        public GameObject sceneDescription = null;

        void Start()
        {
            sceneDescription.SetActive(false);
            Camera.main.fieldOfView = 60f;
            children = GetComponentsInChildren<Transform>();
            for (int i = 0; i < children.Length; i++)
            {
                if (children[i].gameObject != gameObject)
                {
                    children[i].gameObject.SetActive(false);
                    children[i].localPosition = Vector3.zero;
                }
            }

            totalTime = totalTime / (float)children.Length;

            StartCoroutine(ScrollElements());
        }

        IEnumerator ScrollElements()
        {
            int i = 0;
            while (true)
            {
                if (children[i].gameObject == gameObject)
                {
                    i = (i + 1) % children.Length;
                    continue;
                }
                children[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(totalTime);
                children[i].gameObject.SetActive(false);
                i = (i + 1) % children.Length;
            }
        }
    }
}