public interface IGrid
{ 
    ICell[,] Cells { get; }

    ICell GetCell(int x, int y);
    ICell GetStartPathCell();
    ICell GetEndPathCell();
}