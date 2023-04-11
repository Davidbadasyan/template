using Orders.Application.Dtos;

namespace Orders.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrderQueryService _orderQueryService;
    private readonly ILookupQueryService _lookupQueryService;

    public OrdersController(
        IMediator mediator,
        IOrderQueryService orderQueryService,
        ILookupQueryService lookupQueryService)
    {
        _mediator = mediator;
        _orderQueryService = orderQueryService;
        _lookupQueryService = lookupQueryService;
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] OrderRequestDto orderRequest)
    {
        var command = new CreateOrderCommand(orderRequest);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [Route("{id:long}")]
    [HttpPut]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] long id,
        [FromBody] OrderRequestDto orderRequest)
    {
        var command = new UpdateOrderCommand(id, orderRequest);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [Route("{id:long}")]
    [HttpGet]
    public async Task<ActionResult> GetByIdAsync([FromRoute] long id)
    {
        var order = await _orderQueryService.GetByIdAsync(id);

        return Ok(order);
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult> GetPaginatedAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var paginatedOrders = await _orderQueryService.GetPaginatedAsync(pageNumber, pageSize);

        return Ok(paginatedOrders);
    }

    #region lookups
    [Route("lookups/paymentMethods")]
    [HttpGet]
    public ActionResult GetPaymentMethods()
    {
        var paymentMethods = _lookupQueryService.PaymentMethods;

        return Ok(paymentMethods);
    }

    [Route("lookups/shippingMethods")]
    [HttpGet]
    public ActionResult GetShippingMethods()
    {
        var shippingMethods = _lookupQueryService.ShippingMethods;

        return Ok(shippingMethods);
    }

    [Route("lookups/weightUnits")]
    [HttpGet]
    public ActionResult GetWeightUnits()
    {
        var weightUnits = _lookupQueryService.WeightUnits;

        return Ok(weightUnits);
    }
    #endregion
}