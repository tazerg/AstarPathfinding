using System;

public class Cell : ICell
{
    public Cell(StaticCellType staticCellType, DynamicCellType dynamicCellType, int x, int y)
    {
        StaticCellType = staticCellType;
        DynamicCellType = dynamicCellType;
        X = x;
        Y = y;
    }

    public event Action PartOfPathMarked;
    public StaticCellType StaticCellType { get; }
    public DynamicCellType DynamicCellType { get; set; }

    public int X { get; }
    public int Y { get; }

    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost => GCost + HCost;
    
    public ICell ParentCell { get; set; }
    public void SetAsPathCell()
    {
        DynamicCellType = DynamicCellType.PartOfPath;
        PartOfPathMarked?.Invoke();
    }
}
