using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = default(float);
    [SerializeField] private Rigidbody2D _rigidbody = default(Rigidbody2D);
    [SerializeField] private bool facingRight = true;
    [SerializeField] private float jumpForce = default(float);
    [SerializeField] private Transform groundCheck = default(Transform);
    [SerializeField] private LayerMask groundObjects = default(LayerMask);
    [SerializeField] private float checkRadius = default(float);
    [SerializeField] private float health = default(float);

    private Animator animator;

    private bool isJumping = false;
    private bool isGround;

    private float moveDirection;

    private void Awake()
    {
        if(_rigidbody == null)
        {
            throw new System.Exception("No rigid body attached, check the inspector");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        ProcessInput();
        Animate();
    }

    void FixedUpdate()
    {
        // Check if on the ground
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        Debug.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x + checkRadius, groundCheck.position.y + checkRadius), Color.red, 1.0f, false);

        // Move
        Move();       
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(moveDirection * speed, _rigidbody.velocity.y);
        
        if(isJumping)
        {
            _rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            flipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            flipCharacter();
        }
    }

    private void ProcessInput()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Scale of -1 -> 1

        if(moveDirection != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(Input.GetButtonDown("Jump") && isGround)
        {
            isJumping = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy") 
        {
            // Take damage
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            TakeDamage(enemy.damage);

            
        }
    }

    private void TakeDamage(float damage)
    {
        // Hurt animation
        animator.SetTrigger("hurt");
        health -= damage;

        if(health <= 0)
        {
            // Death animation
            FindObjectOfType<GameManager>().GameOver();
        }
            
    }

    private void flipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
