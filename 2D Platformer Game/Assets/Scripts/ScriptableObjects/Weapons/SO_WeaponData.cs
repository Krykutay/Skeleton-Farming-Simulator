using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [SerializeField] float _movementSpeed;

    public int amountOfAttacks { get; protected set; }
    public float movementSpeed { get { return _movementSpeed; } }
}
