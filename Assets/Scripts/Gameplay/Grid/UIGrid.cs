using UnityEngine;
using UnityEngine.UI;

public class UIGrid : Grid
{
    [SerializeField]
    GridLayoutGroup gridLayoutGroup;
    public GridLayoutGroup GridLayoutGroup => gridLayoutGroup;

    [SerializeField]
    GameObject cellPrefab;
    public GameObject CellPrefab => cellPrefab;

    [SerializeField]
    int rowsCount;
    public override int RowsCount => rowsCount;
    [SerializeField]
    int columnsCount;
    public override int ColumnsCount => columnsCount;

    Cell[,] cells;
    public override Cell[,] Cells 
    {
        get
        {
            if (cells == null)
            {
                cells = new Cell[RowsCount, ColumnsCount];

                Cell[] childCells = GetComponentsInChildren<Cell>();
                foreach(Cell childCell in childCells)
                {
                    cells[childCell.YCoordinate, childCell.XCoordinate] = childCell;
                }
            }

            return cells;
        }

        protected set => cells = value;
    }
}
