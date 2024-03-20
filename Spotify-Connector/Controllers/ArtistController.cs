using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Services;

namespace Spotify_Connector.Controllers
{
	[Route("api/")]
	[ApiController]
	public class ArtistController : ControllerBase
	{

		private readonly IArtistService _artistService;

		public ArtistController( IArtistService artistService)
		{
			_artistService = artistService;
		}

		[HttpPost("{id}")]
		public async Task PostRequest([FromRoute] String id)
		{

			await _artistService.PostArtist(id);
		}
	}
}
