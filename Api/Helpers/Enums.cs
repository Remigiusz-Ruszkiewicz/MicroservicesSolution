namespace Api.Helpers;

public class Enums
{
    public enum OperationResult
    {
        Ok = 200,
        Created = 201,
        BadRequest = 400,
        NotFound = 404,
        Error = 500,
        DbConnectionFailed = 503
    }
}