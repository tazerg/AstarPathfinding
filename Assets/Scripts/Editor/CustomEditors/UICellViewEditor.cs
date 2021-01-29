using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(UICellView))]
public class UICellViewEditor : Editor
{
    private UICellView _cell;
    private Image _image;
    private IColorDatabase _colorDatabase;
    private StaticCellType _prevCellType;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TryInjectData();

        TryChangeColor();
    }

    private void TryInjectData()
    {
        if (_cell != null)
            return;
        
        _cell = target as UICellView;
        _prevCellType = _cell.StaticCellType;
        _image = _cell.GetComponent<Image>();
        _colorDatabase = ColorDatabaseLoader.LoadDatabase();
    }

    private void TryChangeColor()
    {
        if (_cell.StaticCellType == _prevCellType)
            return;

        _prevCellType = _cell.StaticCellType;
        var cellTypeColor = _colorDatabase.GetColorForCell(_prevCellType);
        _image.color = cellTypeColor;
    }
}
