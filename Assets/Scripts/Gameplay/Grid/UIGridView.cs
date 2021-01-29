using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class UIGridView : MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private int _rowsCount;
    [SerializeField] private int _columnsCount;

    private Grid _grid;

    public GameObject CellPrefab => _cellPrefab;
    public int RowsCount => _rowsCount;
    public int ColumnsCount => _columnsCount;
    public IGrid Grid => _grid;

    private void Awake()
    {
        CheckReferences();

        var cells = GetCells();
        _grid = new Grid(cells);
    }

    private void CheckReferences()
    {
        if (_cellPrefab == null)
            Debug.LogError("Can't find cell prefab");
    }

    private ICell[,] GetCells()
    {
        var cells = new ICell[_rowsCount, _columnsCount];
        var childCells = GetComponentsInChildren<UICellView>();
        foreach(var childCell in childCells)
        {
            cells[childCell.Y, childCell.X] = childCell.Cell;
        }

        return cells;
    }
}
