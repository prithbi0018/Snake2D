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
        SpawnFood();
    }

    public Transform gameHandlerTransform;
    private void SpawnFood()
    {
        do
        {
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        }
        while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);

        GameObject foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite;
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);

        foodGameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Food";
        foodGameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        this.foodGameObject = foodGameObject;
    }

    public bool TrySnakeEatFood(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            GameHandler.IncrementScore(); 
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SnakeMoved(Vector2Int snakeGridPosition)
    {
        // You can add code here to handle the snake's movement
    }

    public Vector2Int ValidateGridPosition(Vector2Int gridPosition)
    {
        if (gridPosition.x < 0)
        {
            gridPosition.x = width - 1;
        }
        else if (gridPosition.x >= width)
        {
            gridPosition.x = 0;
        }

        if (gridPosition.y < 0)
        {
            gridPosition.y = height - 1;
        }
        else if (gridPosition.y >= height)
        {
            gridPosition.y = 0;
        }
        return gridPosition;
    }

    public int Width { get { return width; } }
    public int Height { get { return height; } }
}