using System;
public class GameController
{
    public event Action<PlayerMarking> OnPlayerChanged;
    public event Action<int, PlayerMarking> OnMoveMade;
    public event Action<PlayerMarking> OnGameFinished;

    private static int[,] comparisons =
    {
        {0, 1, 2},
        {3, 4, 5},
        {6, 7, 8},
        {0, 3, 6},
        {1, 4, 7},
        {2, 5, 8},
        {0, 4, 8},
        {2, 4, 6}
    };
    private GameGrid grid;
    private PlayerMarking currentPlayer;

    public GameController(GameGrid gameGrid)
    {
        grid = gameGrid;
        currentPlayer = PlayerMarking.Empty;
    }

    public void Init(PlayerMarking player)
    {
        if (player == PlayerMarking.Empty) return;
        
        currentPlayer = player;
        OnPlayerChanged?.Invoke(currentPlayer);
    }

    public bool IsFinished()
    {
        return IsFinished(grid);
    }
    
    public void Play(int position, PlayerMarking marking)
    {
        if (grid.Slots[position] != PlayerMarking.Empty) return;
        if (currentPlayer != marking) return;

        grid.Slots[position] = marking;
        OnMoveMade?.Invoke(position, marking);
        
        var (isWin, winner) = FindWinner();
        if (isWin || grid.IsFull())
        {
            OnGameFinished?.Invoke(winner);
            return;
        }
        
        currentPlayer = currentPlayer == PlayerMarking.One
            ? PlayerMarking.Two
            : PlayerMarking.One;
        OnPlayerChanged?.Invoke(currentPlayer);
    }

    public (bool, PlayerMarking) FindWinner()
    {
        return FindWinner(grid);
    }
    
    public static bool IsFinished(GameGrid gameGrid)
    {
        var (isWin, _) = FindWinner(gameGrid);
        return isWin || gameGrid.IsFull();
    }

    public static (bool, PlayerMarking) FindWinner(GameGrid grid)
    {
        var winner = PlayerMarking.Empty;
        var isWin = false;

        for (var i = 0; i < comparisons.GetLength(0); i++)
        {
            var a = comparisons[i, 0];
            var b = comparisons[i, 1];
            var c = comparisons[i, 2];
            var slots = grid.Slots;
        
            if (slots[a] != PlayerMarking.Empty &&
                slots[a] == slots[b] &&
                slots[b] == slots[c])
            {
                isWin = true;
                winner = slots[a];
                break;
            }
        }

        return (isWin, winner);
    }
    
}
