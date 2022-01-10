using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchEgg : MonoBehaviour
{
    // This object will be searching for the egg to bring back to their base.

    [SerializeField] private Rigidbody2D rigidbody = default(Rigidbody2D);
    private GameObject objectToSeek = default(GameObject);
    [SerializeField] private float moveSpeed = default(float);
    [SerializeField] private float xOffSet = default(float);
    [SerializeField] private float yOffSet = default(float);
    [SerializeField] private Egg[] eggs = default(Egg[]);
    [SerializeField] private float aggroDistance = default(float);

    public bool hasEgg = false;
    private GameManager gameManager = default(GameManager);
    private Enemy enemy;

    private Transform objectLocation = default(Transform);
    private Vector2 currentPosition = default(Vector2);
    private bool facingLeft = false;
    public bool isEggHeld = false;
    private Transform nearestExit = default(Transform);
    SearchEgg searchEggS;

    private void Awake()
    {
        if (rigidbody == null)
            throw new System.Exception("No rigidbody attached");

        objectToSeek = GameObject.FindWithTag("Egg");

        if (objectToSeek == null)
        {
            objectToSeek = GameObject.FindWithTag("Player");
        }
        else
        {
            objectLocation = objectToSeek.transform;
        }
        gameManager = FindObjectOfType<GameManager>();
         searchEggS = GetComponent<SearchEgg>();
        currentPosition = transform.localScale;
        
    }

    private void FixedUpdate()
    {
        // If there is an egg to search for
        bool doChaseEgg = !gameManager.enemyHasEgg;
        Debug.Log("I " + isEggHeld + " have the egg");
        if(doChaseEgg)
        {
            //Vector3 dir = transform.position - objectLocation.position;
            //dir = dir.normalized;
            Debug.Log("Chasing egg");
            SearchForEgg();
        }
        else if(!doChaseEgg && !isEggHeld)
        {
            // protect egg by attacking player
            //AttackPlayer();
            AttackPlayer();
        }
        else if(isEggHeld)
        {
            // Run to nearest exit
            RunAway();
        }
        
    }



    // Move to another script
    private void AttackPlayer()
    {
        // play transform animation
        enemy = GetComponent<Enemy>();
        Debug.Log("changing forms!");
        enemy.ChangeState(SpawnManager.State.ATTACK);
    }

    public void GetClosestExit(Transform[] exits)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in exits)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        nearestExit = tMin;
    }


    private void RunAway()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (!collider)
            return;

        transform.Translate(nearestExit.position.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit" && isEggHeld)
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GameOver();
        }
    }

    private void SearchForEgg()
    {
        Vector3 enemyDirectionLocal = objectToSeek.transform.InverseTransformPoint(transform.position);
        //Debug.Log(enemyDirectionLocal);
        // Flip enemy to face left when moving left
        if (enemyDirectionLocal.x > 0)
        {
            //Debug.Log("LEFT");
            //anim.SetBool("isWalking", true);
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector2(-currentPosition.x, currentPosition.y);
            facingLeft = true;

        }
        else if (enemyDirectionLocal.x < 0)
        {
            //Debug.Log("RIGHT");
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (facingLeft)
            {
                //Debug.Log("We're facing left");
                //Flip to the right
                transform.localScale = new Vector2(-currentPosition.x, currentPosition.y);
                facingLeft = false;
            }

            //anim.SetBool("isWalking", true);
        }
        else
        {
            //anim.SetBool("isWalking", false);

        }
        //transform.right = objectLocation.position - transform.position;
    }

}
