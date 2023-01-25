namespace Tokonyadia_Api.DTO;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;// hanya untuk menghilangkan warning
}