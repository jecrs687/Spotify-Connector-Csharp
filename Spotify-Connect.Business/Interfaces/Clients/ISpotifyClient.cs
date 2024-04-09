using Spotify_Connect.Business.Dto.Client.Response;

namespace Spotify_Connect.Business.Interfaces.Clients
{
    public interface ISpotifyClient
    {
        Task<SpotifyGetArtistResponseDto> GetArtist(string id);
    }
}
