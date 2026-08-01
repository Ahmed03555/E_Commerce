using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.Services;
using Microsoft.Extensions.Options;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Payments
{
    internal class StripPaymentGateway : IPaymentGateway
    {
        private readonly PaymentIntentService paymentIntentService = new();
        public StripPaymentGateway(IOptions<PaymentGatewaySettings> options)
        {
            StripeConfiguration.ApiKey = options.Value.SecretKey;
        }
        public async Task<PaymentIntentResult> createPaymentIntentAsync(decimal amount, string currency, CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount), // Stripe expects amount in cents
                Currency = currency,
                PaymentMethodTypes =[ "card" ],
            };
         var intent = await paymentIntentService.CreateAsync(options, cancellationToken:cancellationToken);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }

        public async Task<PaymentIntentResult> updatePaymentIntentAsync(string paymentIntentId, decimal amount, CancellationToken cancellationToken = default)
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)(amount)
            };
            var intent = await paymentIntentService.UpdateAsync(paymentIntentId, options, cancellationToken: cancellationToken);
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);
        }
    }
}
