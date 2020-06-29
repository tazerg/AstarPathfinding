using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWindow : MonoBehaviour
{
    [SerializeField]
    ErrorPanel errorPanel;

    void Awake()
    {
        ErrorMessageGenerator.needShowErrorEvent += OnNeedShowError;

        if (errorPanel.gameObject.activeSelf)
            errorPanel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ErrorMessageGenerator.needShowErrorEvent -= OnNeedShowError;
    }

    private void OnNeedShowError(string errorMessage)
    {
        errorPanel.ErrorText.text = errorMessage;
        errorPanel.gameObject.SetActive(true);
    }
}
