using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistence : MonoBehaviour
{
    int currentSceneNumber;
    void Awake() {
        int num = FindObjectsOfType<ScenePersistence>().Length;
        if(num > 1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start() {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void DestroyScenePersistence()
    {
        Destroy(gameObject);
    }
}
