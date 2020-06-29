using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICell : Cell, IPointerClickHandler
{
    [SerializeField]
    CellType cellType;
    public override CellType CellType => cellType;
    [SerializeField, Readonly]
    int xCoordinate;
    public override int XCoordinate { get => xCoordinate; set => xCoordinate = value; }
    [SerializeField, Readonly]
    int yCoordinate;
    public override int YCoordinate { get => yCoordinate; set => yCoordinate = value; }
    [Header("Ссылки")]
    [SerializeField]
    Image image;

    public delegate void PathBoundChangedHandler(UICell cell);
    public static event PathBoundChangedHandler startPointChangedEvent;
    public static event PathBoundChangedHandler endPointChangedEvent;

    private void Awake()
    {
        startPointChangedEvent += OnStartPointChanged;
        endPointChangedEvent += OnEndPointChanged;
    }

    private void OnDestroy()
    {
        startPointChangedEvent -= OnStartPointChanged;
        endPointChangedEvent -= OnEndPointChanged;
    }

    private void OnValidate()
    {
        if (ColorManager.Instance == null)
            return;

        if (cellType == CellType.Wall)
            image.color = ColorManager.Instance.WallCollor;
        else
            image.color = ColorManager.Instance.FloorColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cellType == CellType.Wall)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            IsStartPathCell = true;
            IsEndPathCell = false;
            image.color = ColorManager.Instance.StartPathColor;

            startPointChangedEvent?.Invoke(this);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            IsEndPathCell = true;
            IsStartPathCell = false;
            image.color = ColorManager.Instance.EndPathColor;

            endPointChangedEvent?.Invoke(this);
        }
    }

    private void OnStartPointChanged(UICell cell)
    {
        if (cell == this || CellType == CellType.Wall || IsEndPathCell)
            return;

        if(image.color != ColorManager.Instance.FloorColor)
            image.color = ColorManager.Instance.FloorColor;

        if (IsStartPathCell)
            IsStartPathCell = false;
    }

    private void OnEndPointChanged(UICell cell)
    {
        if (cell == this || CellType == CellType.Wall || IsStartPathCell)
            return;

        if (image.color != ColorManager.Instance.FloorColor)
            image.color = ColorManager.Instance.FloorColor;

        if (IsEndPathCell)
            IsEndPathCell = false;
    }

    public override void SetAsPathCell()
    {
        image.color = ColorManager.Instance.PathColor;
    }
}
