namespace SamDevs.Infrastructure.Helpers
{
    public class OperationResult<TData>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public TData Data { get; set; }

        public OperationResult(string code, bool success, string message = null, TData data = default)
        {
            Code = code;
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
