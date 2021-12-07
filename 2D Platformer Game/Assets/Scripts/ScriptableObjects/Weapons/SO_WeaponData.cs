using UnityEngine;

public class SO_WeaponData : ScriptableObject
{
    [SerializeField] float _movementSpeed;
    [SerializeField] float _comboDuration;

    public int amountOfAttacks { get; protected set; }
    public float movementSpeed { get { return _movementSpeed; } }
    public float comboDuration { get { return _comboDuration; } }
}
