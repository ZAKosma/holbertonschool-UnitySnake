using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseGrid
{
    
    public BaseGrid(int xSize, int ySize, int snakeStartX = -1, int snakeStartY = -1)
    {
        Debug.Log("Abstract constructor");
        /*grid = new Cell[xSize, ySize];


        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                if (snakeStartX == x && snakeStartY == y)
                {
                    grid[x, y] = new Cell(x, y, Occupant.snake);
                }
                else
                {
                    grid[x, y] = new Cell(x, y);
                }

                Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x, y].GetCellValue());
            }
        }*/
    }

    protected Cell[,] grid;

    public Snake snake;

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

    public abstract Coord GetRandomCoordinate();

    public bool CheckIfCoordExists(Coord c)
    {
        var xLength = grid.GetLength(0);
        var yLength = grid.GetLength(1);

        if (c.x >= xLength - 1 || c.x < 0)
        {
            return false;
        }
        if (c.y >= yLength - 1 || c.y < 0)
        {
            return false;
        }

        return true;
    }

    public Coord GetNextHexCoord(Coord c)
    {
        var d = GameManager.Instance.Snake().GetSnakeHexDirection();
        var outCoord = new Coord(-1, -1);
        
        switch (d)
        {
            case HexDirection.upRight:
                outCoord.x = c.x + 1;
                outCoord.y = c.y + 1;
                break;
            case HexDirection.upLeft:
                outCoord.x = c.x - 1;
                outCoord.y = c.y + 1;
                break;
            case HexDirection.downLeft:
                outCoord.x = c.x - 1;
                outCoord.y = c.y - 1;
                break;
            case HexDirection.downRight:
                outCoord.x = c.x + 1;
                outCoord.y = c.y - 1;
                break;
            case HexDirection.right:
                outCoord.x = c.x + 2;
                outCoord.y = c.y;
                break;
            case HexDirection.left:
                outCoord.x = c.x - 2;
                outCoord.y = c.y;
                break;
            default:
                Debug.LogError("Next Coord Did not receive matching direction: " + d);
                break;
        }

        return outCoord;
    }
    public Coord GetNextSqCoord(Coord c)
    {
        var d = GameManager.Instance.Snake().GetSnakeSqDirection();
        
        var outCoord = new Coord(-1, -1);
        switch (d)
        {
            case SqDirection.up:
                outCoord.x = c.x;
                outCoord.y = c.y + 1;
                break;
            case SqDirection.down:
                outCoord.x = c.x;
                outCoord.y = c.y - 1;
                break;
            case SqDirection.right:
                outCoord.x = c.x + 1;
                outCoord.y = c.y;
                break;
            case SqDirection.left:
                outCoord.x = c.x - 1;
                outCoord.y = c.y;
                break;
            default:
                Debug.LogError("Next Coord Did not receive matching direction: " + d);
                break;
        }

        return outCoord;
    }

}