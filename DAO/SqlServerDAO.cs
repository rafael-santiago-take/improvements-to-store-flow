using ImprovementStoreFlows.Model;
using System.Data.SqlClient;

namespace ImprovementStoreFlows.DAO
{
    public class SqlServerDAO : IDAO
    {
        private readonly string _tableName = "Flows";

        private readonly SqlConnection _connection;
        public SqlServerDAO(string tableName)
        {
            _connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=Builder;Trusted_Connection=True");
            _connection.Open();
            _tableName = tableName;
        }
        public Task DeleteAsync(Guid id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"DELETE FROM {_tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("Id", id.ToString());
            command.ExecuteNonQuery();
            return Task.CompletedTask;
        }

        public async Task<FlowIdentity?> GetAsync(Guid id)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"SELECT Id, Flow FROM {_tableName} WHERE Id = @Id";
            command.Parameters.AddWithValue("Id", id.ToString());
            var reader = await command.ExecuteReaderAsync();
            try
            {
                if (reader != null) {
                    string identifier = reader["Id"].ToString() ?? "";
                    string flow = reader["Flow"].ToString() ?? "";
                    return new FlowIdentity()
                    {
                        Id = Guid.Parse(identifier),
                        Flow = flow
                    };
                }

                return null;
            }
            finally
            {
                reader.Close();
            }
        }

        public Task InsertAsync(FlowIdentity flow)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"INSERT INTO {_tableName} (Id, Flow) values(@Id, @Flow)";
            command.Parameters.AddWithValue("Id", flow.Id.ToString());
            command.Parameters.AddWithValue("Flow", flow.Flow);
            command.ExecuteNonQuery();
            return Task.CompletedTask;
        }

        public Task UpdateAsync(FlowIdentity flow)
        {
            var command = _connection.CreateCommand();
            command.CommandText = $"UPDATE {_tableName} SET Flow = @Flow WHERE Id = @Id";
            command.Parameters.AddWithValue("Id", flow.Id.ToString());
            command.Parameters.AddWithValue("Flow", flow.Flow);
            command.ExecuteNonQuery();
            return Task.CompletedTask;
        }
    }
}
