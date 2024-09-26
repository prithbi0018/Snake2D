using CodeMonkey;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelGrid 
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private SnakeMovement snake;

    public LevelGrid(int width, int height)
    {
        this.width = width;
        this.height = height;

        
        
    }

    public void Setup(SnakeMovement snake) 
    {
        this.snake = snake;
        SpawanFood();
    }

    public Transform gameHandlerTransform;
    private void SpawanFood()
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0 , width) , Random.Range(0 , height));
        }
        while (snake.GetGridPosition() == foodGridPosition); 
       

        GameObject foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);
      

        foodGameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Food";
        foodGameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        this.foodGameObject = foodGameObject;
        
    }
    public void SnakeMoved(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawanFood();
            CMDebug.TextPopupMouse("snake ate food");
        }
    }

}