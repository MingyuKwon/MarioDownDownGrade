using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    GameSession gameSession;
    Image[] heartImages;
    TextMeshProUGUI scoreText;


    [SerializeField] int life;
    [SerializeField] int score;

    void Awake() {
        int numUIs = FindObjectsOfType<UI>().Length;
        if(numUIs > 1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(gameObject);
        }

        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponentInChildren<TextMeshProUGUI>();
        heartImages = GetComponentsInChildren<Image>();
    }
    void Start()
    {
        life = gameSession.GetLife();
        score = gameSession.GetScore();
        
        ShowLife();
        ShowScore();
    }

    public void ShowLife()
    {
        life = gameSession.GetLife();
        if(life < heartImages.Length)
        {
            heartImages[life].enabled = false;
        }
            
    }

    public void ShowScore()
    {
        score = gameSession.GetScore();
        scoreText.text = "Score : " + score;
    }

    public void DestroyUI()
    {
        Destroy(gameObject);
    }
}
