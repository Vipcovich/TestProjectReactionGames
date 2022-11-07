using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadPanel : MonoBehaviour
{
    [SerializeField] private Text titleLabel;
    [SerializeField] private Button closeButton;
    [Space]
    [SerializeField] private List<Button> slots = new List<Button>();

    public bool IsSave { get; set; } = false;

    private void OnEnable()
    {
        if (titleLabel)
        {
            titleLabel.text = IsSave ? "Save" : "Load";
        }

        if (closeButton)
        {
            closeButton.onClick.AddListener(Close);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            Button slot = slots[i];
            slot.onClick.AddListener(() => OnClickSlot(slot));
        }
        RepaintSlots();
    }

    private void OnDisable()
    {
        if (closeButton)
        {
            closeButton.onClick.RemoveListener(Close);
        }
    }

    private void RepaintSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Button slot = slots[i];
            SetSlotTitle(slot, i);
        }
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }

    private void SetSlotTitle(Button slot, int idx)
    {
        slot.interactable = IsSave || (SaveLoadManager.Instance?.HasKey(idx) ?? false);

        Text text = slot.GetComponentInChildren<Text>();
        if (text)
        {
            text.text = SaveLoadManager.Instance?.GetTitle(idx);
        }
    }

    private void OnClickSlot(Button slot)
    {
        int slotIdx = GetSlotIdx(slot);
        if (IsSave)
        {
            SaveLoadManager.Instance?.Save(slotIdx);
        }
        else
        {
            SaveLoadManager.Instance?.Load(slotIdx);
        }

        Close();
    }

    private int GetSlotIdx(Button slot)
    {
        return slots.FindIndex(obj => obj == slot);
    }
}
