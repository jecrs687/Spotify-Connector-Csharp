using Flurl;
using Flurl.Http;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;

namespace Spotify_Connector.Data.Clients
{
	public class SpotifyClient:ISpotifyClient
	{
		private readonly IFlurlClient _flurlClient;
		private readonly ISpotifyService _spotifyService;


		public SpotifyClient(HttpClient httpClient, ISpotifyService spotifyService = null)
		{
			if (httpClient is null)
				throw new ArgumentNullException(nameof(httpClient));
			_flurlClient = new FlurlClient(httpClient);
			_spotifyService = spotifyService;
		}


		public async Task<SpotifyGetAccessTokenDto> GetAccessToken()
		{

			String clientId = "3f7bbadf05cb495481896d0cbc44aa5f";
			String secretId = "7fed47a7bb2640999651b2879c26e886";

			var result = await "https://accounts.spotify.com/api/token"
				.WithHeader("Content-type", "application/x-www-form-urlencoded")
				.PostUrlEncodedAsync(
					new
					{
						grant_type = "client_credentials",
						client_id = clientId,
						client_secret = secretId
					});
				
			if(result.StatusCode != 200) return null;

			return await result.GetJsonAsync<SpotifyGetAccessTokenDto>(); 
		}

		public async Task<SpotifyGetArtistResponseDto> GetArtist(string artistId)
		{
			string accessToken = (await _spotifyService.GetAccessToken()).access_token;

			var result = await $"https://accounts.spotify.com/api/artists/{artistId}" 
				.WithHeader("Content-type", "application/x-www-form-urlencoded")
				.WithOAuthBearerToken(accessToken)
				.GetAsync();

			if (result.StatusCode != 200) return null;

			return await result.GetJsonAsync<SpotifyGetArtistResponseDto>();
		}
	}
}
