using UnityEngine;
using System.Collections; // Needed for IEnumerator and Coroutines


public class Player : MonoBehaviour
{

    public int health = 100;
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    public int extraJumpsValue;
    private int extraJumps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Always intializes to 1 double jump when starting game
        extraJumpsValue = 1;
        extraJumps = extraJumpsValue;
    }

    // Update is called once per frame
    void Update()
    {

        //Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput*movementSpeed, rb.linearVelocity.y);

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        //Jumping (Verical movement)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);   
            }
            //When in air (double jump)
            else if(extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
            }
        }

        //Updating the animation state
        SetAnimation(moveInput);
    }

    //Assuring that the player is on the ground
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    //Animations decider
    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            //Idle 
            if(moveInput == 0)
            {
                animator.Play("Player_idle");
            }
            //Running
            else
            {
                animator.Play("Player_Run");
            }

        }
        //Player in air
        else
        {
            //Moving up
            if(rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump");
            }
            //Moving down
            else
            {
                animator.Play("Player_Fall");
            }
        }

        //Account for horizontal direction of movement
        spriteRenderer.flipX = rb.linearVelocity.x < 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 25;
            //Knockback effect
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());

            if(health <= 0)
            {
                Die();
            }

        }
    }

    //Coroutine (thread) that will run when damaged
    //Must use StartCoroutine() function to invoke
    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        //Reloads the level from scratch
        UnityEngine.SceneManagement.SceneManager.LoadScene("FirstLevel");
    }
}
