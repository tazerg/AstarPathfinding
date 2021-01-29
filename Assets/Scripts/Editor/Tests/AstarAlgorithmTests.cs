using System.Collections.Generic;
using CryoDI;
using Moq;
using NUnit.Framework;

public class AstarAlgorithmTests
{
    private IPathfindingAlgorithm _pathfindingAlgorithm;
    
    [SetUp]
    public void OnSetup()
    {
        var container = new CryoContainer();
        container.RegisterInstance(new Mock<IErrorMessageController>().Object);
        container.RegisterSingleton<IPathfindingAlgorithm, AstarAlgorithm>();

        _pathfindingAlgorithm = container.Resolve<IPathfindingAlgorithm>();
    }
    
    [Test] 
    public void TestPathfindingWithoutBorders()
    {
        var cells = new ICell[,]
        {
            { new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 0), new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 0)  },
            { new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 1), new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 1)  },
        };
        var grid = new Grid(cells);

        var actual = _pathfindingAlgorithm.GetShortestPath(grid);
        Assert.IsEmpty(actual);
    }
    
    [Test] 
    public void TestPathfindingWithoutStartBorder()
    {
        var cells = new ICell[,]
        {
            { new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 0), new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 0)  },
            { new Cell(StaticCellType.Floor, DynamicCellType.EndPath, 0, 1), new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 1)  },
        };
        var grid = new Grid(cells);

        var actual = _pathfindingAlgorithm.GetShortestPath(grid);
        Assert.IsEmpty(actual);
    }
    
    [Test] 
    public void TestPathfindingWithoutEndBorder()
    {
        var cells = new ICell[,]
        {
            { new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 0), new Cell(StaticCellType.Floor, DynamicCellType.StartPath, 1, 0)  },
            { new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 1), new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 1)  },
        };
        var grid = new Grid(cells);

        var actual = _pathfindingAlgorithm.GetShortestPath(grid);
        Assert.IsEmpty(actual);
    }

    [Test] 
    public void TestCorrectPathfinding()
    {
        var cell0x0 = new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 0);
        var cell0x1 = new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 0);
        var cell0x2 = new Cell(StaticCellType.Floor, DynamicCellType.None, 2, 0);
        var cell0x3 = new Cell(StaticCellType.Floor, DynamicCellType.None, 3, 0);
        var cell0x4 = new Cell(StaticCellType.Floor, DynamicCellType.None, 4, 0);
        var cell1x0 = new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 1);
        var cell1x1 = new Cell(StaticCellType.Wall, DynamicCellType.None, 1, 1);
        var cell1x2 = new Cell(StaticCellType.Wall, DynamicCellType.None, 2, 1);
        var cell1x3 = new Cell(StaticCellType.Wall, DynamicCellType.None, 3, 1);
        var cell1x4 = new Cell(StaticCellType.Floor, DynamicCellType.None, 4, 1);
        var cell2x0 = new Cell(StaticCellType.Floor, DynamicCellType.StartPath, 0, 2);
        var cell2x1 = new Cell(StaticCellType.Wall, DynamicCellType.None, 1, 2);
        var cell2x2 = new Cell(StaticCellType.Floor, DynamicCellType.EndPath, 2, 2);
        var cell2x3 = new Cell(StaticCellType.Floor, DynamicCellType.None, 3, 2);
        var cell2x4 = new Cell(StaticCellType.Floor, DynamicCellType.None, 4, 2);
        var cell3x0 = new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 3);
        var cell3x1 = new Cell(StaticCellType.Wall, DynamicCellType.None, 1, 3);
        var cell3x2 = new Cell(StaticCellType.Floor, DynamicCellType.None, 2, 3);
        var cell3x3 = new Cell(StaticCellType.Floor, DynamicCellType.None, 3, 3);
        var cell3x4 = new Cell(StaticCellType.Floor, DynamicCellType.None, 4, 3);
        var cell4x0 = new Cell(StaticCellType.Floor, DynamicCellType.None, 0, 4);
        var cell4x1 = new Cell(StaticCellType.Floor, DynamicCellType.None, 1, 4);
        var cell4x2 = new Cell(StaticCellType.Floor, DynamicCellType.None, 2, 4);
        var cell4x3 = new Cell(StaticCellType.Floor, DynamicCellType.None, 3, 4);
        var cell4x4 = new Cell(StaticCellType.Floor, DynamicCellType.None, 4, 4);

        var cells = new ICell[,]
        {
            {cell0x0, cell0x1, cell0x2, cell0x3, cell0x4},
            {cell1x0, cell1x1, cell1x2, cell1x3, cell1x4},
            {cell2x0, cell2x1, cell2x2, cell2x3, cell2x4},
            {cell3x0, cell3x1, cell3x2, cell3x3, cell3x4},
            {cell4x0, cell4x1, cell4x2, cell4x3, cell4x4},
        };
        var grid = new Grid(cells);

        var expected = new List<ICell>
        {
            cell3x2, cell4x1, cell3x0
        };
        var actual = _pathfindingAlgorithm.GetShortestPath(grid);
        Assert.IsNotEmpty(actual);
        Assert.AreEqual(expected, actual);
    }
}