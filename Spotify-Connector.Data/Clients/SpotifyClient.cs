using Flurl.Http;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Clients;

namespace Spotify_Connector.Data.Clients
{
    public class SpotifyClient : ISpotifyClient
    {
        private readonly IFlurlClient _flurlClient;

        public SpotifyClient(HttpClient httpClient)
        {
            if (httpClient is null)
                throw new ArgumentNullException(nameof(httpClient));
            _flurlClient = new FlurlClient(httpClient);
        }

        public async Task<SpotifyGetArtistResponseDto> GetArtist(string artistId)
        {
            var accessToken = await GetAccessToken();

            var result = await $"https://api.spotify.com/v1/artists/{artistId}"
                .WithHeader("Content-type", "application/x-www-form-urlencoded")
                .WithOAuthBearerToken(accessToken.access_token)
                .AllowAnyHttpStatus()
                .GetAsync();

            if (result.StatusCode != 200) return null;

            return await result.GetJsonAsync<SpotifyGetArtistResponseDto>();
        }

        private async Task<SpotifyGetAccessTokenDto> GetAccessToken()
        {

            var clientId = "3f7bbadf05cb495481896d0cbc44aa5f";
            var secretId = "7fed47a7bb2640999651b2879c26e886";

            var result = await "https://accounts.spotify.com/api/token"
                .WithHeader("Content-type", "application/x-www-form-urlencoded")
                .PostUrlEncodedAsync(
                    new
                    {
                        grant_type = "client_credentials",
                        client_id = clientId,
                        client_secret = secretId
                    });

            if (result.StatusCode != 200) return null;

            return await result.GetJsonAsync<SpotifyGetAccessTokenDto>();
        }
    }
}