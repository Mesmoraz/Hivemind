using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameIsPaused = false;
    public bool enemyHasEgg = false;
    private Enemy[] enemies = null;
    [SerializeField] public Transform[] exits = default(Transform[]);
    [SerializeField] private GameObject pauseScreen = default(GameObject);

    bool gameHasEnded = false;
    public float restartDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        enemyHasEgg = false;
        //enemies = GetComponent<Enemy[]>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && !gameIsPaused)
        {
            Time.timeScale = 0;
            gameIsPaused = true;
            pauseScreen.SetActive(true);

        }
        else if(Input.GetKeyUp(KeyCode.Escape) && gameIsPaused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            gameIsPaused = false;
            
        }
    }

    public void GameOver()
    {
        if(!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
