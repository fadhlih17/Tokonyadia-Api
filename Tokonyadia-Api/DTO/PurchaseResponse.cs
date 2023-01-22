namespace Tokonyadia_Api.DTO;

public class PurchaseResponse
{
    public string Id { get; set; }
    public DateTime TransDate { get; set; }
    public string CustomerId { get; set; }
    public ICollection<PurchaseDetailResponse> PurchaseDetails { get; set; }
}