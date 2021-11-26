using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeAttackStateData", menuName = "Data/State Data/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject
{
    [SerializeField] AttackDetails[] _attackDetails;
    [SerializeField] float _meleeAttackCooldown = 0.5f;

    public AttackDetails[] attackDetails { get { return _attackDetails; } }
    public float meleeAttackCooldown { get { return _meleeAttackCooldown; } }
}
