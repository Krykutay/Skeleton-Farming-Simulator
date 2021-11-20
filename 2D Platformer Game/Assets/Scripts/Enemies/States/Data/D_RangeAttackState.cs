using UnityEngine;

[CreateAssetMenu(fileName = "NewRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackState : ScriptableObject
{
    public float projectileDamage = 10f;
    public float projectileSpeed = 20f;
    public float projectileTravelDistance = 10f;
}
