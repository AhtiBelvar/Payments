namespace ClearBank.DeveloperTest.Payments;

public interface IPaymentService
{
    MakePaymentResult MakePayment(MakePaymentRequest request);
}
