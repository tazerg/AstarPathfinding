using System.Collections.Generic;
using UnityEngine;

public class ColorDatabase : IColorDatabase
{
    private readonly Dictionary<StaticCellType, Color> _staticCellColors = new Dictionary<StaticCellType, Color>();
    private readonly Dictionary<DynamicCellType, Color> _dynamicCellColors = new Dictionary<DynamicCellType, Color>();

    public ColorDatabase(ColorsInfo colorsInfo)
    {
        TryAddStaticCellColors(colorsInfo.StaticCellColors);
        TryAddDynamicCellColors(colorsInfo.DynamicCellColors);
    }
    
    public Color GetColorForCell(StaticCellType staticCellType)
    {
        if (!_staticCellColors.TryGetValue(staticCellType, out var cellColor))
        {
            Debug.LogError($"Can't find color for {staticCellType}");
            return Color.white;
        }

        return cellColor;
    }

    public Color GetColorForCell(DynamicCellType dynamicCellType)
    {
        if (!_dynamicCellColors.TryGetValue(dynamicCellType, out var cellColor))
        {
            Debug.LogError($"Can't find color for {dynamicCellType}");
            return Color.white;
        }

        return cellColor;
    }

    private void TryAddStaticCellColors(StaticCellColor[] staticCellColors)
    {
        foreach (var staticCellColor in staticCellColors)
        {
            var staticCellType = staticCellColor.StaticCellType;
            if (_staticCellColors.ContainsKey(staticCellType))
            {
                Debug.LogError($"_staticCellColors already exist color for {staticCellType}");
                continue;
            }
                
            _staticCellColors.Add(staticCellType, staticCellColor.Color);
        }
    }
    
    private void TryAddDynamicCellColors(DynamicCellColor[] dynamicCellColors)
    {
        foreach (var dynamicCellColor in dynamicCellColors)
        {
            var dynamicCellType = dynamicCellColor.DynamicCellType;
            if (_dynamicCellColors.ContainsKey(dynamicCellType))
            {
                Debug.LogError($"_dynamicCellColors already exist color for {dynamicCellType}");
                continue;
            }
                
            _dynamicCellColors.Add(dynamicCellType, dynamicCellColor.Color);
        }
    }
}
