using System.Linq;
using CryoDI;
using UnityEngine;

public class ErrorMessageController : IErrorMessageController
{
    [Dependency] private IErrorMessageFactory ErrorMessageFactory { get; set; }
    
    private ErrorPanel _errorPanel;
    private ErrorPanel ErrorPanel
    {
        get
        {
            if (_errorPanel == null)
                _errorPanel = Resources.FindObjectsOfTypeAll<ErrorPanel>().FirstOrDefault();

            return _errorPanel;
        }
    }

    public void ShowErrorMessage(ErrorMessageType errorMessageType)
    {
        var errorMessage = ErrorMessageFactory.CreateErrorMessage(errorMessageType);
        ErrorPanel.OpenPanel(errorMessage);
    }
}