using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerDetectedStateData", menuName = "Data/State Data/Player Detected State")]
public class D_PlayerDetectedState : ScriptableObject
{
    [SerializeField] float _longRangeActionTime = 1.5f;

    public float LongRangeActionTime { get { return _longRangeActionTime; } }
}
