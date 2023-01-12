using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Primitive Value")]
    [SerializeField] float runSpeed = 5f;  
    [SerializeField] float climbSpeed = 1f;  
    [SerializeField] float JumpSpeed = 10f;
    [SerializeField] float BounceHeight = 10f;
    [SerializeField] float defaultGravity = 8f;
    [SerializeField] bool isAlive = true;

    [Header("GameObject")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject instantiateParent; 



    Animator playerAnimator;
    BoxCollider2D BodyCollider;
    PolygonCollider2D feetCollider;
    Vector2 moveInput;
    Rigidbody2D rb;
    GameSession gameSession;
    

    void Awake() {
        gameSession = FindObjectOfType<GameSession>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        BodyCollider = GetComponent<BoxCollider2D>();
        feetCollider = GetComponent<PolygonCollider2D>();
        rb.gravityScale = defaultGravity;
    }

    void Update()
    {
        if(!isAlive) return;
        
        Run();
        Climbing();
        Flip();
        Die();
        
    }

    public void TouchedWithCoin(int value)
    {
        gameSession.IncreaseScore(value);
    }

    void Run()
    {
        if(!isAlive) return;
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, 0f);
        rb.velocity = new Vector2( playerVelocity.x , rb.velocity.y);
        if(Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        
    }

    void Climbing()
    {
        if(!isAlive) return;
        bool isTouchingClimb = feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if(!isTouchingClimb)
        {
            rb.gravityScale = defaultGravity;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        bool isTouchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        rb.gravityScale = 0f;
        Vector2 playerVelocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        rb.velocity = playerVelocity;

        bool isPlayerHasVerticalInput = Mathf.Abs(moveInput.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", isPlayerHasVerticalInput);

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) return;
        bool isTouching = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        
        if(!isTouching) return;

        if(value.isPressed)
        {
            rb.velocity += new Vector2(0f, JumpSpeed);
        }
    }


    void Flip()
    {
        if(!isAlive) return;
        bool isPlayerHasHorizontalSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;

        if(isPlayerHasHorizontalSpeed)
        {
           transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1f);
        }
    }

    void Die()
    {
        if(BodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazard")) && isAlive)
        {
            isAlive = false;
            playerAnimator.SetTrigger("Death");
            rb.velocity += new Vector2( -Mathf.Sign(rb.velocity.x) * BounceHeight, BounceHeight);        
            FindObjectOfType<GameSession>().ProcesPlayerDeath();
        }

        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")) && isAlive)
        {
            isAlive = false;
            playerAnimator.SetTrigger("Death");
            rb.velocity += new Vector2( -Mathf.Sign(rb.velocity.x) * BounceHeight, BounceHeight);        
            FindObjectOfType<GameSession>().ProcesPlayerDeath();
        }
    }

    void OnFire(InputValue input)
    {
        if(!isAlive) return;

        if(input.isPressed)
        {
           GameObject instanceBullet  = Instantiate(bullet, gun.transform.position, Quaternion.identity);
           instanceBullet.transform.parent = instantiateParent.transform;
        }
    }
}
