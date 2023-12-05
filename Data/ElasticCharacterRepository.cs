using dotnet_todo.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using System.Linq;


namespace dotnet_todo.Data
{
    public class ElasticCharacterRepository<T>() : IRepository<T> where T : class, IActor
    {
        
        private readonly ElasticsearchClient _client = new(new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
                .CertificateFingerprint("017aa2f46d794fe586f2f6b6ad267ddc555fbd377ad8137080813176ae677850")
                .Authentication(new BasicAuthentication("elastic", "Kf7b47i+TbIu5jdfeM+1"))
                .DefaultIndex("character")
                .DisableDirectStreaming());
        private readonly string _index = "character";
        public async Task<int> Add(T entity)
        {
            var response = await _client.IndexAsync(entity, _index);
            return response.IsValidResponse ? int.Parse(response.Id) : throw new Exception("Character could not be added.");
        }

        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync(_index, id);
            if (!response.IsValidResponse)
            {
                throw new Exception("Failed To Delete");
            }

        }

        public async Task<T> Get(int id)
        {
            var response = await _client.GetAsync<T>(id, idx => idx.Index(_index));
            if (response.IsValidResponse)
            {
                return response.Source;
            }
            else
            {
                throw new Exception("Could not get requested character.");
            }
        }

        public async Task<List<T>?> GetAll()
        {
            var response = await _client.SearchAsync<T>(s => s
                .Query(q => q.MatchAll())
            );
            var docs = response.Documents;
            return [.. docs];
        }

        async Task IRepository<T>.Update<U>(U entity)
        {
            var response = await _client.UpdateAsync<U,U>("character", entity.Id, c => c.Doc(entity));
        }
    }
}
