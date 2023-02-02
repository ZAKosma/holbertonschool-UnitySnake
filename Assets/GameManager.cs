using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GridModel grid;

    private Snake snake;

    //public GameEvent simulate;
    private Coroutine nextSimulation;

    public int simulationsPerSecond = 2;

    public float startDelay = 3;
    
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Debug.Log("Other Singleton of GameManager exists, destroy me!");
            Destroy(this); 
        } 
        else 
        {
            Debug.Log("There can only be one!");
            Instance = this;
        } 
        grid = GridModel.Instance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GridModel.Instance;
        StartCoroutine(StartDelay());
    }

    void SimulateCurrentState()
    {
        var snakeHead = snake.GetSnakeHead();

        Coord startingValue = new Coord(snakeHead.X(), snakeHead.Y());
        //Move the player / snake
        switch (snake.snakeDirection)
        {
            case Direction.up:
                Debug.Log("Current: " + grid.GetCell(snakeHead.X(), snakeHead.Y()).GetCellValue());
                if (CheckNext(snakeHead.X(), snakeHead.Y() + 1))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(snakeHead.X(), snakeHead.Y() + 1, startingValue);
                break;
            case Direction.down:
                if (CheckNext(snakeHead.X(), snakeHead.Y() - 1))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(snakeHead.X(), snakeHead.Y() - 1, startingValue);
                break;
            case Direction.right:
                if (CheckNext(snakeHead.X() + 1, snakeHead.Y()))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(snakeHead.X() + 1, snakeHead.Y(), startingValue);
                break;
            case Direction.left:
                if (CheckNext(snakeHead.X() - 1, snakeHead.Y()))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(snakeHead.X() - 1, snakeHead.Y(), startingValue);
                break;

        }
        //Check if they hit anything
        //And???
    }

    private void GameOver()
    {
        //Game end! Uh oh
        Debug.Log("Game over!");
        StopSimulation();
    }

    //Returns true if the player is going to die?
    //Returns false if the the player can move into the next space
    bool CheckNext(int x, int y)
    {
        Debug.Log("Next: " + grid.GetCell(x, y).GetCellValue());
        var nextCell = grid.GetCell(x, y);

        if (x < 0 || x > grid.xSize)
        {
            //Hit wall
            Debug.Log("X is hitting the wall");
            return true;
        }
        if (y < 0 || y > grid.ySize)
        {
            //Hit wall
            Debug.Log("Y is hitting the wall");
            return true;
        }
        
        switch (nextCell.GetCellValue())
        {
            case Occupant.empty:
                return false;
            case Occupant.fruit:
                //Trigger fruiting!
                return false;
            case Occupant.wall:
                return true;
            case Occupant.snake:
                return true;
            default:
                return true;
        }
    }

    void MoveSnakeTo(int x, int y, Coord oldCoord)
    {
        grid.SetCell(x, y, Occupant.snake);
        snake.snakeCells[0] = grid.GetCell(x, y);
        
        Coord previousPosition = oldCoord;
        //Move older snake body to front;
        for (var index = 1; index < snake.snakeCells.Count - 1; index++)
        {
            previousPosition.x = snake.snakeCells[index].X();
            previousPosition.y = snake.snakeCells[index].Y();
            
            grid.SetCell(previousPosition.x, previousPosition.y, Occupant.snake);
            snake.snakeCells[1] = grid.GetCell(previousPosition.x, previousPosition.y);
        }

        grid.SetCell(previousPosition.x, previousPosition.y, Occupant.empty);
    }

    IEnumerator StartDelay()
    {
        var splitDelay = startDelay / 3;
        
        yield return new WaitForSeconds(.1f);
        //Debug.Log("Cell: " + grid.GetCell(grid.snakeStartX, grid.snakeStartY));
        snake = Snake();
        yield return new WaitForSeconds(splitDelay);
        StopLight.Instance.UpdateLight();
        yield return new WaitForSeconds(splitDelay);
        StopLight.Instance.UpdateLight();
        yield return new WaitForSeconds(splitDelay);
        StopLight.Instance.UpdateLight();
        Debug.LogWarning("Starting Simulation");
        StartCoroutine(Simulate());
    }

    IEnumerator Simulate()
    {
        Debug.Log("Simulation!");
        SimulateCurrentState();
        yield return new WaitForSeconds(1f / simulationsPerSecond);
        nextSimulation = StartCoroutine(Simulate());
    }

    public void StopSimulation()
    {
        StopCoroutine(Simulate());
        StopCoroutine(nextSimulation);
    }

    public Snake Snake()
    {
        if (snake == null)
        {
            if (grid == null)
            {
                //Probably starting or somethin idk
                snake = new Snake(new Cell(0, 0, Occupant.snake));
                return snake;
            }
            snake = new Snake(grid.GetCell(grid.snakeStartX, grid.snakeStartY));
        }
        return snake;
    }

    public void CreateSnake(Cell c)
    {
        snake = new Snake(new Cell(0, 0, Occupant.snake));
        snake.InitSnake(c);
    }
}