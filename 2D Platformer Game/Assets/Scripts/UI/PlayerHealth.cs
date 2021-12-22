using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image[] healthIndicators;

    void OnEnable()
    {
        for (int i = 0; i < healthIndicators.Length ; i++)
        {
            EnableHealthIndicators(i);
        }

        SetHealthIndicatorColor();
    }

    public void EnableHealthIndicators(int count)
    {
        if (count < Player.Instance.maxHealth)
        {
            healthIndicators[count].enabled = true;
        }
        else
        {
            healthIndicators[count].enabled = false;
        }
    }

    public void SetHealthIndicatorColor()
    {
        for (int i = 0; i < Player.Instance.maxHealth; i++)
        {
            if (i < Player.Instance.currentHealth)
            {
                healthIndicators[i].color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                healthIndicators[i].color = new Color(0f, 0f, 0f, 0.4f);
            }
        }

        if (Player.Instance.currentHealth % 1 == 0.5f)
            healthIndicators[(int)Player.Instance.currentHealth].color = new Color(0.7f, 0.7f, 0.7f, 0.8f);
    }

}
