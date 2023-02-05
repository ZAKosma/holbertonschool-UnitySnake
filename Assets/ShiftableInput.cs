using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftableInput : MonoBehaviour
{
    public KeyCode rotateLeft = KeyCode.A;
    public KeyCode rotateRight = KeyCode.D;
    public KeyCode altLeft = KeyCode.LeftArrow;
    public KeyCode altRight = KeyCode.RightArrow;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(rotateLeft) || Input.GetKeyDown(altLeft))
        {
            //Rotate left
            GameManager.Instance.Snake().RotateLeft();

        }
        if (Input.GetKeyDown(rotateRight) || Input.GetKeyDown(altRight))
        {
            //Rotate right
            GameManager.Instance.Snake().RotateRight();
        }
        
    }
}
