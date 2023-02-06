using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BaseGrid
{
    public BaseGrid(int xSize, int ySize, int snakeStartX = -1, int snakeStartY = -1) {
        Debug.Log("Abstract constructor");
        grid = new Cell[xSize, ySize];


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
                Debug.Log("X: " + x + " Y: " + y + " Value: " + grid[x,y].GetCellValue());
            }
        }
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
}


public abstract class A{

    private string data;

    protected A(string myString){
        data = myString;
    }

}

public class B : A {

    B(string myString) : base(myString){}

}