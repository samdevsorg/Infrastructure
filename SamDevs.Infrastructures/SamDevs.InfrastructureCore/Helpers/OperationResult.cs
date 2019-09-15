using SamDevs.InfrastructureCore.Enums;

namespace SamDevs.InfrastructureCore.Helpers
{
    public class OperationResult<TData>
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public MessageType MessageType { get; set; }
        public bool Success { get; set; }
        public TData Data { get; set; }

        public OperationResult(string code, bool success, string message = null, MessageType messageType = MessageType.None, TData data = default)
        {
            Code = code;
            Success = success;
            Message = message;
            MessageType = messageType;
            Data = data;
        }
    }
}
