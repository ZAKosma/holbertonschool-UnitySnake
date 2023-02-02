using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    right = 0,
    left = 1,
    upRight = 2,
    upLeft = 3,
    downRight = 4,
    downLeft = 5
}
public class Snake
{
    public Direction snakeDirection;

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
}
