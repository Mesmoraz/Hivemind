using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody = default(Rigidbody2D);
    private Vector2 attackLocation = default(Vector2);
    private GameObject player = default(GameObject);
    [SerializeField] private float moveSpeed = default(float);
    private bool facingRight = true;
    Vector2 startingPos = default;
    private SpriteRenderer enemySprite = default(SpriteRenderer);

    // Start is called before the first frame update
    void Start()
    {
        // Play attack animation
        // Get player component
        player = FindObjectOfType<PlayerMovement>().gameObject;
        startingPos = transform.position;
        enemySprite = GetComponent<SpriteRenderer>();
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Search for the player
        attackLocation = player.transform.position - transform.position;
        // Go towards player
        AttackPlayer();
        // Do damage if hit
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //DealDamage();
        }
    }

    //void DealDamage()
    //{

    //}

    private void FixedUpdate()
    {
        
    }

    private void AttackPlayer()
    {

        //Debug.Log(enemyDirectionLocal);
        // Flip enemy to face left when moving left
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * moveSpeed);
        Debug.Log(attackLocation.x);
        Debug.Log(facingRight);

        if (attackLocation.x > 0) // X is LARGER than 0 therefore moving right
        {
            if (!facingRight)
                Flip();
            //Debug.Log("LEFT");
            //anim.SetBool("isWalking", true);
            Debug.Log("Moving right");
            //transform.localScale = new Vector2(-transform.position.x, transform.position.y);
        }
        else if (attackLocation.x < 0) // moving left
        {
            if (facingRight)
            {
                Flip();
            }
                
            //Debug.Log("We're facing left");
            //Flip to the right
            //transform.localScale = new Vector2(-transform.position.x, transform.position.y);

            //anim.SetBool("isWalking", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Debug.Log("Flipping!");
        transform.Rotate(0f, 180f, 0f);
    }
}
