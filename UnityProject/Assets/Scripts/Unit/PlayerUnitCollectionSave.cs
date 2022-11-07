using System;

public partial class PlayerUnitCollection : ISaveLoad
{
    [Serializable]
    public class SavePlayerUnitCollection
    {
        public string selectPlayerName;

        public SavePlayerUnitCollection(PlayerUnitCollection playerUnitCollection)
        {
            selectPlayerName = playerUnitCollection.Current?.Name ?? string.Empty;
        }

        public void Deserial(PlayerUnitCollection playerUnitCollection)
        {
            if (!string.IsNullOrEmpty(selectPlayerName))
            {
                PlayerUnit playerUnit = playerUnitCollection.playerUnits.Find(obj => obj.Name == selectPlayerName);
                playerUnitCollection.SelectCurrentPlayer(playerUnit);
            }
        }
    }

    public SaveLoadObject Save()
    {
        return SaveLoadObject.Save(this, new SavePlayerUnitCollection(this));
    }

    public void Load(SaveLoadObject saveLoadObject)
    {
        saveLoadObject.Load<SavePlayerUnitCollection>()?.Deserial(this);
    }
}
