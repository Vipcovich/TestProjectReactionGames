using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthImg;

    private void Update()
    {
        if (!healthImg)
        {
            return;
        }

        float precent = 0f;

        PlayerUnit playerUnit = PlayerUnitCollection.Instance?.Current;
        if (playerUnit)
        {
            float health = playerUnit.Health;
            float maxHealth = Mathf.Max(Mathf.Epsilon, playerUnit.MaxHealth);
            precent = Mathf.Clamp01(health / maxHealth);
        }

        Vector3 scale = healthImg.transform.localScale;
        if (!Mathf.Approximately(precent, scale.x))
        {
            scale.x = precent;
            healthImg.transform.localScale = scale;
        }
    }
}
