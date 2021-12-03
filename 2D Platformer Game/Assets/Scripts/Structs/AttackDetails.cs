using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AttackDetails
{
    public string attackName;
    public float damageAmount;
    public float stunDamageAmount;

    [Header("Knockback")]
    public float knockbackStrength;
    public Vector2 knockbackAngle;

    [Header("Hit Circle")]
    public float attackRadius;

    [Header("Hit Box")]
    public Vector2 size;

    [System.NonSerialized] public Vector2 position;
}