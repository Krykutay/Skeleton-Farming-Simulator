using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] AttackDetails[] _attackDetails;

    public AttackDetails[] attackDetails { get { return _attackDetails; } }

    void OnEnable()
    {
        amountOfAttacks = _attackDetails.Length;
    }
}
