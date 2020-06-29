using UnityEngine;

public class ErrorMessageGenerator
{
    const string PATH_BORDER_NOT_FOUND_ERROR = "Границы поиска пути не найдены!";
    const string START_PATH_POINT_NOT_FOUND_ERROR = "Не найдена стартовая точка!";
    const string END_PATH_POINT_NOT_FOUND_ERROR = "Не найдена конечная точка!";
    const string PATH_NOT_FOUND_ERROR = "Не найден путь для заданных точек!";
    const string MESSAGE_NOT_FOUND = "Не найдено подходящее сообщение об ошибке.";

    public delegate void NeedShowErrorHandler(string errorMessage);
    public static event NeedShowErrorHandler needShowErrorEvent;

    public static void ShowErrorMessage(ErrorMessageType errorMessageType)
    {
        switch (errorMessageType)
        {
            case ErrorMessageType.EndPathPointNotFound:
                {
                    needShowErrorEvent?.Invoke(END_PATH_POINT_NOT_FOUND_ERROR);
                    break;
                }
            case ErrorMessageType.PathBordersNotFound:
                {
                    needShowErrorEvent?.Invoke(PATH_BORDER_NOT_FOUND_ERROR);
                    break;
                }
            case ErrorMessageType.PathNotFound:
                {
                    needShowErrorEvent?.Invoke(PATH_NOT_FOUND_ERROR);
                    break;
                }
            case ErrorMessageType.StartPathPointNotFound:
                {
                    needShowErrorEvent?.Invoke(START_PATH_POINT_NOT_FOUND_ERROR);
                    break;
                }
            default:
                {
                    Debug.LogError(MESSAGE_NOT_FOUND);
                    break;
                }
        }
    }
}
