using ImprovementStoreFlows.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace ImprovementStoreFlows.DAO
{
    public class BsonFlowIdentity
    {
        [BsonId()]
        public Guid Id { get; set; }

        [BsonElement("flow")]
        public BsonDocument Flow { get; set; } = new BsonDocument();

        public static BsonFlowIdentity Create(FlowIdentity flow) => new BsonFlowIdentity()
            {
                Id = flow.Id,
                Flow = BsonSerializer.Deserialize<BsonDocument>(flow.Flow)
            };

        public FlowIdentity ToFlowIdentity() => new FlowIdentity()
        {
            Id = Id,
            Flow = Flow.ToString()
        };
    }
}
