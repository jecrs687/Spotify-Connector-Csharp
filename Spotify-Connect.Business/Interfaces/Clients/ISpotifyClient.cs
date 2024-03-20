using Spotify_Connect.Business.Dto.Client.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Interfaces.Clients
{
	public interface ISpotifyClient
	{
		Task<SpotifyGetAccessTokenDto> GetAccessToken();
		Task<SpotifyGetArtistResponseDto> GetArtist(string id);
	}

}
