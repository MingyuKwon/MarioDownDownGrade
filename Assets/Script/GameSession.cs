using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playersLife = 3;
    [SerializeField] int score = 0;
    [SerializeField] float delayTime = 0.5f;

    UI ui;

    void Awake() 
    {
        // 싱글톤
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(gameObject);
        }

        ui = FindObjectOfType<UI>();
    
    }

    public void IncreaseScore(int value)
    {
        score += value;
        ui.ShowScore();
    }
    
    public int GetLife()
    {
        return playersLife;
    }

    public int GetScore()
    {
        return score;
    }

    public void ProcesPlayerDeath()
    {
        if(playersLife > 1 )
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }   
    }

    IEnumerator ResetGameSession()
    {
        playersLife--;
        ui.ShowLife();
        yield return new WaitForSecondsRealtime(delayTime);
        FindObjectOfType<ScenePersistence>().DestroyScenePersistence();
        SceneManager.LoadScene(0);
        ui.DestroyUI();
        Destroy(gameObject); // 자체적으로 Destroy 안해주면 
                            //새로 load 할때도 삭제 안되고 계속 유지됨
    }

    IEnumerator TakeLife()
    {
        playersLife--;
        ui.ShowLife();
        yield return new WaitForSecondsRealtime(delayTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
