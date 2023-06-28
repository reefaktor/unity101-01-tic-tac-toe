using NUnit.Framework;

public class GameControllerTest
{
    [Test]
    public void GameControllerTestCreation()
    {
        var (controller, _) = Initialize();
        Assert.NotNull(controller);
    }

    [Test]
    public void GameControllerNotFinished()
    {
        var (controller, grid) = Initialize();
        Assert.AreEqual(grid.Slots[0], PlayerMarking.Empty);
        Assert.False(controller.IsFinished());
        // Assert.IsFalse(controller.IsFinished(grid));
    }

    [Test]
    public void GameControllerBaseState()
    {
        var (controller, _) = Initialize();
        Assert.False(controller.IsFinished());
    }

    [Test]
    public void GameControllerNoWinner()
    {
        var (_, grid) = Initialize();
        var (isWin, player) = GameController.FindWinner(grid);
        Assert.False(isWin);
        Assert.AreEqual(player, PlayerMarking.Empty);
    }

    [Test]
    public void GameControllerWinnerOne()
    {
        var (_, grid) = Initialize();
        const PlayerMarking marking = PlayerMarking.One;
        grid.Slots[0] = PlayerMarking.One;
        grid.Slots[1] = PlayerMarking.One;
        grid.Slots[2] = PlayerMarking.One;
        
        var (isWin, player) = GameController.FindWinner(grid);
        Assert.True(isWin);
        Assert.AreEqual(player, marking);
    }
    
    [Test]
    public void GameControllerWinnerTwo()
    {
        var (_, grid) = Initialize();
        const PlayerMarking marking = PlayerMarking.Two;
        grid.Slots[0] = PlayerMarking.Two;
        grid.Slots[1] = PlayerMarking.Two;
        grid.Slots[2] = PlayerMarking.Two;
        
        var (isWin, player) = GameController.FindWinner(grid);
        Assert.True(isWin);
        Assert.AreEqual(player, marking);
    }
    
    [TestCase(0, 1, 2)]
    [TestCase(3, 4, 5)]
    [TestCase(6, 7, 8)]
    [TestCase(0, 3, 6)]
    [TestCase(1, 4, 7)]
    [TestCase(2, 5, 8)]
    [TestCase(0, 4, 8)]
    [TestCase(2, 4, 6)]
    public void GameControllerWins(int a, int b, int c)
    {
        var (controller, grid) = Initialize();
        const PlayerMarking marking = PlayerMarking.One;
        grid.Slots[a] = marking;
        grid.Slots[b] = marking;
        grid.Slots[c] = marking;
        
        var (isWin, player) = controller.FindWinner();
        Assert.True(isWin);
        Assert.AreEqual(player, marking);
    }

    private static (GameController, GameGrid) Initialize()
    {
        var grid = new GameGrid();
        var controller = new GameController(grid);
        return (controller, grid);
    }


}
