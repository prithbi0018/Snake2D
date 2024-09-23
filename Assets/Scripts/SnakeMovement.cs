using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private Vector2Int snakeMoveDirection;
    private Vector2Int snakePosition;
    private float snakeMoveTimer;
    private float snakeMoveTimerMax;

    private void Awake()
    {
        snakeMoveDirection = new Vector2Int(1, 0);
        snakePosition = new Vector2Int(25, 25);
        snakeMoveTimerMax = 1f;
        snakeMoveTimer = snakeMoveTimerMax;
    }

    private void Update()
    {
        HandleInput();
        HandleSnakeMovement();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (snakeMoveDirection.y != -1)
            {
                snakeMoveDirection.x = 0;
                snakeMoveDirection.y = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (snakeMoveDirection.y != 1)
            {
                snakeMoveDirection.x = 0;
                snakeMoveDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (snakeMoveDirection.x != 1)
            {
                snakeMoveDirection.x = -1;
                snakeMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (snakeMoveDirection.x != -1)
            {
                snakeMoveDirection.x = 1;
                snakeMoveDirection.y = 0;
            }
        }
    }

    private void HandleSnakeMovement()
    {
        snakeMoveTimer += Time.deltaTime;
        if (snakeMoveTimer >= snakeMoveTimerMax)
        {
            snakeMoveTimer -= snakeMoveTimerMax;
            snakePosition += snakeMoveDirection;

            transform.position = new Vector3(snakePosition.x, snakePosition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(snakeMoveDirection) - 90);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}