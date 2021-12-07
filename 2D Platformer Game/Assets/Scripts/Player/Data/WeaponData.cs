using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Player Data/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [SerializeField] float _movementSpeed;
    [SerializeField] float _comboDuration;
    [SerializeField] LayerMask _damageable;
    [SerializeField] AttackDetails[] _attackDetails;

    public int amountOfAttacks { get; protected set; }
    public float movementSpeed { get { return _movementSpeed; } }
    public float comboDuration { get { return _comboDuration; } }
    public LayerMask damageable { get { return _damageable; } }
    public AttackDetails[] attackDetails { get { return _attackDetails; } }

    void OnEnable()
    {
        amountOfAttacks = _attackDetails.Length;
    }
}
