using case_study_02.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace case_study_02.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customersCollection;

        public CustomerService(
            IOptions<CustomerDatabaseSettings> customerDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                customerDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                customerDatabaseSettings.Value.DatabaseName);

            _customersCollection = mongoDatabase.GetCollection<Customer>(
                customerDatabaseSettings.Value.BankCollectionName);
        }

        public async Task<List<Customer>> GetAsync() =>
            await _customersCollection.Find(_ => true).ToListAsync();

        public async Task<Customer?> GetAsync(string id) =>
            await _customersCollection.Find(x => x.CustomerID == id).FirstOrDefaultAsync();

        public async Task CreateAsync(List<Customer> newCustomers)
        {
            foreach (var item in newCustomers)
            {
                item.CustomerPassword = SecretHasher.Hash(item.CustomerPassword);
            }
            await _customersCollection.InsertManyAsync(newCustomers);
        }


        public async Task UpdateAsync(string id, Customer updatedCustomer) =>
            await _customersCollection.ReplaceOneAsync(x => x.CustomerID == id, updatedCustomer);

        public async Task RemoveAsync(string id) =>
            await _customersCollection.DeleteOneAsync(x => x.CustomerID == id);
    }
}