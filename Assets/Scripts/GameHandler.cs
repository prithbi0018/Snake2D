using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private static GameHandler instance;
    private static int score;
    [SerializeField] private SnakeMovement snake;
    private LevelGrid levelGrid;
    private ReloadButtonScript reloadButtonScript;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20, 20);

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);

        
        reloadButtonScript = GameObject.FindObjectOfType<ReloadButtonScript>();
    }

    public static int Score
    {
        get { return score; }
    }

    public static void IncrementScore()
    {
        score += 100;
    }

    public static void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void GameOver()
    {
      
        ReloadButtonScript reloadButtonScript = GameObject.FindObjectOfType<ReloadButtonScript>();
        reloadButtonScript.SetGameOver(true);
    }
}