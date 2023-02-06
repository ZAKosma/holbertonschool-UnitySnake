using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGridModel
{
    public static SquareGridModel Instance { get; private set; }
    
    private SqGrid sqGrid;
    
    //Public default variabLes?
    public GameObject cellPrefab;
    public GameObject gridAnchorPoint;
    public float cellSize = 120;
    public float borderSize = 20;
    public float cellSpacing = 20;
    
    //Public settings variables
    public int xSize = 12;
    public int ySize = 12;

    public int snakeStartX = -1;
    public int snakeStartY = -1;

    public int startingFruit = 2;

    //Colors
    public Color backGroundColor;
    public Color snakeColor;
    public Color fruitColor;
    public Color wallColor;
    public Color emptyColor;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (sqGrid == null)
        {
            CreateNewGrid();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Create grid in Edit mode")]
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
            sqGrid = new SqGrid(xSize, ySize);

        }
        else
        {
            sqGrid = new SqGrid(xSize, ySize, snakeStartX, snakeStartY);

        }
        
        //Create and Render the models
        Vector3 anchorStart = gridAnchorPoint.transform.position;

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                Occupant o = sqGrid.GetCellValue(x, y);
                GameObject go = Instantiate(cellPrefab, gridAnchorPoint.transform);
                CellModel cm = go.GetComponent<CellModel>();

                cm.Init(sqGrid.GetCell(x,y));
                cm.SetSize(cellSize - borderSize, cellSize);
                sqGrid.GetCell(x, y).SetModel(cm);

                var newPos = anchorStart;
                newPos.x += (x * (cellSize + cellSpacing)) + cellSpacing;
                if (y % 2 == 0)
                {
                    newPos.x += (cellSize / 2);
                } 
                newPos.y += y * ((cellSize + cellSpacing)* .75f);
                
                
                go.transform.position = newPos;
                
                cm.UpdateCellColor(GetOccupantColor(o));
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public Color GetOccupantColor(Occupant occupant)
    {
        switch (occupant)
        {
            case Occupant.empty:
                return emptyColor;
            case Occupant.fruit:
                return fruitColor;
            case Occupant.snake:
                return snakeColor;
            case Occupant.wall:
                return wallColor;
            default:
                Debug.LogError("Somehow we got passed an occupant that doesn't exist");
                return Color.magenta;
        }
    }

    public CellModel SetCell(int x, int y, Occupant newOccupant)
    {
        var cell = sqGrid.GetCell(x, y);
        cell.SetCellValue(newOccupant);
        cell.GetModel().UpdateCellColor();

        return cell.GetModel();
    }

    public CellModel SetCell(Coord c, Occupant newOccupant)
    {
        return SetCell(c.x, c.y, newOccupant);
    }

    public Cell GetCell(int x, int y)
    {
        //Debug.Log(grid.GetCell(x,y));
        return sqGrid.GetCell(x, y);
    }

    public Cell GetCell(Coord c)
    {
        return GetCell(c.x, c.y);
    }
}
