using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum HexDirection
{
    left = 0,
    upLeft = 1,
    upRight = 2,
    right = 3,
    downRight = 4,
    downLeft = 5
}
public enum SqDirection
{
    left = 0,
    up = 2,
    right = 3,
    down = 4
}

public class Snake {
    [SerializeField] private HexDirection _snakeHexDirection;

    [SerializeField] private SqDirection _snakeSqDirection;

    public List<Cell> snakeCells;

    public Snake(Cell startingCell) {
        InitSnake(startingCell);
    }

    public Snake InitSnake(Cell c) {
        snakeCells = new List<Cell>();

        snakeCells.Add(c);

        _snakeHexDirection = HexDirection.right;
        _snakeSqDirection = SqDirection.right;

        return this;
    }

    public Cell GetSnakeHead() {
        return snakeCells[0];
    }

    public void SetSnakeHead(Cell snakeHead) {
        InitSnake(snakeHead);
    }

    public void Grow(Coord c) {
        snakeCells.Add(GridModel.Instance.GetCell(c));
    }

    public void MoveSnakePart(int x, int y, int index) {
        snakeCells[index] = GridModel.Instance.SetCell(x, y, Occupant.snake).GetCellRaw();
    }

    public void MoveSnakeHead(int x, int y) {
        snakeCells[0] = GridModel.Instance.SetCell(x, y, Occupant.snakeHead).GetCellRaw();
    }

    public void RotateLeft() {
        _snakeHexDirection = _snakeHexDirection - 1;
        _snakeSqDirection = _snakeSqDirection - 1;
        if ((int)_snakeHexDirection < 0) {
            _snakeHexDirection = HexDirection.downLeft;
            _snakeSqDirection = SqDirection.down;
        }

        GameManager.Instance.HighlightCells(snakeCells[0].Coord(), _snakeHexDirection);
    }

    public void RotateRight() {
        _snakeHexDirection = _snakeHexDirection + 1;
        _snakeSqDirection = _snakeSqDirection + 1;
        if ((int)_snakeHexDirection > 5) {
            _snakeHexDirection = HexDirection.left;
            _snakeSqDirection = SqDirection.left;
        }

        GameManager.Instance.HighlightCells(snakeCells[0].Coord(), _snakeHexDirection);

    }

    public HexDirection GetSnakeDirection() {
        return _snakeHexDirection;
    }

    public SqDirection GetSnakeSqDirection() {
        return _snakeSqDirection;
    }

    public SqDirection SetSnakeDirection(SqDirection d) {
        _snakeSqDirection = d;
    }
}
