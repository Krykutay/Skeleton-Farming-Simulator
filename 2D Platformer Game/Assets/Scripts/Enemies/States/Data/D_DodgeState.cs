using UnityEngine;

[CreateAssetMenu(fileName = "NewDodgeStateData", menuName = "Data/State Data/Dodge State")]
public class D_DodgeState : ScriptableObject
{
    [SerializeField] float _dodgeSpeed = 15f;
    [SerializeField] float _dodgeTime = 0.2f;
    [SerializeField] float _dodgeCooldown = 2f;
    [SerializeField] Vector2 _dodgeAngle = new Vector2(1f, 1f);

    public float dodgeSpeed { get { return _dodgeSpeed; } }
    public float dodgeTime { get { return _dodgeTime; } }
    public float dodgeCooldown { get { return _dodgeCooldown; } }
    public Vector2 dodgeAngle { get { return _dodgeAngle; } }
}
