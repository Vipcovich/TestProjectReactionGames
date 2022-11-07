using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class LevelTimeLabel : MonoBehaviour
{
    private Text label;

    private void Awake()
    {
        label = GetComponent<Text>();
    }

    private void Update()
    {
        if (LevelRules.Instance)
        {
            TimeSpan time = LevelRules.Instance.Statistics.Data.levelTime;
            label.text = LevelRules.Instance.Statistics.GetLevelTimeStr();
        }
    }
}
