
using UnityEngine;


public class GameHandler : MonoBehaviour
{
    [SerializeField] private SnakeMovement snake;    
    private LevelGrid levelGrid;
    private void Start() 
    {
        Debug.Log("GameHandler.Start");

        levelGrid = new LevelGrid(20 , 20);    

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);
    }

}
