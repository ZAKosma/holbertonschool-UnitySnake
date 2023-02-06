using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SqGrid : BaseGrid
{
    public SqGrid(int xSize, int ySize, int snakeStartX = -1, int snakeStartY = -1) : base(xSize, ySize, snakeStartX, snakeStartY) {
        Debug.Log("Square constructor");
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
            }
        }
    }
}


