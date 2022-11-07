using System;
using UnityEngine;

public partial class Bullet : ISaveLoad
{
    [Serializable]
    public class SaveBullet : SaveTransform
    {
        public float[] StartPosition = new float[3];

        public SaveBullet(Bullet bullet) : base(bullet.transform)
        {
            StartPosition[0] = bullet.startPos.x;
            StartPosition[1] = bullet.startPos.y;
            StartPosition[2] = bullet.startPos.z;
        }

        public void Deserial(Bullet bullet)
        {
            base.Deserial(bullet.transform);
            bullet.startPos = new Vector3(StartPosition[0], StartPosition[1], StartPosition[2]);
        }
    }

    public SaveLoadObject Save()
    {
        return SaveLoadObject.Save(this, new SaveBullet(this));
    }

    public void Load(SaveLoadObject saveLoadObject)
    {
        saveLoadObject.Load<SaveBullet>()?.Deserial(this);
    }
}
