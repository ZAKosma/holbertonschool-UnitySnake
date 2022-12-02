using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Occupant
{
    empty = 0,
    snake = 1,
    wall = 2,
    fruit = 3
}

public class Coord
{
    public int x;
    public int y;

    public Coord(int xValue, int yValue)
    {
        x = xValue;
        y = yValue;
    }
}

public class Cell
{
    private int x;
    private int y;

    private Occupant cellValue;

    private CellModel model;

    public Cell(int xPos, int yPos, Occupant occupant = 0)
    {
        x = xPos;
        y = yPos;

        cellValue = occupant;
    }

    public Cell SetCellValue(Occupant occupant)
    {
        cellValue = occupant;

        

        return this;
    }

    public Cell SetCellValue(int occupantValue)
    {
        return SetCellValue(occupantValue);
    }

    public CellModel SetModel(CellModel cm)
    {
        model = cm;
        return model;
    }

    public CellModel GetModel()
    {
        return model;
    }

    public Occupant GetCellValue()
    {
        return cellValue;
    }

    public int X()
    {
        return x;
    }
    public int Y()
    {
        return y;
    }
}
