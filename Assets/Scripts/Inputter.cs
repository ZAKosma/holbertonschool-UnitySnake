using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputter : MonoBehaviour
{
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.A;
    
    public KeyCode upAlt = KeyCode.UpArrow;
    public KeyCode downAlt = KeyCode.DownArrow;
    public KeyCode rightAlt = KeyCode.RightArrow;
    public KeyCode leftAlt = KeyCode.LeftArrow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up))
        {
            if(GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.down)
                GameManager.Instance.Snake().SetSnakeDirection(SqDirection.up);
        }
        else if (Input.GetKeyDown(right)) {
            if (GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.up)
                GameManager.Instance.Snake().SetSnakeDirection(SqDirection.down);
        }
        else if (Input.GetKeyDown(right))
        {
            if(GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.left)
                GameManager.Instance.Snake().SetSnakeDirection(SqDirection.right);
        }
        else if (Input.GetKeyDown(left))
        {
            if(GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.right)
                GameManager.Instance.Snake().SetSnakeDirection(SqDirection.left);
        }
    }
}
