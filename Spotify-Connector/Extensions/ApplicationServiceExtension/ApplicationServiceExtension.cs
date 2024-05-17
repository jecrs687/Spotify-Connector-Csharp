using dot_net_api_study.Consumers;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Repositories;
using Spotify_Connect.Business.Interfaces.Services;
using Spotify_Connect.Business.Services;
using Spotify_Connector.Data.Clients;
using Spotify_Connector.Data.Repositories;

namespace Spotify_Connector.Extensions.ApplicationServiceExtension
{
	public static class ApplicationServiceExtension
	{
		public static IServiceCollection AddServicesExtension(this IServiceCollection service)
		{
			service.AddScoped<IArtistService, ArtistService>();

			return service;
		}
		public static IServiceCollection AddClientsExtension(this IServiceCollection service)
		{
			service.AddHttpClient<ISpotifyClient, SpotifyClient>();
			return service;
		}
		public static IServiceCollection AddRepositoryExtension(this IServiceCollection service)
		{
			service.AddScoped<IArtistRepository, ArtistRepository>();
			return service;
		}
		public static IServiceCollection AddBindExtension(this IServiceCollection service, IConfiguration configuration)
		{
			var redisConfig = new RedisConfiguration();

			configuration.Bind("Redis", redisConfig);
			service.AddSingleton(redisConfig);

			var kafkaConfig = new KafkaConfiguration();
			configuration.Bind("Kafka", kafkaConfig);
			service.AddSingleton(kafkaConfig);

			return service;
		}
		public static IServiceCollection AddBackgroundExtension(this IServiceCollection service)
		{
			service.AddHostedService<ArtistConsumer>();
			return service;
		}
	}
}
