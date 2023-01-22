namespace Tokonyadia_Api.DTO;

public class PageResponse<T>
{
    public List<T> Content { get; set; }
    public int TotalPage { get; set; }
    public int TotalElement { get; set; }
}