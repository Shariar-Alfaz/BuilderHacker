namespace BuilderHacker.Tests.Models.ServiceFactoryModel
{
    public interface IPaymentService
    {
        public int Pay(decimal amount);
    }
    public class FakePaymentServiceOne : IPaymentService
    {
        public int Pay(decimal amount)
        {
            return (int)amount;
        }
    }

    public class FakePaymentServiceTwo : IPaymentService
    {
        public int Pay(decimal amount)
        {
            return (int)(amount * 2);
        }
    }
}
