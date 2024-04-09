using Microsoft.AspNetCore.Mvc;
using Spotify_Connect.Business.Interfaces.Services;

namespace Spotify_Connector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {

        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostRequest([FromRoute] string id)
        {
            await _artistService.PostArtist(id);
            return Ok();
        }
    }
}
