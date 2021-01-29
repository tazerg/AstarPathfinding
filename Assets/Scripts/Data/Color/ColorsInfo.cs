using UnityEngine;

[CreateAssetMenu(fileName = "ColorsInfo", menuName = "Pathfinding/Data/Color", order = 0)]
public class ColorsInfo : ScriptableObject
{
    [SerializeField] private Color _floorColor;
    [SerializeField] private Color _wallColor;
    [SerializeField] private Color _startPathColor;
    [SerializeField] private Color _endPathColor;
    [SerializeField] private Color _partOfPathColor;
    
    public Color FloorColor => _floorColor;
    public Color WallColor => _wallColor;
    public Color StartPathColor => _startPathColor;
    public Color EndPathColor => _endPathColor;
    public Color PartOfPathColor => _partOfPathColor;
}