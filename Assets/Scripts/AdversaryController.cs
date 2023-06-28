
public class AdversaryController
{
    private GameGrid grid;
    private GameController controller;

    public AdversaryController(GameGrid gameGrid, GameController gameController)
    {
        grid = gameGrid;
        controller = gameController;
    }

    public void MakeMove()
    {
        if (grid.IsFull()) return;
        var position = grid.FindRandomPosition();
        controller.Play(position, PlayerMarking.Two); 
    }
}
