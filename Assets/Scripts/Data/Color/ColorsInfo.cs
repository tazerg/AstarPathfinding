using UnityEngine;

[CreateAssetMenu(fileName = "ColorsInfo", menuName = "Pathfinding/Data/Color", order = 0)]
public class ColorsInfo : ScriptableObject
{
    [SerializeField] private StaticCellColor[] _staticCellColors;
    [SerializeField] private DynamicCellColor[] _dynamicCellColors;
    
    public StaticCellColor[] StaticCellColors => _staticCellColors;
    public DynamicCellColor[] DynamicCellColors => _dynamicCellColors;
}