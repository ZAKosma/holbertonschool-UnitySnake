using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Grid
{
    private Cell[,] grid;

    public Snake snake;

    public Grid()
    {
        grid = new Cell[12, 12];

        for (int x = 0; x < 12; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                grid[x, y] = new Cell(x, y);
            }
        }
    }
    
    public Grid(int xSize, int ySize)
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
    
    public Grid(int xSize, int ySize, int snakeStartX, int snakeStartY)
    {
        grid = new Cell[xSize, ySize];

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (snakeStartX == x && snakeStartY == y)
                {
                    grid[x, y] = new Cell(x, y, Occupant.snake);
                    GameManager.Instance.CreateSnake(grid[x,y]);
                    //Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
                }
                else
                {
                    grid[x, y] = new Cell(x, y);
                    //Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
                }
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


