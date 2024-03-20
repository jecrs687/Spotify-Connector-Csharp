using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Services
{
    public class SpotifyService : ISpotifyService
    {
        private readonly ISpotifyClient _client;
        private readonly IRedisClient _redisClient;
        private readonly RedisConfiguration _redisConfiguration;
        private readonly string _cacheKey = "Spotify-Connector:AccessToken";

        public SpotifyService(ISpotifyClient client, IRedisClient redisClient, RedisConfiguration redisConfiguration)
        {
            _client = client;
            _redisClient = redisClient;
            _redisConfiguration = redisConfiguration;
        }

        public async Task<SpotifyGetAccessTokenDto> GetAccessToken()
        {
            var cache = await _redisClient.Get<SpotifyGetAccessTokenDto>(_cacheKey);
            if (cache != null) return cache;
            var result = await _client.GetAccessToken();
            await _redisClient.Set(_cacheKey, result, TimeSpan.FromSeconds(_redisConfiguration.TTL));
            return result;
        }
    }
}
