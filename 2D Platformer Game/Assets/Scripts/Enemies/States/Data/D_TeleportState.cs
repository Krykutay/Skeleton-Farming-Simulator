using UnityEngine;

[CreateAssetMenu(fileName = "NewTeleportStateData", menuName = "Data/State Data/Teleport State")]
public class D_TeleportState : ScriptableObject
{
    [SerializeField] float _teleportSpeed = 3f;
    [SerializeField] float _teleportDuration = 1f;
    [SerializeField] float _teleportCooldown = 2f;

    public float teleportSpeed { get { return _teleportSpeed; } }
    public float teleportDuration { get { return _teleportDuration; } }
    public float teleportCooldown { get { return _teleportCooldown; } }
}
