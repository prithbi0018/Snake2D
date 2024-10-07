using CodeMonkey;
using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    private float gridMoveTimerMax;
    internal static object i;
    private LevelGrid levelGrid;
    private int snakeBodySize;
    [SerializeField]
    private List<Vector2Int> snakeMovePositionList;
    [SerializeField]
    private List<Transform> SnakeBodyTransformList;
    public GameObject snakeBoddy;
    private List<SnakeBodyPart> snakeBodyPartList = new List<SnakeBodyPart>();
    private enum State { Alive, Dead };
    private State state;
    public float snakeSpeed = 0.5f;
    public void Setup(LevelGrid levelGrid)
    {
        this.levelGrid = levelGrid;
    }

    private void Awake()
    {
        gridMoveDirection = new Vector2Int(1, 0);
        gridPosition = new Vector2Int(25, 25);
        gridMoveTimerMax = 1f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);

        snakeBodySize = 0;
        snakeMovePositionList = new List<Vector2Int>();
        SnakeBodyTransformList = new List<Transform>();

        state = State.Alive;
        gridMoveTimerMax = snakeSpeed;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                break;
            case State.Dead:
                
                break;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != 1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != 1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = 1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private void HandleGridMovement()
    {
        if (state == State.Dead) return;

        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            snakeMovePositionList.Insert(0, gridPosition);

            gridPosition += gridMoveDirection;

           
            gridPosition = levelGrid.ValidateGridPosition(gridPosition);

            if (levelGrid.TrySnakeEatFood(gridPosition))
            {
                snakeBodySize++;
                CreateSnakeBody();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);
            levelGrid.SnakeMoved(gridPosition);

            for (int i = 0; i < SnakeBodyTransformList.Count; i++)
            {
                Vector3 snakeBodyPosition = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
                SnakeBodyTransformList[i].position = snakeBodyPosition;
            }

            // Update snakeBodyPartList
            snakeBodyPartList.Clear();
            for (int i = 1; i < snakeMovePositionList.Count; i++)
            {
                snakeBodyPartList.Add(new SnakeBodyPart(snakeMovePositionList[i]));
            }

            foreach (SnakeBodyPart snakeBody in snakeBodyPartList)
            {
                Vector2Int snakeBodyPartGridPosition = snakeBody.GetGridPosition();
                if (gridPosition == snakeBodyPartGridPosition)
                {
                    Debug.Log("Collision detected!");
                    state = State.Dead;
                    GameHandler.GameOver(); 
                }
            }
        }
    }

    private void CreateSnakeBody()
    {
        Vector2Int snakeBodyPosition = snakeMovePositionList[snakeMovePositionList.Count - 1];
        GameObject snakeBodyGameObject = Instantiate(snakeBoddy, new Vector3(snakeBodyPosition.x, snakeBodyPosition.y), Quaternion.identity);
        SnakeBodyTransformList.Add(snakeBodyGameObject.transform);
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPosition));
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridPosition;
    }

    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }
    private class SnakeBodyPart
    {
        private Vector2Int gridPosition;

        public SnakeBodyPart(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }
    }
}