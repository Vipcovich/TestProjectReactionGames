using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsLabels : MonoBehaviour
{
    [SerializeField] private Text statusLabel;
    [SerializeField] private Text timeLabel;
    [SerializeField] private Text enemiesLabel;

    private Dictionary<Text, string> prefixLabels = new Dictionary<Text, string>();

    private void Awake()
    {
        prefixLabels[statusLabel]  = statusLabel.text;
        prefixLabels[timeLabel]    = timeLabel.text;
        prefixLabels[enemiesLabel] = enemiesLabel.text;
    }

    private void OnEnable()
    {
        if (LevelRules.Instance?.Statistics)
        {
            SetTexts(LevelRules.Instance?.Statistics);
        }
    }

    private void SetTexts(LevelStatistics statistics)
    {
        SetLabel(statusLabel,   statistics.Data.statusWin ? "win! =)" : "lost! ='(");
        SetLabel(timeLabel,     statistics.GetLevelTimeStr());
        SetLabel(enemiesLabel,  statistics.GetCreateKillEnemiesStr());
    }

    private void SetLabel(Text label, string value)
    {
        prefixLabels.TryGetValue(label, out string prefix);
        label.text = string.Format("{0} {1}", prefix ?? string.Empty, value);
    }
}
