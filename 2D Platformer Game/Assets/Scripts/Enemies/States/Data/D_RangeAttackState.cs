using UnityEngine;

[CreateAssetMenu(fileName = "NewRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackState : ScriptableObject
{
    [SerializeField] float _projectileDamage = 15f;
    [SerializeField] float _projectileSpeed = 20f;
    [SerializeField] float _projectileTravelDistance = 10f;
    [SerializeField] float _rangeAttackCooldown = 0.5f;

    public float projectileDamage { get { return _projectileDamage; } }
    public float projectileSpeed { get { return _projectileSpeed; } }
    public float projectileTravelDistance { get { return _projectileTravelDistance; } }
    public float rangeAttackCooldown { get { return _rangeAttackCooldown; } }
}