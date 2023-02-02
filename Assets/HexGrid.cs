using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGrid : Grid
{
    public HexGrid()
    {
        grid = new Cell[24, 24];

        for (int x = 0; x < 24; x++)
        {
            for (int y = 0; y < 24; y++)
            {
                grid[x, y] = new Cell(x, y);
            }
        }
    }
    
    public HexGrid(int xSize, int ySize)
    {
        grid = new Cell[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                grid[x, y] = new Cell(x, y);
                Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
            }
        }
    }
    
    public HexGrid(int xSize, int ySize, int snakeStartX, int snakeStartY)
    {
        grid = new Cell[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (snakeStartX == x && snakeStartY == y)
                {
                    grid[x, y] = new Cell(x, y, Occupant.snake);
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.CreateSnake(grid[x,y]);
                    }
                    Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
                }
                else
                {
                    grid[x, y] = new Cell(x, y);
                    Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
                }
            }
        }
    }
}


