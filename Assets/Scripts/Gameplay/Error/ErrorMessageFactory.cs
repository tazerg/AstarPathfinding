using System;

public class ErrorMessageFactory : IErrorMessageFactory
{
    private const string PathBorderNotFoundError = "Границы поиска пути не найдены!";
    private const string StartPathPointNotFoundError = "Не найдена стартовая точка!";
    private const string EndPathPointNotFoundError = "Не найдена конечная точка!";
    private const string PathNotFoundError = "Не найден путь для заданных точек!";

    public string CreateErrorMessage(ErrorMessageType errorMessageType)
    {
        switch (errorMessageType)
        {
            case ErrorMessageType.PathNotFound:
                return PathNotFoundError;
            case ErrorMessageType.PathBordersNotFound:
                return PathBorderNotFoundError;
            case ErrorMessageType.StartPathPointNotFound:
                return StartPathPointNotFoundError;
            case ErrorMessageType.EndPathPointNotFound:
                return EndPathPointNotFoundError;
            default:
                throw new ArgumentOutOfRangeException($"Not suported error message type {errorMessageType}");
        }
    }
}
