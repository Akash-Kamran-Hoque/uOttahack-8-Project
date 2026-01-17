using UnityEngine;

public class Player : MonoBehaviour
{

    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput*movementSpeed, rb.linearVelocity.y);

        //Jumping (Verical movement)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
    }
}
