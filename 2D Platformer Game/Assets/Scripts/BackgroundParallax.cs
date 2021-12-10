using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] float _parallaxEffect = 0;

    Transform _cameraTransform;
    Vector3 _lastCameraPosition;

    Vector3 _deltaMovement;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
    }

    void Start()
    {
        _lastCameraPosition = _cameraTransform.position;
    }

    void LateUpdate()
    {
        _deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += _deltaMovement * _parallaxEffect;
        _lastCameraPosition = _cameraTransform.position;
    }
}
