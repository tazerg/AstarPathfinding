using UnityEngine;

public interface IColorDatabase
{
    Color GetColorForCell(StaticCellType staticCellType);

    Color GetColorForCell(DynamicCellType dynamicCellType);
}