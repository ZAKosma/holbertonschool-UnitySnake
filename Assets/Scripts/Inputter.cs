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
        if (Input.GetKeyDown(up) || Input.GetKeyDown(upAlt))
        {
            if(GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.down)
                GameManager.Instance.Snake().SetSnakeSqDirection(SqDirection.up);
        }
        else if (Input.GetKeyDown(down) || Input.GetKeyDown(downAlt)) {
            if (GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.up)
                GameManager.Instance.Snake().SetSnakeSqDirection(SqDirection.down);
        }
        else if (Input.GetKeyDown(right) || Input.GetKeyDown(rightAlt))
        {
            if(GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.left)
                GameManager.Instance.Snake().SetSnakeSqDirection(SqDirection.right);
        }
        else if (Input.GetKeyDown(left) || Input.GetKeyDown(leftAlt))
        {
            if(GameManager.Instance.Snake().GetSnakeSqDirection() != SqDirection.right)
                GameManager.Instance.Snake().SetSnakeSqDirection(SqDirection.left);
        }
    }
}
