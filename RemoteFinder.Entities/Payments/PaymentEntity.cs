using RemoteFinder.Entities.Authentication;
using RemoteFinder.Entities.Constants;

namespace RemoteFinder.Entities.Payments;

public class PaymentEntity
{
    public int Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public string Currency { get; set; }
    
    public string OrderId { get; set; }
    
    public PaymentStatus Status { get; set; }
    
    public DateTime CreatedOn { get; set; }
    
    public UserSocialEntity UserSocial { get; set; }
    public int UserSocialId { get; set; }
}