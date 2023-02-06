using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridModel : GridModel
{
    /*[ContextMenu("Create grid in Edit mode")]
    public void CreateNewGrid()
    {
        if (gridAnchorPoint.transform.childCount > 0)
        {
            var childList = new Transform[gridAnchorPoint.transform.childCount];
            var i = 0;
            for (; i < gridAnchorPoint.transform.childCount; i++)
            {
                childList[i] = gridAnchorPoint.transform.GetChild(i);
            }

            for (i = i - 1; i > -1; i--)
            {
                GameObject.DestroyImmediate(childList[i].gameObject);
            }
        }

        if (snakeStartX < 0 || snakeStartY < 0)
        {
            _hexGrid = new HexGrid(xSize, ySize);

        }
        else
        {
            _hexGrid = new HexGrid(xSize, ySize, snakeStartX, snakeStartY);

        }

        //Create and Render the models
        Vector3 anchorStart = gridAnchorPoint.transform.position;

       int xMod = 0;
        for (int y = 0; y < ySize; y++)
        {
            if (y % 2 == 1)
            {
                xMod = 1;
            }
            else
            {
                xMod = 0;
            }
            for (int x = 0; x < xSize * 2; x += 2)
            {
                
                //Create hexcell gameobject
                Occupant o = _hexGrid.GetCellValue(x + xMod, y);
                GameObject go = Instantiate(cellPrefab, gridAnchorPoint.transform);
                CellModel cm = go.GetComponent<CellModel>();

                cm.Init(_hexGrid.GetCell(x +xMod, y));
                cm.SetSize(cellSize - borderSize, cellSize);
                _hexGrid.GetCell(x + xMod, y).SetModel(cm);

                //Set hexcell position
                var newPos = anchorStart;
                newPos.x += ((x/2) * (cellSize + cellSpacing)) + cellSpacing;
                if (y % 2 == 1)
                {
                    newPos.x += (cellSize / 2);
                }

                newPos.y += y * ((cellSize + cellSpacing) * .75f);


                go.transform.position = newPos;

                cm.UpdateCellColor(GetOccupantColor(o));
            }
        }
    }*/
    protected override Vector3 GetTargetPosition(int x, int y)
    {
        var newPos = gridAnchorPoint.transform.position;
        
        newPos.x += ((x/2) * (cellSize + cellSpacing)) + cellSpacing;
        if (staggerRows && y % 2 == 1)
        {
            newPos.x += (cellSize / 2);
        }

        newPos.y += y * ((cellSize + cellSpacing) * .75f);

        return newPos;
    }
}
