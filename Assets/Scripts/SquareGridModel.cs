using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGridModel : GridModel
{
    protected override Vector3 GetTargetPosition(int x, int y)
    {
        var newPos = gridAnchorPoint.transform.position;
        newPos.x += (x * (cellSize + cellSpacing)) + cellSpacing;
        if (staggerRows && y % 2 == 0)
        {
            newPos.x += (cellSize / 2);
        } 
        newPos.y += y * ((cellSize + cellSpacing));


        return newPos;
    }
}
