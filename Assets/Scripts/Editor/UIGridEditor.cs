using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(UIGrid))]
public class UIGridEditor : Editor
{
    UIGrid grid;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        grid = target as UIGrid;
        if (GUILayout.Button("Generate Grid!"))
            GenerateGrid();
    }

    public void GenerateGrid()
    {
        UICell[] cellsInScene = FindObjectsOfType<UICell>();
        foreach(UICell cell in cellsInScene)
        {
            DestroyImmediate(cell.gameObject);
        }

        //Заполняем информацию для новой сетки
        if (grid.RowsCount < grid.ColumnsCount)
        {
            grid.GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.GridLayoutGroup.constraintCount = grid.RowsCount;
        }
        else
        {
            grid.GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            grid.GridLayoutGroup.constraintCount = grid.ColumnsCount;
        }

        //Строим новую сетку
        for (int i = 0; i < grid.RowsCount; i++)
        {
            for (int j = 0; j < grid.ColumnsCount; j++)
            {
                UICell newCell = Instantiate(grid.CellPrefab, grid.GridLayoutGroup.transform).GetComponent<UICell>();
                newCell.YCoordinate = i;
                newCell.XCoordinate = j;
            }
        }
    }
}
