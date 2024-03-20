using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Producers;
using Spotify_Connect.Business.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Services
{
    public class ArtistService:IArtistService
    {
        private readonly IKafkaProducer _kafkaProducer;
        private readonly KafkaConfiguration _kafkaConfiguration;
		private readonly ISpotifyClient _spotifyClient;

		public ArtistService(IKafkaProducer kafkaProducer, KafkaConfiguration kafkaConfiguration, ISpotifyClient spotifyClient)
		{
			_kafkaProducer = kafkaProducer;
			_kafkaConfiguration = kafkaConfiguration;
			_spotifyClient = spotifyClient;
		}

		public async Task PostArtist(string artistId)
        {
           await _kafkaProducer.SendMessage(artistId, _kafkaConfiguration.Topics.Artist);
        }
		public async Task<SpotifyGetArtistResponseDto> GetArtist(string artistId)
		{
			var artist  = await _spotifyClient.GetArtist(artistId);
			return artist;
		}


	}
}
