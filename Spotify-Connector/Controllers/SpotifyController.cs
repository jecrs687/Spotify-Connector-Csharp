using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Services;

namespace Spotify_Connector.Controllers
{
	[Route("api/")]
	[ApiController]
	public class SpotifyController : ControllerBase
	{

		private readonly ISpotifyService _spotifyService;

		public SpotifyController(ISpotifyService spotifyService)
		{
			_spotifyService = spotifyService;
		}
		[HttpPost("")]
		public async Task<SpotifyGetAccessTokenDto> PostRequest(SpotifyGetAccessTokenDto request)
		{
			return await _spotifyService.GetAccessToken();
		}
	}
}
