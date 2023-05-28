using System;
using ClearBank.DeveloperTest.Accounts;
using ClearBank.DeveloperTest.Accounts.Storage;
using ClearBank.DeveloperTest.Payments;
using ClearBank.DeveloperTest.Tests.Builders;
using ClearBank.DeveloperTest.Tests.Stubs;
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

    public class BacsPayments
    {
        [Fact]
        public void WhenAbleToMakePayment_ThenPaymentSucceeds()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs,
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.Bacs,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(20);
        }

        [Fact]
        public void WhenPaymentSchemeNotAllowed_ThenPaymentFails()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.Bacs,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(50);
        }
    }

    public class FasterPayments
    {
        [Fact]
        public void WhenAccountHasEnoughFunds_ThenPaymentSucceeds()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.FasterPayments,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(20);
        }

        [Fact]
        public void WhenPaymentSchemeNotAllowed_ThenPaymentFails()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.FasterPayments,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(50);
        }

        [Fact]
        public void WhenAccountUnderFunded_ThenPaymentFails()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                Balance = 10
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.FasterPayments,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(10);
        }
    }

    public class ChapsPayments
    {
        [Fact]
        public void WhenAccountIsLive_ThenPaymentSucceeds()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                Status = AccountStatus.Live,
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.Chaps,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(20);
        }

        [Fact]
        public void WhenPaymentSchemeNotAllowed_ThenPaymentFails()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                Status = AccountStatus.Live,
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.Chaps,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(50);
        }

        [Fact]
        public void WhenAccountIsNotLive_ThenPaymentFails()
        {
            var testAccount = new Account
            {
                AccountNumber = Guid.NewGuid().ToString(),
                AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                Status = AccountStatus.Disabled,
                Balance = 50
            };
            var accountStore = new StubAccountStore(testAccount);
            var paymentService = new PaymentServiceBuilder()
                .WithAccountStore(accountStore)
                .Build();

            var request = new MakePaymentRequest
            {
                DebtorAccountNumber = testAccount.AccountNumber,
                PaymentScheme = PaymentScheme.Chaps,
                Amount = 30
            };

            var result = paymentService.MakePayment(request);
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();

            var updatedAccount = accountStore.GetAccount(testAccount.AccountNumber);
            updatedAccount.Should().NotBeNull();
            updatedAccount.Balance.Should().Be(50);
        }
    }
}