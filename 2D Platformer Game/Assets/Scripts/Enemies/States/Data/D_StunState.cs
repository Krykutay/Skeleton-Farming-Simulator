using UnityEngine;

[CreateAssetMenu(fileName = "NewStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stunDuration = 3f;

    public float stunKnockbackDuration = 0.2f;
    public float stunKnockbackSpeed = 20f;
    public Vector2 stunKnockbackAngle;
}
