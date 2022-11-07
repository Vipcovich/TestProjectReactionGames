using UnityEngine;

public abstract class SingletonMonoBehaviourDontDestroy<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}