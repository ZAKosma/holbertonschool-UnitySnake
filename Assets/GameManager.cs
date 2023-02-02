using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    private bool snakeIsGrowing = false;
    
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
        Coord nextCell = new Coord(snakeHead.X(), snakeHead.Y());
        //Move the player / snake
        switch (snake.snakeDirection)
        {
            case Direction.upRight:
                //Debug.Log("Current: " + grid.GetCell(snakeHead.X(), snakeHead.Y()).GetCellValue());
                nextCell.x += 1;
                nextCell.y -= 1;
                if (CheckNext(nextCell))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(nextCell, startingValue);
                break;
            case Direction.upLeft:
                nextCell.x--;
                nextCell.y--;
                //Debug.Log("Current: " + grid.GetCell(snakeHead.X(), snakeHead.Y()).GetCellValue());
                if (CheckNext(nextCell))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(nextCell, startingValue);
                break;
            case Direction.downLeft:
                nextCell.x--;
                nextCell.y++;
                if (CheckNext(nextCell))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(nextCell, startingValue);
                break;
            case Direction.downRight:
                nextCell.x++;
                nextCell.y++;
                if (CheckNext(nextCell))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(nextCell, startingValue);
                break;
            case Direction.right:
                nextCell.x -= 2;
                if (CheckNext(nextCell))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(nextCell, startingValue);
                break;
            case Direction.left:
                nextCell.x += 2;
                if (CheckNext(nextCell))
                {
                    GameOver();
                    return;
                }
                MoveSnakeTo(nextCell, startingValue);
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
                //First we add a new snake cell
                snakeIsGrowing = true;
                //Add fruit
                return false;
            case Occupant.wall:
                return true;
            case Occupant.snake:
                return true;
            default:
                return true;
        }
    }

    bool CheckNext(Coord c)
    {
        return CheckNext(c.x, c.y);
    }

    void MoveSnakeTo(int x, int y, Coord oldCoord)
    {
        /* Added to Snake Script as MoveSnakePart()
         * grid.SetCell(x, y, Occupant.snake);
         * snake.snakeCells[0] = grid.GetCell(x, y);
        */
        snake.MoveSnakePart(x, y, 0);
        
        Coord previousPosition = oldCoord;
        //Move older snake body to front;
        for (var index = 1; index < snake.snakeCells.Count; index++)
        {
            grid.SetCell(previousPosition.x, previousPosition.y, Occupant.snake);

            var oldX = snake.snakeCells[index].X();
            var oldY = snake.snakeCells[index].Y();

            snake.snakeCells[index] = grid.GetCell(previousPosition.x, previousPosition.y);

            previousPosition.x = oldX;
            previousPosition.y = oldY;
        }

        if (snakeIsGrowing)
        {
            snake.Grow(previousPosition);
            
            snakeIsGrowing = false;
            AddFruit();
        }
        else
        {
            grid.SetCell(previousPosition.x, previousPosition.y, Occupant.empty);
        }
    }

    void MoveSnakeTo(Coord target, Coord oldCoord)
    {
        MoveSnakeTo(target.x, target.y, oldCoord);
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
        Debug.LogWarning("Generating fruit");
        for (var i = 0; i < GridModel.Instance.startingFruit; i++)
        {
            AddFruit();
        }
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

    public void AddFruit()
    {
        var isSafeSpawn = false;
        var spawnLocation = GetRandomCoordinate();

        while (!isSafeSpawn)
        {
            if (GridModel.Instance.GetCell(spawnLocation).GetCellValue() == Occupant.empty)
            {
                isSafeSpawn = true;
            }
            else
            {
                spawnLocation = GetRandomCoordinate();
            }
        }

        GridModel.Instance.SetCell(spawnLocation, Occupant.fruit);
    }

    private Coord GetRandomCoordinate()
    {
        Coord newCoord = new Coord(Random.Range(0, GridModel.Instance.xSize), 
            Random.Range(0, GridModel.Instance.ySize));

        return newCoord;
    }

    [ContextMenu("Debug my snake!")]
    private void DebugSnakePosition()
    {
        Debug.LogWarning("SNAKE DEBUG!!!");
        foreach (var body in snake.snakeCells)
        {
            Debug.Log("Snake location: " + body.X() + ", " + body.Y());

        }
        
    }
}
