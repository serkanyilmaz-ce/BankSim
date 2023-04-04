namespace casestudy02.Models
{
    public class CustomerDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BankCollectionName { get; set; } = null!;
    }
}