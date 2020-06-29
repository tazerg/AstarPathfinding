using UnityEngine;

public abstract class Grid : MonoBehaviour
{
    public abstract int RowsCount { get; }
    public abstract int ColumnsCount { get; }

    public abstract Cell[,] Cells { get; protected set; }

    public Cell GetCellByCoordinates(int xCoordinate, int yCoordinate)
    {
        //Если одна из координат не валидна или ячейка в этой координате - стена
        if (!IsCoordinateValidValue(xCoordinate, ColumnsCount) || !IsCoordinateValidValue(yCoordinate, RowsCount) || Cells[yCoordinate, xCoordinate].CellType == CellType.Wall)
            //Ничего не возвращаем
            return null;

        return Cells[yCoordinate, xCoordinate];
    }

    public Cell GetStartPathCell()
    {
        Cell cellToReturn = null;
        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                if (Cells[i, j].IsStartPathCell)
                {
                    cellToReturn = Cells[i, j];
                    break;
                }
            }
        }

        return cellToReturn;
    }

    public Cell GetEndPathCell()
    {
        Cell cellToReturn = null;
        for (int i = 0; i < RowsCount; i++)
        {
            for (int j = 0; j < ColumnsCount; j++)
            {
                if (Cells[i, j].IsEndPathCell)
                {
                    cellToReturn = Cells[i, j];
                    break;
                }
            }
        }

        return cellToReturn;
    }

    bool IsCoordinateValidValue(int coordinateValue, int coordinateMaxValue) => coordinateValue < coordinateMaxValue && coordinateValue >= 0;
}
