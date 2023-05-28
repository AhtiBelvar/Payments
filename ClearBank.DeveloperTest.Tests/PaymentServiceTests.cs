using System;
using ClearBank.DeveloperTest.Accounts.Storage;
using ClearBank.DeveloperTest.Payments;
using ClearBank.DeveloperTest.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace ClearBank.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void GivenAccountStoreIsNull_WhenMakingPayment_ThenPaymentFails()
    {
        var service = new PaymentServiceBuilder()
            .WithAccountStore(null)
            .Build();
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = Guid.NewGuid().ToString()
        };

        var result = service.MakePayment(request);
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }

    [Fact]
    public void GivenNonExistentAccount_WhenMakingPayment_ThenPaymentFails()
    {
        var service = new PaymentServiceBuilder().Build();
        var request = new MakePaymentRequest
        {
            DebtorAccountNumber = Guid.NewGuid().ToString()
        };

        var result = service.MakePayment(request);
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
    }

    // public class BacsPayments
    // {
    //     [Fact]
    //     public void WhenAbleToMakePayment_ThenPaymentSucceeds()
    //     {
    //         var paymentService = BuildPaymentsService();

    //         throw new NotImplementedException();
    //     }

    //     [Fact]
    //     public void WhenPaymentSchemeNotAllowed_ThenPaymentFails()
    //     {
    //         var paymentService = BuildPaymentsService();

    //         throw new NotImplementedException();
    //     }
    // }

    // Faster Payments
    // WhenAccountHasEnoughFunds_ThenPaymentSucceeds
    // WhenPaymentSchemeNotAllowed_ThenPaymentFails
    // WhenAccountUnderFunded_ThenPaymentFails

    // Chaps Payments
    // WhenAccountIsLive_ThenPaymentSucceeds
    // WhenPaymentSchemeNotAllowed_ThenPaymentFails
    // WhenAccountIsNotLive_ThenPaymentFails
}