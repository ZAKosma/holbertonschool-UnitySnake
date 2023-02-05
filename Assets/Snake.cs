using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    left = 0,
    upLeft = 1,
    upRight = 2,
    right = 3,
    downRight = 4,
    downLeft = 5
}
public class Snake
{
    [SerializeField]
    private Direction snakeDirection;

    public List<Cell> snakeCells;

    public Snake(Cell startingCell)
    {
        InitSnake(startingCell);
    }

    public Snake InitSnake(Cell c)
    {
        snakeCells = new List<Cell>();
        
        snakeCells.Add(c);

        snakeDirection = Direction.right;

        return this;
    }

    public Cell GetSnakeHead()
    {
        return snakeCells[0];
    }

    public void SetSnakeHead(Cell snakeHead)
    {
        InitSnake(snakeHead);
    }

    public void Grow(Coord c)
    {
        snakeCells.Add(GridModel.Instance.GetCell(c));
    }

    public void MoveSnakePart(int x, int y, int index)
    {
        snakeCells[index] = GridModel.Instance.SetCell(x, y, Occupant.snake).GetCellRaw();
    }
    public void MoveSnakeHead(int x, int y)
    {
        snakeCells[0] = GridModel.Instance.SetCell(x, y, Occupant.snakeHead).GetCellRaw();
    }

    public void RotateLeft()
    {
        snakeDirection = snakeDirection - 1;
        if ((int)snakeDirection < 0)
        {
            snakeDirection = Direction.downLeft;
        }
        
        GameManager.Instance.HighlightCells(snakeCells[0].Coord(), snakeDirection);
    }
    public void RotateRight()
    {
        snakeDirection = snakeDirection + 1;
        if ((int)snakeDirection > 5)
        {
            snakeDirection = Direction.left;
        }
        
        GameManager.Instance.HighlightCells(snakeCells[0].Coord(), snakeDirection);

    }

    public Direction GetSnakeDirection()
    {
        return snakeDirection;
    }
}
