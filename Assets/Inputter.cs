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
            GameManager.Instance.Snake().snakeDirection = Direction.upLeft;
        }
        if (Input.GetKeyDown(upRight))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.upRight;
        }
        if (Input.GetKeyDown(downLeft))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.downLeft;
        }
        if (Input.GetKeyDown(downRight))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.downRight;
        }
        if (Input.GetKeyDown(right))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.right;
        }if (Input.GetKeyDown(left))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.left;
        }
    }
}
