using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace SampleBank.Infrastructure
{
    public class JsonRepository<T> : IRepository<T>
    {
        private readonly string _dataFolder;

        public JsonRepository(IOptions<RepositoryOptions> repositoryOptions)
        {
            _dataFolder = Path.Combine(repositoryOptions.Value.BaseDataFolder, typeof(T).Name);
            Directory.CreateDirectory(_dataFolder);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var path = GetPathById(id);
            if (!File.Exists(path)) return default;

            using (var reader = new StreamReader(path))
            {
                var jsonContent = await reader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<T>(jsonContent);
            }
        }

        public async Task SaveAsync(T entity, string id)
        {
            var path = GetPathById(id);

            using (var writer = new StreamWriter(path))
            {
                var jsonContent = JsonConvert.SerializeObject(entity);

                await writer.WriteAsync(jsonContent);
            }
        }

        private string GetPathById(string id) => Path.Combine(_dataFolder, $"{id}.json");
    }
}