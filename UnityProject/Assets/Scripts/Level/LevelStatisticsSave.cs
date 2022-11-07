using System;

public partial class LevelStatistics : ISaveLoad
{
    [Serializable]
    public class SaveStatisticsData
    {
        public LevelStatisticsData StatData;

        public SaveStatisticsData(LevelStatistics levelStatistics)
        {
            StatData = levelStatistics.Data;
        }

        public void Deserial(LevelStatistics levelStatistics)
        {
            levelStatistics.Data = StatData;
        }
    }

    public SaveLoadObject Save()
    {
        return SaveLoadObject.Save(this, new SaveStatisticsData(this));
    }

    public void Load(SaveLoadObject saveLoadObject)
    {
        saveLoadObject.Load<SaveStatisticsData>()?.Deserial(this);
    }
}
