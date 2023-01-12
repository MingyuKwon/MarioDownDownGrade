using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulley : MonoBehaviour
{    
    [SerializeField] float bulletSpeed = 8f;

    float delayTime = 0.8f;
    CircleCollider2D bulletCollider;
    PlayerMove playerMove;
    Rigidbody2D bulletRigidBody;

    private void Awake() {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Start() 
    {
        bulletCollider = GetComponent<CircleCollider2D>();
        bulletRigidBody = GetComponent<Rigidbody2D>();

        if(playerMove.transform.localScale.x > 0)
        {
            bulletRigidBody.velocity = new Vector2(bulletSpeed, 0f);
        }else
        {
            bulletRigidBody.velocity = new Vector2(-bulletSpeed, 0f);
        }

        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(bulletCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }



}
