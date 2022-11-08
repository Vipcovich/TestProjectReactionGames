using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImg;
    [SerializeField] private Image healthBkgImg;
    [SerializeField] private Text healthValueLabel;
    [SerializeField] private float healthStep = 6;

    private void UpdateValue(PlayerUnit playerUnit)
    {
        if (healthValueLabel)
        {
            healthValueLabel.enabled = playerUnit;
            healthValueLabel.text = string.Format("{0}/{1}", (int)(playerUnit?.Health ?? 0), (int)(playerUnit?.MaxHealth ?? 0));
        }
    }

    private void UpdateHealthBkgImg(PlayerUnit playerUnit)
    {
        if (!healthBkgImg)
        {
            return;
        }

        healthBkgImg.enabled = playerUnit;
        if (!playerUnit)
        {
            return;
        }

        Vector2 sizeDelta = healthBkgImg.rectTransform.sizeDelta;
        sizeDelta.x = playerUnit.MaxHealth * healthStep;
        healthBkgImg.rectTransform.sizeDelta = sizeDelta;
    }

    private void UpdateHealthImg(PlayerUnit playerUnit)
    {
        if (!healthImg)
        {
            return;
        }

        healthImg.enabled = playerUnit;
        if (!playerUnit)
        {
            return;
        }

        float precent = 0f;
        float health = playerUnit.Health;
        float maxHealth = Mathf.Max(Mathf.Epsilon, playerUnit.MaxHealth);
        precent = Mathf.Clamp01(health / maxHealth);

        Vector3 scale = healthImg.transform.localScale;
        if (!Mathf.Approximately(precent, scale.x))
        {
            scale.x = precent;
            healthImg.transform.localScale = scale;
        }
    }

    private void Update()
    {
        PlayerUnit playerUnit = PlayerUnitCollection.Instance?.Current;

        UpdateHealthBkgImg(playerUnit);
        UpdateHealthImg(playerUnit);
        UpdateValue(playerUnit);
    }
}
