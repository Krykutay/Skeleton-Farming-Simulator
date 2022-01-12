using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] Vector2 _parallaxEffect;
    [SerializeField] bool _infiniteHorizontal;
    [SerializeField] bool _infiniteVertical;

    Transform _cameraTransform;
    Vector3 _lastCameraPosition;

    Vector3 _deltaMovement;
    Vector3 _workSpace;
    float _textureUnitSizeX;
    float _textureUnitSizeY;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        _textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    void Start()
    {
        _lastCameraPosition = _cameraTransform.position;
    }

    void LateUpdate()
    {
        _deltaMovement = _cameraTransform.position - _lastCameraPosition;
        _workSpace.Set(_deltaMovement.x * _parallaxEffect.x, _deltaMovement.y * _parallaxEffect.y, _deltaMovement.z);
        transform.position -= _workSpace;
        _lastCameraPosition = _cameraTransform.position;

        if (_infiniteHorizontal)
        {
            if (Mathf.Abs(_cameraTransform.position.x - transform.position.x) >= _textureUnitSizeX)
            {
                float offsetPositionX = (_cameraTransform.position.x - transform.position.x) % _textureUnitSizeX;
                transform.position = new Vector3(_cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }


        if (_infiniteVertical)
        {
            if (Mathf.Abs(_cameraTransform.position.y - transform.position.y) >= _textureUnitSizeY)
            {
                float offsetPositionY = (_cameraTransform.position.y - transform.position.y) % _textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, _cameraTransform.position.y + offsetPositionY);
            }
        }
    }
}
