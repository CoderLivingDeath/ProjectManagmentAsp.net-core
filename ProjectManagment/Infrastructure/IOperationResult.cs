namespace ProjectManagment.Infrastructure
{
    public interface IOperationResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        List<string> Errors { get; set; }
    }

    public interface IOperationResult<T> : IOperationResult
    {
        T? Data { get; set; }
    }
}
