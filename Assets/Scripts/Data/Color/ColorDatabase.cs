using System;
using UnityEngine;

public class ColorDatabase : IColorDatabase
{
    private readonly Color _floorColor;
    private readonly Color _wallColor;
    private readonly Color _startPathColor;
    private readonly Color _endPathColor;
    private readonly Color _partOfPathColor;

    public ColorDatabase(ColorsInfo colorsInfo)
    {
        _floorColor = colorsInfo.FloorColor;
        _wallColor = colorsInfo.WallColor;
        _startPathColor = colorsInfo.StartPathColor;
        _endPathColor = colorsInfo.EndPathColor;
        _partOfPathColor = colorsInfo.PartOfPathColor;
    }
    
    public Color GetColorForCell(StaticCellType staticCellType)
    {
        switch (staticCellType)
        {
            case StaticCellType.Floor:
                return _floorColor;
            case StaticCellType.Wall:
                return _wallColor;
            default:
                throw new ArgumentOutOfRangeException($"Not suported static cell type {staticCellType}");
        }
    }

    public Color GetColorForCell(DynamicCellType dynamicCellType)
    {
        switch (dynamicCellType)
        {
            case DynamicCellType.None:
                return _floorColor;
            case DynamicCellType.StartPath:
                return _startPathColor;
            case DynamicCellType.EndPath:
                return _endPathColor;
            case DynamicCellType.PartOfPath:
                return _partOfPathColor;
            default:
                throw new ArgumentOutOfRangeException($"Not suported dynamic cell type {dynamicCellType}");
        }
    }
}
