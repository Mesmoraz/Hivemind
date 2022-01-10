using UnityEngine;


public class Enemy : MonoBehaviour, SpawnManager.IEnemy
{
    private SpawnManager.IEnemyAttack _enemy;
    [SerializeField] private SpawnManager.State _state;
    [SerializeField] private float moveSpeed = default(float);
    [SerializeField] private float maxHealth = default(float);
    private float currentHealth = 0f;
    private int eggSearchCount = 0;

    public float damage = 1f;
    private int attackCount = 0;
    private GameManager gameManager;
    public SpawnManager.State CurrentState {get{return _state;}}

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("damage Taken!");
    }

    void Die()
    {
        // Play death animation
        // Destroy
        Destroy(gameObject);
    }

    public void ChangeState(SpawnManager.State newState)
    {
        Debug.Log(this.name + " is changing their mind");
        Debug.Log("from " + _state + " to " + newState);
        switch(_state)
        {
            case SpawnManager.State.SEARCH_EGG:
                this.GetComponent<SearchEgg>().enabled = false;
                break;
            case SpawnManager.State.ATTACK:
                this.GetComponent<Attack>().enabled = false;
                Debug.Log("Attacking");
                break;
            case SpawnManager.State.RETURN:
                Debug.Log("Returning");
                break;
            case SpawnManager.State.PATROL:
                this.GetComponent<Patrol>().enabled = false;
                Debug.Log("Looking for you");
                break;
            default:
                Debug.Log("I have no state! ");
                this.GetComponent<SearchEgg>().enabled = false;
                break;
        }
        _state = newState;
        Think();
    }

    public void Think()
    {
        switch (_state)
        {
            case SpawnManager.State.SEARCH_EGG:
                this.GetComponent<SearchEgg>().enabled = true;
                break;
            case SpawnManager.State.ATTACK:
                this.GetComponent<Attack>().enabled = true;
                Debug.Log("Attacking");
                break;
            case SpawnManager.State.RETURN:
                Debug.Log("Returning");
                break;
            case SpawnManager.State.PATROL:
                Debug.Log("Looking for you");
                break;
            default:
                Debug.Log("I have no state! ");
                this.GetComponent<SearchEgg>().enabled = false;
                break;
        }
    }

    private void Awake()
    {
        Think();
        currentHealth = maxHealth;
        if(eggSearchCount < 1)
        {
            _state = SpawnManager.State.SEARCH_EGG;
        }
       
    }

   

}