using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SaveLoadManager : SingletonMonoBehaviourDontDestroy<SaveLoadManager>
{
    [Serializable]
    public class SaveRecord
    {
        public List<SaveLoadObject> saveLoadObjects = new List<SaveLoadObject>();
        public int levelIdx = -1;
        public long ticks;
        public DateTime dateTime => new DateTime(ticks);
    }

    private Coroutine loadCoroutine = null;
    private string titleSlotFormat = "dd MMMM yyyy HH:mm:ss";

    private static string SaveKey(int saveIdx)
    {
        return string.Format("Slot {0}", saveIdx);
    }

    public bool HasKey(int saveIdx)
    {
        string saveKey = SaveKey(saveIdx);
        return PlayerPrefs.HasKey(saveKey);
    }

    public string GetTitle(int saveIdx)
    {
        return LoadData(saveIdx)?.dateTime.ToString(titleSlotFormat) ?? "- - -";
    }

    private SaveRecord LoadData(int saveIdx)
    {
        string saveKey = SaveKey(saveIdx);

        if (!PlayerPrefs.HasKey(saveKey))
        {
            return null;
        }

        string saveStr = PlayerPrefs.GetString(saveKey);
        if (string.IsNullOrEmpty(saveStr))
        {
            return null;
        }

        SaveRecord saveRecord = JsonUtility.FromJson<SaveRecord>(saveStr);
        if (saveRecord == null)
        {
            return null;
        }

        return saveRecord;
    }

    public void Save(int saveIdx)
    {
        List<ISaveLoad> saveObjects = SceneManager.GetActiveScene()
            .GetRootGameObjects()
            .SelectMany(obj => obj.GetComponentsInChildren<ISaveLoad>())
            .ToList();

        SaveRecord saveRecord = new SaveRecord();
        foreach (ISaveLoad obj in saveObjects)
        {
            saveRecord.saveLoadObjects.Add(obj.Save());
        }
        saveRecord.ticks = DateTime.Now.Ticks;
        saveRecord.levelIdx = LevelLoader.Instance.LevelIdx;

        string saveStr = JsonUtility.ToJson(saveRecord);
        string saveKey = SaveKey(saveIdx);

        PlayerPrefs.SetString(saveKey, saveStr);
    }

    public void Load(int saveIdx)
    {
        SaveRecord saveRecord = LoadData(saveIdx);
        if (saveRecord == null)
        {
            Debug.LogErrorFormat("Can't load in slot {0}", saveIdx);
            return;
        }

        if (loadCoroutine != null)
        {
            StopCoroutine(loadCoroutine);
            loadCoroutine = null;
        }
        loadCoroutine = StartCoroutine(LoadInternal(saveRecord));
    }

    private IEnumerator LoadInternal(SaveRecord saveRecord)
    {
        LevelLoader.Instance.LoadLevel(saveRecord.levelIdx);

        yield return new WaitForSeconds(0f);

        Dictionary<string, ISaveLoad> sceneObjects = SceneManager.GetActiveScene()
            .GetRootGameObjects()
            .SelectMany(obj => obj.GetComponentsInChildren<ISaveLoad>())
            .ToDictionary(obj => string.Format("{0}/{1}", obj.gameObject.GetPath(), obj.GetType().Name));

        Dictionary<string, SaveLoadObject> loadObjects = saveRecord.saveLoadObjects.ToDictionary(obj => obj.Path);

        string[] loadKeys = loadObjects.Keys.ToArray();
        for (int i = 0; i < loadKeys.Length; i++)
        {
            string key = loadKeys[i];
            if (sceneObjects.TryGetValue(key, out ISaveLoad sceneObj))
            {
                sceneObj.Load(loadObjects[key]);
                sceneObjects.Remove(key);
            }
        }

        sceneObjects.Values.ToList().ForEach(obj => Destroy(obj.gameObject));

        string bulletKepeerType = typeof(Bullet.SaveBullet).Name;
        List<SaveLoadObject> bulletDataList = saveRecord.saveLoadObjects.Where(obj => obj.KeeperType == bulletKepeerType).ToList();

        if (bulletDataList.Count > 0)
        {
            if (ObjectFactory.Instance)
            {
                foreach (SaveLoadObject data in bulletDataList)
                {
                    Bullet bullet = ObjectFactory.Instance.Create<Bullet>();
                    bullet.Load(data);
                }
            }
            else
            {
                Debug.LogError("Can't restore bullets: ObjectFactory.Instance == null!");
            }
        }
    }
}
