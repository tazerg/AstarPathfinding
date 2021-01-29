using System.Collections.Generic;
using System.Linq;
using CryoDI;
using UnityEngine;

public class AstarAlgorithm : IPathfindingAlgorithm
{
    private const int HorizontalVerticalCost = 10;
    private const int DiagonalCost = 14;
    private const int Offset = 1;
    
    [Dependency] private IErrorMessageController ErrorMessageController { get; set; }

    private readonly IList<ICell> _neighbourCells = new List<ICell>();
    private readonly IList<ICell> _openCells = new List<ICell>();
    private readonly IList<ICell> _closedCells = new List<ICell>();
    private readonly IList<ICell> _shortestPath = new List<ICell>();

    private IGrid _workedGrid;
    private ICell _startPathCell;
    private ICell _endPathCell;
    private ICell _processedCell;
    private ICell _currentNeighbourCell;

    private bool _isPathFinished;

    public IEnumerable<ICell> GetShortestPath(IGrid grid)
    {
        _workedGrid = grid;
        
        ClearData();
        CollectBoundaryValues();
        if (TryShowErrorMessage())
            return _shortestPath;

        InitializePathfinding();
        FindShortestPath();

        if (!_isPathFinished)
        {
            ShowErrorMessage(ErrorMessageType.PathNotFound);
            return _shortestPath;
        }
        
        CalculateLowestPath();
        return _shortestPath;
    }

    private void ClearData()
    {
        _neighbourCells.Clear();
        _openCells.Clear();
        _closedCells.Clear();
        _shortestPath.Clear();
    }

    private void CollectBoundaryValues()
    {
        _startPathCell = _workedGrid.GetStartPathCell();
        _endPathCell = _workedGrid.GetEndPathCell();
    }

    private bool TryShowErrorMessage()
    {
        var haveStartPathPoint = _startPathCell != null;
        var haveEndPathPoint = _endPathCell != null;

        if (!haveStartPathPoint && !haveEndPathPoint)
        {
            ShowErrorMessage(ErrorMessageType.PathBordersNotFound);
            return true;
        }

        if (!haveStartPathPoint)
        {
            ShowErrorMessage(ErrorMessageType.StartPathPointNotFound);
            return true;
        }

        if (!haveEndPathPoint)
        {
            ShowErrorMessage(ErrorMessageType.EndPathPointNotFound);
            return true;
        }

        return false;
    }

    private void ShowErrorMessage(ErrorMessageType errorMessageType)
    {
        ErrorMessageController.ShowErrorMessage(errorMessageType);
    }

    private void InitializePathfinding()
    {
        _openCells.Add(_startPathCell);

        foreach(var cell in _workedGrid.Cells)
        {
            cell.GCost = int.MaxValue;
        }
        _startPathCell.GCost = 0;
        _startPathCell.HCost = CalculateDistanceCost(_startPathCell, _endPathCell);

        _isPathFinished = false;
    }

    private int CalculateDistanceCost(ICell start, ICell end)
    {
        var xDifference = Mathf.Abs(start.X - end.X);
        var yDifference = Mathf.Abs(start.Y - end.Y);
        var difference = Mathf.Abs(xDifference - yDifference);

        return DiagonalCost * Mathf.Min(xDifference, yDifference) + HorizontalVerticalCost * difference;
    }

    private void FindShortestPath()
    {
        while (_openCells.Count > 0)
        {
            _processedCell = FindLowestFCostCell();
            if (IsEndOfPath())
            {
                _isPathFinished = true;
                break;
            }
            
            CloseProcessedCell();
            CollectNeighbourCells();
            ProcessNeighbourCells();
        }
    }

    private ICell FindLowestFCostCell()
    {
        var lowestFCostInOpenCells = _openCells.Min(x => x.FCost);
        var lowestFCostCell = _openCells.FirstOrDefault(x => x.FCost == lowestFCostInOpenCells);
        return lowestFCostCell;
    }

    private bool IsEndOfPath()
    {
        return _processedCell == _endPathCell;
    }

    private void CloseProcessedCell()
    {
        _openCells.Remove(_processedCell);
        _closedCells.Add(_processedCell);
    }

    private void CollectNeighbourCells()
    {
        _neighbourCells.Clear();

        //Соседи слева
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X - Offset, _processedCell.Y));
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X - Offset, _processedCell.Y - Offset));
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X - Offset, _processedCell.Y + Offset));

        //Соседи справа
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X + Offset, _processedCell.Y));
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X + Offset, _processedCell.Y - Offset));
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X + Offset, _processedCell.Y + Offset));

        //Сосед сверху
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X, _processedCell.Y + Offset));

        //Сосед сниху
        _neighbourCells.AddIfNotNull(_workedGrid.GetCell(_processedCell.X, _processedCell.Y - Offset));
    }

    private void ProcessNeighbourCells()
    {
        foreach (var neighbourCell in _neighbourCells)
        {
            if (_closedCells.Contains(neighbourCell))
                continue;

            var newNeighbourGCost = _processedCell.GCost + CalculateDistanceCost(_processedCell, neighbourCell);
            if (newNeighbourGCost >= neighbourCell.GCost)
                continue;
            
            UpdateNeighbourCellInfo(neighbourCell, newNeighbourGCost);
        }
    }

    private void UpdateNeighbourCellInfo(ICell neighbourCell, int newGCost)
    {
        neighbourCell.ParentCell = _processedCell;
        neighbourCell.GCost = newGCost;
        neighbourCell.HCost = CalculateDistanceCost(neighbourCell, _endPathCell);

        if (!_openCells.Contains(neighbourCell))
            _openCells.Add(neighbourCell);
    }

    private void CalculateLowestPath()
    {
        _shortestPath.Clear();
        
        var currentCell = _endPathCell.ParentCell;
        if (currentCell == _startPathCell)
            return;

        do
        {
            _shortestPath.Add(currentCell);
            currentCell = currentCell.ParentCell;
        } while (currentCell != _startPathCell);
    }
}
