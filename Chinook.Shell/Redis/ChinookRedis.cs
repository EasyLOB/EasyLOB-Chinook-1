using Chinook.Data;
using EasyLOB.Library;
using ServiceStack;
using ServiceStack.Redis;

namespace Chinook.Shell
{
    public class ChinookRedis
    {
        private IRedisClient _client;

        public IRedisClient Client { get { return _client; } }

        public ChinookRedis()
        {
            _client = new RedisClient(LibraryHelper.AppSettings<string>("Redis.Chinook"));

            ModelConfig<Genre>.Id(x => x.GenreId);
        }
    }
}