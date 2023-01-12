using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelExit : MonoBehaviour
{
    [SerializeField] float delayTime = 0.5f;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
           StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(delayTime);

        int sceneNumber = SceneManager.GetActiveScene().buildIndex;
        int MaxSceneNumber = SceneManager.sceneCountInBuildSettings;

        if(sceneNumber == MaxSceneNumber-1)
        {
            sceneNumber = -1;
        }
        sceneNumber++;

        FindObjectOfType<ScenePersistence>().DestroyScenePersistence();
        SceneManager.LoadScene(sceneNumber);
    }

}
