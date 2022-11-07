using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerUnitNameLabel : MonoBehaviour
{
    private Text playerUnitName;

    private void Awake()
    {
        playerUnitName = GetComponent<Text>();
    }

    private void Start()
    {
        PlayerUnitCollection.OnSelectPlayerUnit += OnSelectPlayerUnit;
        OnSelectPlayerUnit(PlayerUnitCollection.Instance?.Current);
    }

    private void OnDestroy()
    {
        PlayerUnitCollection.OnSelectPlayerUnit -= OnSelectPlayerUnit;
    }

    private void OnSelectPlayerUnit(PlayerUnit playerUnit)
    {
        playerUnitName.text = playerUnit?.Name ?? string.Empty;
    }
}
