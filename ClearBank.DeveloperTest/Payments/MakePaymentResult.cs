namespace ClearBank.DeveloperTest.Payments;

public class MakePaymentResult
{
    public bool Success { get; set; }

    public static MakePaymentResult Succeeded()
        => new MakePaymentResult { Success = true };

    public static MakePaymentResult Failure()
        => new MakePaymentResult { Success = false };
}
