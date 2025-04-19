using FunStore.Models.Request;
using FunStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunStore.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PurchaseProcessorController : ControllerBase
{
    protected readonly IProductService _productService;
    protected readonly IPurchaseProcessorService _purchaseProcessorService;

    public PurchaseProcessorController(IProductService productService, IPurchaseProcessorService purchaseProcessorService)
    {
        _productService = productService;
        _purchaseProcessorService = purchaseProcessorService;
    }

    [HttpGet("products/memberships")]
    public async Task<IActionResult> GetMembershipProducts()
    {
        return Ok(await _productService.GetMembershipsAsync());
    }

    [Authorize(Roles = "VideoClubUser, PremiumUser")]
    [HttpGet("products/videos")]
    public async Task<IActionResult> GetVideoProducts()
    {
        return Ok(await _productService.GetVideosAsync());
    }

    [Authorize(Roles = "BookClubUser, PremiumUser")]
    [HttpGet("products/books")]
    public async Task<IActionResult> GetBookProducts()
    {
        return Ok(await _productService.GetBooksAsync());
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseOrder([FromBody] PurchaseOrderRequestModel orderRequestModel)
    {
        return Ok(await _purchaseProcessorService.PurchaseOrder(orderRequestModel.ProductIds));
    }
}