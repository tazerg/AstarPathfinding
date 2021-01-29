using UnityEngine;
using UnityEngine.UI;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Text _errorText;

    public void OpenPanel(string errorMessage)
    {
        _errorText.text = errorMessage;
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        CheckReferences();
        
        _closeButton.onClick.AddListener(ClosePanel);
    }

    private void CheckReferences()
    {
        if (_closeButton == null)
            Debug.LogError("Can't find close button");
        
        if (_errorText == null)
            Debug.LogError("Can't find error text");
    }
    
    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
