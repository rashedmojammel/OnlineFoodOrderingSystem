namespace BAL.DTOs
{
    public class CheckoutDTO
    {
        public string PaymentMethod { get; set; } = null!;
        public decimal Total { get; set; }
    }
}