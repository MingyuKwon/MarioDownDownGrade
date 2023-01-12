using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{    
    [SerializeField] AudioClip coinClip;
    [SerializeField] int score = 100;
    bool wasCollected = false;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            AudioSource.PlayClipAtPoint(coinClip, Camera.main.transform.position, 0.5f);
            PlayerMove pm = other.gameObject.GetComponent<PlayerMove>();
            pm.TouchedWithCoin(score);
            Destroy(gameObject);
        }
    }
}
