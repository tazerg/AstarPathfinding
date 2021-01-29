public interface ICell
{
    StaticCellType StaticCellType { get; }
    DynamicCellType DynamicCellType { get; }
    
    int X { get; }
    int Y { get; }
    
    int GCost { get; set; }
    int HCost { set; }
    int FCost { get; }

    ICell ParentCell { get; set; }

    void SetAsPathCell();
}