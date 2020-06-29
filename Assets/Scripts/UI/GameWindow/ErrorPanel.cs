using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField]
    Text errorText;
    public Text ErrorText => errorText;

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
