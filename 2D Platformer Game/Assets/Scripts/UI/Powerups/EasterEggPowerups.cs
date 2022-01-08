using UnityEngine;

public class EasterEggPowerups : MonoBehaviour
{
    enum PowerupType
    {
        Vaporize,
        Damage,
        Shield,
        Health
    }

    [SerializeField] PowerupType powerupType;

    void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.Play(SoundManager.SoundTags.Powerup);

        if (powerupType == PowerupType.Vaporize)
            PowerupManager.Instance.VaporizePowerupCollected();
        else if (powerupType == PowerupType.Damage)
            PowerupManager.Instance.DamagePowerupCollected();
        else if (powerupType == PowerupType.Shield)
            PowerupManager.Instance.ShieldPowerupCollected();
        else
            PowerupManager.Instance.HealthPowerupCollected();

        Destroy(gameObject);
    }
}
