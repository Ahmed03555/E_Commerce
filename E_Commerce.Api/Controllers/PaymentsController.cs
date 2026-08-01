using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;

namespace E_Commerce.Api.Controllers
{

    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentServices _paymentService;
        private readonly PaymentGatewaySettings _options;

        public PaymentsController(IPaymentServices paymentService, IOptions<PaymentGatewaySettings> options)
        {
            _paymentService = paymentService;
            _options = options.Value;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymenyIntent(string basketId, CancellationToken ct)
        {
            var result = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId, ct);
            return ToActionResult(result);
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"],
                    _options.WebhookSecret);

                switch (stripeEvent.Type)
                {
                    case EventTypes.PaymentIntentSucceeded:
                        var succededPayment = stripeEvent.Data.Object as PaymentIntent;

                        if (succededPayment is not null)
                            await _paymentService.PaymentSucceeded(succededPayment.Id);

                      break;
                    case EventTypes.PaymentIntentPaymentFailed:
                    var failedPayment = stripeEvent.Data.Object as PaymentIntent;

                    if (failedPayment is not null)
                        await _paymentService.PaymentFailed(failedPayment.Id);

                    break;
                }

                return Ok();
            }
            catch (StripeException ex)
            {
                return BadRequest(ex.Message);
            }
        } 
       } 
    }  

