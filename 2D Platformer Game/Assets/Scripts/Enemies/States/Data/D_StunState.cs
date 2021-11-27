using UnityEngine;

[CreateAssetMenu(fileName = "NewStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    [SerializeField] float _stunDuration = 1.5f;
    [SerializeField] float _stunKnockbackDuration = 0.2f;
    [SerializeField] float _stunKnockbackSpeed = 12f;
    [SerializeField] Vector2 _stunKnockbackAngle = new Vector2(1f, 1f);

    public float stunDuration { get { return _stunDuration; } }
    public float stunKnockbackDuration { get { return _stunKnockbackDuration; } }
    public float stunKnockbackSpeed { get { return _stunKnockbackSpeed; } }
    public Vector2 stunKnockbackAngle { get { return _stunKnockbackAngle; } }
}
