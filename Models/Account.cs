namespace case_study_02.Models
{
    public class Account
    {
        public string? AccountNumber { get; set; }
        public float AccountBalance { get; set; }
        public List<Transaction> AccountTransactions { get; set; } = null!;

    }
}


