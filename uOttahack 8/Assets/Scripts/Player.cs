using UnityEngine;

public class Player : MonoBehaviour
{

    public float movementSpeed = 5f;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput*movementSpeed, rb.linearVelocity.y);
    }
}
