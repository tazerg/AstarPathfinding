using System.Collections.Generic;

public interface IPathfindingAlgorithm
{
    IEnumerable<ICell> GetShortestPath(IGrid grid);
}