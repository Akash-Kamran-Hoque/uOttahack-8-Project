using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public Transform[] points;

    private int i;


    void Start()
    {
        //Move the platform to the starting point
        transform.position = points[0].position;    
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            if(i == points.Length)
            {
                i=0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Attaching the player to move with platform
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Detatching the player to move with platform
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

}
