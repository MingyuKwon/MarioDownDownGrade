using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon : MonoBehaviour
{
    void Awake() {
        int num = FindObjectsOfType<SingleTon>().Length;
        if(num > 1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void DestroySingleTon()
    {
        Destroy(gameObject);
    }
    
}
