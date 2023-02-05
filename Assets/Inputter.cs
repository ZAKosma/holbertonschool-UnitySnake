using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputter : MonoBehaviour
{
    public KeyCode upLeft = KeyCode.W;
    public KeyCode upRight = KeyCode.E;
    public KeyCode downLeft = KeyCode.X;
    public KeyCode downRight = KeyCode.C;
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.S;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upLeft))
        {
            if(GameManager.Instance.Snake().snakeDirection != Direction.downRight)
                GameManager.Instance.Snake().snakeDirection = Direction.upLeft;
        }
        if (Input.GetKeyDown(upRight))
        {
            if(GameManager.Instance.Snake().snakeDirection != Direction.downLeft)
                GameManager.Instance.Snake().snakeDirection = Direction.upRight;
        }
        if (Input.GetKeyDown(downLeft))
        {
            if(GameManager.Instance.Snake().snakeDirection != Direction.upRight)
                GameManager.Instance.Snake().snakeDirection = Direction.downLeft;
        }
        if (Input.GetKeyDown(downRight))
        {
            if(GameManager.Instance.Snake().snakeDirection != Direction.upLeft)
                GameManager.Instance.Snake().snakeDirection = Direction.downRight;
        }
        if (Input.GetKeyDown(right))
        {
            if(GameManager.Instance.Snake().snakeDirection != Direction.left)
                GameManager.Instance.Snake().snakeDirection = Direction.right;
        }if (Input.GetKeyDown(left))
        {
            if(GameManager.Instance.Snake().snakeDirection != Direction.right)
                GameManager.Instance.Snake().snakeDirection = Direction.left;
        }
    }
}
