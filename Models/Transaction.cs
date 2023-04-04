namespace casestudy02.Models
{
    public class Transaction
    {
        public string Date { get; set; } = null!;
        public string Description { get; set; } = null!;
        public float Amount { get; set; }
    }
}