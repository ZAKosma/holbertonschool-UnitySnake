using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GridModel: MonoBehaviour
{
    public static GridModel Instance { get; private set; }
    
    private BaseGrid grid;

    public GameType gameType;
    
    //Public default variabLes?
    public GameObject cellPrefab;
    public GameObject gridAnchorPoint;
    public float cellSize = 120;
    public float borderSize = 20;
    public float cellSpacing = 20;
    
    //Public settings variables
    [SerializeField]
    protected int xSize = 12;
    public int ySize = 12;

    [HideInInspector]
    public int xSizeAdjusted;

    public int snakeStartX = -1;
    public int snakeStartY = -1;

    public int startingFruit = 2;

    //Colors
    public Color backGroundColor;
    public Color snakeColor;
    public Color snakeHeadColor;
    public Color fruitColor;
    public Color wallColor;
    public Color emptyColor;
    public Color highlightColor;

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

        xSizeAdjusted = xSize * 2;
    }

    private void Start()
    {
        if (grid == null)
        {
            CreateNewGrid();
        }
    }

    [ContextMenu("Create grid in Edit mode")]
    public void CreateNewGrid()
    {
        //Delete children
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

        if (gameType == GameType.hex) {


            //Check for snake start pos
            if (snakeStartX < 0 || snakeStartY < 0) {
                grid = new HexGrid(xSize, ySize);
                grid = CreateSceneModelsHex((BaseGrid)grid);
            }
            //Start grid
            else {
                grid = new HexGrid(xSize, ySize, snakeStartX, snakeStartY);

            }
        }
        else if (gameType == GameType.square) {


            //Check for snake start pos
            if (snakeStartX < 0 || snakeStartY < 0) {
                grid = new SqGrid(xSize, ySize);

            }
            //Start grid
            else {
                grid = new SqGrid(xSize, ySize, snakeStartX, snakeStartY);

            }
        }



        //Create and Render the models

        /*for (int x = 0; x < xSize; x++)
         {
             for (int y = 0; y < ySize; y++)
             {
                 Occupant o = grid.GetCellValue(x, y);
                 GameObject go = Instantiate(cellPrefab, gridAnchorPoint.transform);
                 CellModel cm = go.GetComponent<CellModel>();
 
                 cm.Init(grid.GetCell(x, y));
                 cm.SetSize(cellSize - borderSize, cellSize);
                 grid.GetCell(x, y).SetModel(cm);
 
                 var newPos = anchorStart;
                 newPos.x += (x * (cellSize + cellSpacing)) + cellSpacing;
                 if (y % 2 == 0)
                 {
                     newPos.x += (cellSize / 2);
                 }
 
                 newPos.y += y * ((cellSize + cellSpacing) * .75f);
 
 
                 go.transform.position = newPos;
 
                 cm.UpdateCellColor(GetOccupantColor(o));
             }
         }*/
       
    }

    private HexGrid CreateSceneModelsHex(HexGrid hg) {
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
                Occupant o = hg.GetCellValue(x + xMod, y);
                GameObject go = Instantiate(cellPrefab, gridAnchorPoint.transform);
                CellModel cm = go.GetComponent<CellModel>();

                cm.Init(hg.GetCell(x +xMod, y));
                cm.SetSize(cellSize - borderSize, cellSize);
                hg.GetCell(x + xMod, y).SetModel(cm);

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
        
        return hg;
    }
    private SqGrid CreateSceneModelsSq(SqGrid sqGrid) {
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

        return sqGrid;
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
            case Occupant.snakeHead:
                return snakeHeadColor;
            default:
                Debug.LogError("Somehow we got passed an occupant that doesn't exist");
                return Color.magenta;
        }
    }

    public CellModel SetCell(int x, int y, Occupant newOccupant)
    {
        var cell = _hexGrid.GetCell(x, y);
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
        return _hexGrid.GetCell(x, y);
    }

    public Cell GetCell(Coord c)
    {
        return GetCell(c.x, c.y);
    }

    [ContextMenu("Show all the coordinates")]
    public void DebugAllCoordinates()
    {
        foreach (var c in gridAnchorPoint.GetComponentsInChildren<CellModel>())
        {
            c.DebugCoordinates();
        }
    }
}