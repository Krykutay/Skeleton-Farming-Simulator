using UnityEngine;

[CreateAssetMenu(fileName = "NewDeadStateData", menuName = "Data/State Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    [SerializeField] float _corpseDuration = 2f;

    public float corpseDuration { get { return _corpseDuration; } }
}
