using UnityEngine;

[CreateAssetMenu(fileName = "NewDeadStateData", menuName = "Data/State Data/Dead State")]
public class D_DeadState : ScriptableObject
{
    [SerializeField] float _respawnTime = 4f;

    public float respawnTime { get { return _respawnTime; } }
}
