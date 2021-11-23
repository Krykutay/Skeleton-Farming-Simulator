using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHithoxToWeapon : MonoBehaviour
{
    AggressiveWeapon _weapon;

    void Awake()
    {
        _weapon = GetComponentInParent<AggressiveWeapon>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _weapon.AddToDetected(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _weapon.RemoveFromDetected(collision);
    }

}
