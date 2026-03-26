namespace ProjectManagment.Infrastructure
{
    public struct OperationResult : IOperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public OperationResult(bool success, string message, List<string> errors)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        public static OperationResult Ok(string message = "")
        {
            return new OperationResult { Success = true, Message = message, Errors = new List<string>() };
        }

        public static OperationResult Fail(string message, List<string>? errors = null)
        {
            return new OperationResult { Success = false, Message = message, Errors = errors ?? new List<string>() };
        }

        public static OperationResult FromException(Exception ex, string? message = null)
        {
            var errors = new List<string>();
            var current = ex;
            while (current != null)
            {
                errors.Add(current.Message);
                current = current.InnerException;
            }
            return new OperationResult { Success = false, Message = message ?? ex.Message, Errors = errors };
        }
    }

    public struct OperationResult<T> : IOperationResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T? Data { get; set; }

        public OperationResult(bool success, string message, List<string> errors, T? data)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Data = data;
        }

        public static OperationResult<T> Ok(T data, string message = "")
        {
            return new OperationResult<T> { Success = true, Message = message, Errors = new List<string>(), Data = data };
        }

        public static OperationResult<T> Fail(string message, List<string>? errors = null)
        {
            return new OperationResult<T> { Success = false, Message = message, Errors = errors ?? new List<string>() };
        }

        public static OperationResult<T> FromException(Exception ex, string? message = null)
        {
            var errors = new List<string>();
            var current = ex;
            while (current != null)
            {
                errors.Add(current.Message);
                current = current.InnerException;
            }
            return new OperationResult<T> { Success = false, Message = message ?? ex.Message, Errors = errors };
        }
    }
}
