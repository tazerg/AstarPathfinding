public class Grid : IGrid
{
    public Grid(ICell[,] cells)
    {
        Cells = cells;
        _rowsCount = Cells.GetLength(0);
        _columnsCount = Cells.GetLength(1);
    }

    private readonly int _rowsCount;
    private readonly int _columnsCount;
    
    public ICell[,] Cells { get; }

    public ICell GetCell(int x, int y)
    {
        //Если одна из координат не валидна или ячейка в этой координате - стена
        if (!IsCoordinateValidValue(x, _columnsCount) || 
            !IsCoordinateValidValue(y, _rowsCount) || 
            Cells[y, x].StaticCellType == StaticCellType.Wall)
            //Ничего не возвращаем
            return null;

        return Cells[y, x];
    }

    public ICell GetStartPathCell()
    {
        var startCell = FindCell(DynamicCellType.StartPath);
        return startCell;
    }

    public ICell GetEndPathCell()
    {
        var endCell = FindCell(DynamicCellType.EndPath);
        return endCell;
    }

    private bool IsCoordinateValidValue(int coordinateValue, int coordinateMaxValue)
    {
        return coordinateValue < coordinateMaxValue && coordinateValue >= 0;
    }

    private ICell FindCell(DynamicCellType dynamicCellType)
    {
        for (var i = 0; i < _rowsCount; i++)
        {
            for (var j = 0; j < _columnsCount; j++)
            {
                if (Cells[i, j].DynamicCellType != dynamicCellType) 
                    continue;

                return Cells[i, j];
            }
        }

        return null;
    }
}
