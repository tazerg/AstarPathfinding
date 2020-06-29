using UnityEngine;

public abstract class Cell : MonoBehaviour
{
    public abstract CellType CellType { get; }

    public abstract int YCoordinate { get; set; }
    public abstract int XCoordinate { get; set; }

    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost => GCost + HCost;
    public Cell ParentCell { get; set; }

    public bool IsStartPathCell { get; protected set; }
    public bool IsEndPathCell { get; protected set; }

    public abstract void SetAsPathCell();
}
