using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(UIGridView))]
public class UIGridViewEditor : Editor
{
    private UIGridView _grid;
    private GridLayoutGroup _gridLayoutGroup;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (_grid == null)
        {
            _grid = target as UIGridView;
            _gridLayoutGroup = _grid.GetComponent<GridLayoutGroup>();
        }

        if (GUILayout.Button("Generate Grid!"))
            GenerateGrid();
    }

    private void GenerateGrid()
    {
        DestroyOldCells();
        FillGridInfo();
        CreateGrid();
    }

    private void DestroyOldCells()
    {
        var cellsInScene = FindObjectsOfType<UICellView>();
        foreach(var cell in cellsInScene)
        {
            DestroyImmediate(cell.gameObject);
        }
    }

    private void FillGridInfo()
    {
        if (_grid.RowsCount < _grid.ColumnsCount)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = _grid.RowsCount;
            return;
        }
        
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _grid.ColumnsCount;
    }

    private void CreateGrid()
    {
        for (var i = 0; i < _grid.RowsCount; i++)
        {
            for (var j = 0; j < _grid.ColumnsCount; j++)
            {
                var newCellView = Instantiate(_grid.CellPrefab, _gridLayoutGroup.transform).GetComponent<UICellView>();
                newCellView.Y = i;
                newCellView.X = j;
            }
        }
    }
}
