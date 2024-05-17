using Confluent.Kafka;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Dto.Client.Response;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Producers;
using Spotify_Connect.Business.Interfaces.Repositories;
using Spotify_Connect.Business.Interfaces.Services;
using Spotify_Connect.Business.Models;

namespace Spotify_Connect.Business.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IKafkaProducer _kafkaProducer;
        private readonly KafkaConfiguration _kafkaConfiguration;
        private readonly ISpotifyClient _spotifyClient;
        private readonly IArtistRepository _artistRepository;


		public ArtistService(IKafkaProducer kafkaProducer, KafkaConfiguration kafkaConfiguration, ISpotifyClient spotifyClient, IArtistRepository artistRepository)
		{
			_kafkaProducer = kafkaProducer;
			_kafkaConfiguration = kafkaConfiguration;
			_spotifyClient = spotifyClient;
			_artistRepository = artistRepository;
		}

		public async Task PostArtist(string artistId)
        {
            await _kafkaProducer.SendMessage(artistId, _kafkaConfiguration.Topics.Artist);
        }

        public async Task SaveArtist(string artistId)
        {
            var artist = await _spotifyClient.GetArtist(artistId);
            if(artist is null) return;
            await _artistRepository.SaveArtist(
                new Artist { ArtistCode = artist.id, Name = artist.name,  Uri = artist.uri   }
                );
        }
        public async Task<Artist> GetArtist(string artistId)
        {
			var artist = await _artistRepository.GetAsync(artistId);
            if(artist is null) return new Artist();
            return artist;
		}
    }
}
