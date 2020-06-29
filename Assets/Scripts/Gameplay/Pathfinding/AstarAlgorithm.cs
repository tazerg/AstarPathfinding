using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AstarAlgorithm 
{
    const int HORIZONTAL_VERTICAL_COST = 10;
    const int DIAGONAL_COST = 14;

    static List<Cell> neighbourCells = new List<Cell>();
    static List<Cell> openCells = new List<Cell>();
    static List<Cell> closedCells = new List<Cell>();

    public static void Run(Grid grid)
    {
        openCells.Clear();
        closedCells.Clear();

        //Стартовая и конечная точки на пути
        Cell startPathCell = grid.GetStartPathCell();
        Cell endPathCell = grid.GetEndPathCell();

        //Проверка наличия граничных точек
        bool haveStartPathPoint = startPathCell != null;
        bool haveEndPathPoint = endPathCell != null;
        if (!haveStartPathPoint || !haveEndPathPoint)
        {
            if (!haveStartPathPoint && !haveEndPathPoint)
                ErrorMessageGenerator.ShowErrorMessage(ErrorMessageType.PathBordersNotFound);
            else if (!haveStartPathPoint)
                ErrorMessageGenerator.ShowErrorMessage(ErrorMessageType.StartPathPointNotFound);
            else if (!haveEndPathPoint)
                ErrorMessageGenerator.ShowErrorMessage(ErrorMessageType.EndPathPointNotFound);

            return;
        }

        openCells.Add(startPathCell);

        //Инициализируем начальную стоимость достижения ячейки
        foreach(Cell cell in grid.Cells)
        {
            cell.GCost = int.MaxValue;
        }
        startPathCell.GCost = 0;
        startPathCell.HCost = CalculateDistanceCost(startPathCell, endPathCell);

        bool isPathFinded = false;

        //Основной алгоритм
        while (openCells.Count > 0)
        {
            //Поиск открытой точки c наименьшей суммарной стоимостью
            int lowestFCostInOpenCells = openCells.Min(x => x.FCost);
            Cell lowestFCostCell = openCells.Find(x => x.FCost == lowestFCostInOpenCells);

            //Если эта точка конец пути - алгоритм завершен.
            if (lowestFCostCell == endPathCell)
            {
                isPathFinded = true;
                break;
            }

            openCells.Remove(lowestFCostCell);
            closedCells.Add(lowestFCostCell);

            //Сбор соседних точек
            CollectNeighbourCells(grid, lowestFCostCell);
            foreach (Cell neighbourCell in neighbourCells)
            {
                if (closedCells.Contains(neighbourCell))
                    continue;

                //Расчёт нового GCost
                int newNeighbourGCost = lowestFCostCell.GCost + CalculateDistanceCost(lowestFCostCell, neighbourCell);
                //Если новая стоимость меньше хранимой в ячейке - переписываем данные
                if (newNeighbourGCost < neighbourCell.GCost)
                {
                    neighbourCell.ParentCell = lowestFCostCell;
                    neighbourCell.GCost = newNeighbourGCost;
                    neighbourCell.HCost = CalculateDistanceCost(neighbourCell, endPathCell);

                    if (!openCells.Contains(neighbourCell))
                        openCells.Add(neighbourCell);
                }
            }
        }

        if (!isPathFinded)
        {
            ErrorMessageGenerator.ShowErrorMessage(ErrorMessageType.PathNotFound);
            return;
        }

        CalculateLowestPath(startPathCell, endPathCell);
    }

    static int CalculateDistanceCost(Cell start, Cell end)
    {
        int xDifference = Mathf.Abs(start.XCoordinate - end.XCoordinate);
        int yDifference = Mathf.Abs(start.YCoordinate - end.YCoordinate);
        int difference = Mathf.Abs(xDifference - yDifference);

        return DIAGONAL_COST * Mathf.Min(xDifference, yDifference) + HORIZONTAL_VERTICAL_COST * difference;
    }

    static void CollectNeighbourCells(Grid grid, Cell originCell)
    {
        neighbourCells.Clear();

        const int OFFSET = 1;
        //Соседи слева
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate - OFFSET, originCell.YCoordinate));
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate - OFFSET, originCell.YCoordinate - OFFSET));
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate - OFFSET, originCell.YCoordinate + OFFSET));

        //Соседи справа
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate + OFFSET, originCell.YCoordinate));
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate + OFFSET, originCell.YCoordinate - OFFSET));
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate + OFFSET, originCell.YCoordinate + OFFSET));

        //Сосед сверху
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate, originCell.YCoordinate + OFFSET));

        //Сосед сниху
        neighbourCells.AddIfNotNull(grid.GetCellByCoordinates(originCell.XCoordinate, originCell.YCoordinate - OFFSET));
    }

    static void CalculateLowestPath(Cell startPath, Cell endPath)
    {
        Cell currentCell = endPath.ParentCell;

        //Если родительская точка сразу является стартовой - строить путь не нужно
        if (currentCell == startPath)
            return;

        do
        {
            currentCell.SetAsPathCell();
            currentCell = currentCell.ParentCell;
        } while (currentCell != startPath);
    }
}
