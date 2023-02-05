using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grid
{
    protected Cell[,] grid;

    public Snake snake;

    
    public Grid(int xSize, int ySize, int snakeStartX = -1, int snakeStartY = -1)
    {
        xSize *= 2;
        grid = new Cell[xSize, ySize];

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                if (snakeStartX == x && snakeStartY == y)
                {
                    grid[x, y] = new Cell(x, y, Occupant.snake);
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.CreateSnake(grid[x,y]);
                    } 
                }
                else
                {
                    grid[x, y] = new Cell(x, y);
                }

                if ((x + y) % 2 == 1)
                {
                    SetCellValue(x,y, Occupant.bad);
                }

                Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
            }
        }
    }

    public Occupant GetCellValue(int x, int y)
    {
        return grid[x, y].GetCellValue();
    }

    public Cell GetCell(int x, int y)
    {
        return grid[x, y];
    }
    
    public Cell SetCellValue(int x, int y, Occupant occupant = 0)
    {
        return grid[x, y].SetCellValue(occupant);
    }
}


