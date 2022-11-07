using System;

public partial class Unit : ISaveLoad
{
    [Serializable]
    public class SaveUnit : SaveTransform
    {
        public float health;

        public SaveUnit(Unit unit) : base(unit.transform)
        {
            health = unit.health;
        }

        public void Deserial(Unit unit)
        {
            base.Deserial(unit.transform);
            unit.health = health;
        }
    }

    public SaveLoadObject Save()
    {
        return SaveLoadObject.Save(this, new SaveUnit(this));
    }

    public void Load(SaveLoadObject saveLoadObject)
    {
        saveLoadObject.Load<SaveUnit>()?.Deserial(this);
    }
}

