using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody = default(Rigidbody2D);
    [SerializeField] private float patrolSpeed = default(float);
    [SerializeField] private float patrolLength = default(float);
    [SerializeField] private float rotSpeed = default(float);

    private Transform[] patrolPoints;
    private int currentWP = 0;
    
    private bool mustPatrol = false;
    private Enemy enemy;

    public float moveSpeed { get => moveSpeed; set => moveSpeed = patrolSpeed; }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        mustPatrol = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
    }

    private Transform[] getPatrolPoints()
    {
        // Moving between two points
        patrolPoints = new Transform[2];
        patrolPoints[0].position = new Vector2(transform.position.x + patrolLength, transform.position.y);
        patrolPoints[1].position = new Vector2(transform.position.x - patrolLength, transform.position.y);
        return patrolPoints;
    }

    void Patrol()
    {
        
        if (Vector2.Distance(this.transform.position, patrolPoints[currentWP].transform.position) < 3)
            currentWP++;

        if (currentWP >= patrolPoints.Length)
        {
            currentWP = 0;
        }

        // this.transform.LookAt(waypoints[currentWP].transform.position);

        Quaternion lookatWP = Quaternion.LookRotation(patrolPoints[currentWP].transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookatWP, rotSpeed * Time.deltaTime);

        this.transform.Translate(0, 0, moveSpeed * Time.deltaTime);
        transform.Translate(new Vector2(patrolSpeed * Time.deltaTime, transform.position.y));
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        patrolSpeed *= -1;
        mustPatrol = true;
    }

}
