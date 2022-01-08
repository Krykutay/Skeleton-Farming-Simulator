using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Player Data/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] float _movementSpeed;
    [SerializeField] float _comboDuration;
    [SerializeField] LayerMask _damageable;
    [SerializeField] AttackDetails[] _attackDetails;
    [SerializeField] int[] _initialAttackDamages;
    [SerializeField] int _damageIncreaseAmount = 1;

    public int amountOfAttacks { get; protected set; }
    public float movementSpeed { get { return _movementSpeed; } }
    public float comboDuration { get { return _comboDuration; } }
    public LayerMask damageable { get { return _damageable; } }
    public AttackDetails[] attackDetails { get { return _attackDetails; } }
    public int[] initialAttackDamages { get { return _initialAttackDamages; } }
    public int damageIncreaseAmount { get { return _damageIncreaseAmount; } }

    void OnEnable()
    {
        amountOfAttacks = _attackDetails.Length;
    }

    public void IncreaseDamage()
    {
        for (int i = 0; i < _attackDetails.Length; i++)
        {
            _attackDetails[i].damageAmount++;
        }
    }

    public void SetInitialDamage()
    {
        for (int i = 0; i < _attackDetails.Length; i++)
        {
            _attackDetails[i].damageAmount = _initialAttackDamages[i];
        }
    }
}
