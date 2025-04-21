using Microsoft.IdentityModel.Tokens;

namespace FunStore.Models.Response;

public class PurchaseOrderResponseModel(int orderId, int customerId, IList<string> items, decimal total, string membershipProductProcessingResult)
{
    public int OrderId { get; private set; } = orderId;

    public int CustomerId { get; private set; } = customerId;

    public IList<string> Items { get; private set; } = items;

    public  decimal Total { get; private set; } = total;

    public TokenReissueInfo TokenReissueInfo { get; private set; } = new TokenReissueInfo(membershipProductProcessingResult);
}

public class TokenReissueInfo(string membershipProductProcessingResult)
{
    public bool IsReissued { get; private set; } = !membershipProductProcessingResult.IsNullOrEmpty();

    public string NewAccessToken { get; private set; } = membershipProductProcessingResult;
}