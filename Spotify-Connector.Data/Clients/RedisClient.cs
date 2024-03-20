using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Clients;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spotify_Connector.Data.Clients
{
	public class RedisClient:IRedisClient
	{
		private readonly IConnectionMultiplexer _connectionMultiplexer;
		private readonly RedisConfiguration _redisConfig;

		public RedisClient(IConnectionMultiplexer connection,
				RedisConfiguration redisConfig
			)
		{
			_connectionMultiplexer = connection;
			_redisConfig = redisConfig;
		}
		public async Task<T> Get<T>(string key)
		{
			var value = await _connectionMultiplexer
				.GetDatabase(_redisConfig.Database)
				.StringGetAsync(key);

			if (value.IsNull)
				return default;

			return JsonSerializer.Deserialize<T>(value);
		}

		public async Task<bool> Set(string key, object value, TimeSpan expiry)
		{
			var jsonValue = JsonSerializer.Serialize(value);
			return await _connectionMultiplexer
				.GetDatabase(_redisConfig.Database)
				.StringSetAsync(key, jsonValue, expiry);
		}
	}
}
