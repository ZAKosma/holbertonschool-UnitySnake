using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Direction
{
    up = 0,
    down = 1,
    right = 2,
    left = 3
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

        snakeDirection = Direction.up;

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
