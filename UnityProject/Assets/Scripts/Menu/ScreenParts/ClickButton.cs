using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public abstract class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Button button;
    private bool pressed = false;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
    }

    protected virtual void OnEnable()
    {
        button.onClick.AddListener(OnClick);
    }

    protected virtual void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!pressed)
        {
            pressed = true;
            OnDown();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (pressed)
        {
            pressed = false;
            OnUp();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerUp(eventData);
    }

    protected virtual void OnClick() { }
    protected virtual void OnDown() { }
    protected virtual void OnUp() { }
}
