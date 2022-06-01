using ImprovementStoreFlows.Model;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace ImprovementStoreFlows.DAO
{
    public class MongoDbDAO : IDAO
    {
        private readonly IMongoCollection<BsonFlowIdentity> _collection;

        public MongoDbDAO()
        {
            IMongoClient client = new MongoClient("mongodb://root:santiago@localhost:27017/");
            IMongoDatabase database = client.GetDatabase("builder");
            _collection = database.GetCollection<BsonFlowIdentity>("flows");
        }

        public Task DeleteAsync(Guid id)
        {
            _collection.DeleteOne(CreateFilterById(id));

            return Task.CompletedTask;
        }

        public async Task<FlowIdentity?> GetAsync(Guid id)
        {
            var filter = CreateFilterById(id);                

            var flow = (await _collection.FindAsync(filter)).FirstOrDefault();

            return flow?.ToFlowIdentity();
        }

        public Task InsertAsync(FlowIdentity flow) => _collection.InsertOneAsync(BsonFlowIdentity.Create(flow));

        public Task UpdateAsync(FlowIdentity flow)
        {
            _collection.ReplaceOne(CreateFilterById(flow.Id), BsonFlowIdentity.Create(flow));

            return Task.CompletedTask;
        }
        
        private Expression<Func<BsonFlowIdentity, bool>>  CreateFilterById(Guid id) => x => x.Id.Equals(id);
    }
}
