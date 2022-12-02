using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputter : MonoBehaviour
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.A;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.up;
        }
        if (Input.GetKeyDown(down))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.down;
        }if (Input.GetKeyDown(right))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.right;
        }if (Input.GetKeyDown(left))
        {
            GameManager.Instance.Snake().snakeDirection = Direction.left;
        }
    }
}
