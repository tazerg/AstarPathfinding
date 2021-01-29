using CryoDI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class UICellView : CryoBehaviour, IPointerClickHandler
{
    [Dependency] private IColorDatabase ColorDatabase { get; set; }

    [SerializeField] private StaticCellType _staticCellType;
    [SerializeField, Readonly] private int _x;
    [SerializeField, Readonly] private int _y;
    
    private Image _image;
    private Cell _cell;

    private DynamicCellType DynamicCellType
    {
        get => _cell.DynamicCellType;
        set
        {
            _cell.DynamicCellType = value;
            SetImageColor(value);
        }
    }

    public StaticCellType StaticCellType => _staticCellType;
    public ICell Cell => _cell;
    public int X
    {
        get => _x;
        set => _x = value;
    }
    public int Y
    {
        get => _y;
        set => _y = value;
    }

    public delegate void PathBoundChangedHandler(UICellView cellView);
    public static event PathBoundChangedHandler StartPointChangedEvent;
    public static event PathBoundChangedHandler EndPointChangedEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_staticCellType == StaticCellType.Wall)
            return;

        if (TrySetStartCellType(eventData))
            return;
        
        if (TrySetEndCellType(eventData))
            return;
    }

    protected override void Awake()
    {
        base.Awake();

        _image = GetComponent<Image>();
        _cell = new Cell(_staticCellType, DynamicCellType.None, _x, _y);
        
        StartPointChangedEvent += OnStartPointChanged;
        EndPointChangedEvent += OnEndPointChanged;
        _cell.PartOfPathMarked += OnPartOfPathMarked;
    }

    private void OnDestroy()
    {
        StartPointChangedEvent -= OnStartPointChanged;
        EndPointChangedEvent -= OnEndPointChanged;
        _cell.PartOfPathMarked -= OnPartOfPathMarked;
    }

    private void OnStartPointChanged(UICellView cellView)
    {
        if (!ReferenceEquals(cellView, this))
        {
            TryDropDynamicCellType(DynamicCellType.EndPath);
            return;
        }
        
        if (!CanOverrideStaticCellType())
            return;

        DynamicCellType = DynamicCellType.StartPath;
    }

    private void OnEndPointChanged(UICellView cellView)
    {
        if (!ReferenceEquals(cellView, this))
        {
            TryDropDynamicCellType(DynamicCellType.StartPath);
            return;
        }
        
        if (!CanOverrideStaticCellType())
            return;

        DynamicCellType = DynamicCellType.EndPath;
    }

    private void OnPartOfPathMarked()
    {
        SetImageColor(DynamicCellType.PartOfPath);
    }

    private void SetImageColor(DynamicCellType dynamicCellType)
    {
        var cellTypeColor = ColorDatabase.GetColorForCell(dynamicCellType);
        _image.color = cellTypeColor;
    }

    private void TryDropDynamicCellType(DynamicCellType excludeCellType)
    {
        if (!CanOverrideStaticCellType())
            return;
        
        if (DynamicCellType == excludeCellType)
            return;

        DynamicCellType = DynamicCellType.None;
    }

    private bool TrySetStartCellType(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) 
            return false;
        
        StartPointChangedEvent?.Invoke(this);
        return true;
    }

    private bool TrySetEndCellType(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right)
            return false;

        EndPointChangedEvent?.Invoke(this);
        return true;
    }

    private bool CanOverrideStaticCellType()
    {
        return _staticCellType != StaticCellType.Wall;
    }
}
