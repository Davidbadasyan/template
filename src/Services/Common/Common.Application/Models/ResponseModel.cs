namespace Common.Application.Models;
public record ResponseModel
{
    public ResponseType ResponseType { get; protected set; }
    public string? Message { get; protected set; }

    public static ResponseModel Create(ResponseType responseType, string message)
    {
        return new ResponseModel
        {
            ResponseType = responseType,
            Message = message
        };
    }
}

public record ResponseModel<TData> : ResponseModel where TData : IResponseDto
{
    public TData Data { get; init; }

    public static ResponseModel<TData> Create(ResponseType responseType, TData data = default, string message = null)
    {
        return new ResponseModel<TData>
        {
            ResponseType = responseType,
            Message = message,
            Data = data
        };
    }
}

public interface IResponseDto { }
public interface IRequestDto { }
