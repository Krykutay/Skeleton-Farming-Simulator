using System.Collections;
using UnityEngine;

namespace AllIn1SpriteShader
{
    public class DemoCamera : MonoBehaviour
    {
        [SerializeField] private Transform targetedItem = null;
        [SerializeField] private All1ShaderDemoController demoController = null;
        [SerializeField] private float speed = 0;
        private Vector3 offset;
        private Vector3 target;
        private bool canUpdate = false;

        void Awake()
        {
            offset = transform.position - targetedItem.position;
            StartCoroutine(SetCamAfterStart());
        }

        private void Update()
        {
            if (!canUpdate) return;
            target.y = demoController.GetCurrExpositor() * demoController.expositorDistance;
            transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        }

        IEnumerator SetCamAfterStart()
        {
            yield return null;
            transform.position = targetedItem.position + offset;
            target = transform.position;
            canUpdate = true;
        }
    }
}