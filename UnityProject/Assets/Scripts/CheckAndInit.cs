using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAndInit : MonoBehaviour
{
    private static bool inited;

    private IEnumerator Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!inited && !GameManager.Instance)
        {
            int levelIdx = SceneManager.GetActiveScene().buildIndex;
            if (levelIdx > 0)
            {
                // load menu
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                while (!LevelLoader.Instance)
                {
                    yield return null;
                }

                LevelLoader.Instance.LoadLevel(levelIdx);
            }
            else
            {
                Debug.LogError("Can't find this level in level list!");
                yield break;
            }
        }

        Destroy(gameObject);
    }
}
