using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    static ColorManager instance;
    public static ColorManager Instance 
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ColorManager>();

            return instance;
        }
    }

    [SerializeField]
    Color floorColor;
    public Color FloorColor => floorColor;
    [SerializeField]
    Color wallCollor;
    public Color WallCollor => wallCollor;
    [SerializeField]
    Color startPathColor;
    public Color StartPathColor => startPathColor;
    [SerializeField]
    Color endPathColor;
    public Color EndPathColor => endPathColor;
    [SerializeField]
    Color pathColor;
    public Color PathColor => pathColor;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }
}
