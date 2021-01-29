using System.Collections.Generic;
using System.Linq;
using CryoDI;
using UnityEngine;

public class AlgorithmRunner : CryoBehaviour
{
    [Dependency] private UIGridView GridView { get; set; }
    [Dependency] private IPathfindingAlgorithm PathfindingAlgorithm { get; set; }

    private IEnumerable<ICell> _shortestPath;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FindShortestPath();
    }

    private void FindShortestPath()
    {
        _shortestPath = PathfindingAlgorithm.GetShortestPath(GridView.Grid);
        
        if (_shortestPath.Count() < 0)
            return;

        ShowShortestPath();
    }

    private void ShowShortestPath()
    {
        foreach (var cell in _shortestPath)
        {
            cell.SetAsPathCell();
        }
    }
}
