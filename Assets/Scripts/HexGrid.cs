using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HexGrid : BaseGrid
{
    public HexGrid(int xSize, int ySize, int snakeStartX = -1, int snakeStartY = -1) : base(xSize, ySize, snakeStartX, snakeStartY) {
        Debug.Log("Hex constructor");
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

                //Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
            }
        }
    }

    public override Coord GetRandomCoordinate()
    {
        Coord newCoord = new Coord(Random.Range(0, GridModel.Instance.xSizeAdjusted), 
            Random.Range(0, GridModel.Instance.ySize));

        if ((newCoord.x + newCoord.y) % 2 == 1)
        {
            return GetRandomCoordinate();
        }

        return newCoord;
    }
}


