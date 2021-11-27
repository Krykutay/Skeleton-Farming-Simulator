using UnityEngine;

[CreateAssetMenu(fileName = "NewChargeStateData", menuName = "Data/State Data/Charge State")]
public class D_ChargeState : ScriptableObject
{
    [SerializeField] float _chargeSpeed = 5f;
    [SerializeField] float _chargeTime = 1f;

    public float chargeSpeed { get { return _chargeSpeed; } }
    public float chargeTime { get { return _chargeTime; } }
}
