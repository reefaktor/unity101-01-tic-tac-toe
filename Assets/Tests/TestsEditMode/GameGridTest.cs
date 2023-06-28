using NUnit.Framework;

public class GameGridTest
{
    [Test]
    public void GameGridTestCreation()
    {
        var grid = new GameGrid();
        Assert.NotNull(grid);
        Assert.NotNull(grid.Slots);
        Assert.AreEqual(grid.Slots[0], PlayerMarking.Empty);
    }

    [Test]
    public void GameGridIsEmpty()
    {
        var grid = new GameGrid();
        Assert.False(grid.IsFull());
    }

    [Test]
    public void GameGridIsFull()
    {
        var grid = new GameGrid();
        for (var i = 0; i < grid.Slots.Length; i++)
        {
            var marking = i % 2 == 0 ? PlayerMarking.One : PlayerMarking.Two;
            grid.Slots[i] = marking;
        }
        Assert.True(grid.IsFull());
        Assert.AreEqual(grid.Slots[0], PlayerMarking.One);
        Assert.AreEqual(grid.Slots[1], PlayerMarking.Two);
        Assert.AreEqual(grid.Slots[8], PlayerMarking.One);
    }
    
    [Test]
    public void GameGridNotRandomPosition()
    {
        var grid = new GameGrid();
        for (var i = 0; i < grid.Slots.Length; i++)
        {
            var marking = i % 2 == 0 ? PlayerMarking.One : PlayerMarking.Two;
            grid.Slots[i] = marking;
        }
        Assert.NotNull(grid);
        Assert.AreEqual(grid.FindRandomPosition(), -1);
        Assert.True(grid.IsFull());
    }

    [Test]
    public void GameGridRandomPosition()
    {
        var grid = new GameGrid();
        Assert.NotNull(grid);
        Assert.Greater(grid.FindRandomPosition(), -1);
    }
}
