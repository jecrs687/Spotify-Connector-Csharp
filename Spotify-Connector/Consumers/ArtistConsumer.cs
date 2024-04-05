
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Services;
using Spotify_Connect.Business.Services;

namespace dot_net_api_study.Consumers
{
    public class ArtistConsumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly KafkaConfiguration _kafkaConfiguration;
        private readonly IServiceScopeFactory _serviceScopeFactory; 


		public ArtistConsumer(IConsumer<Ignore, string> consumer, KafkaConfiguration kafkaConfiguration,
            IServiceScopeFactory serviceScopeFactory
            )
		{
			_consumer = consumer;
			_kafkaConfiguration = kafkaConfiguration;
            _serviceScopeFactory = serviceScopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            try
            {
				using IServiceScope scope = _serviceScopeFactory.CreateScope();


				var artistService = scope
			    .ServiceProvider
		        .GetRequiredService<IArtistService>();
                _consumer.Subscribe(_kafkaConfiguration.Topics.Artist);
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    if (result is null || result.IsPartitionEOF || stoppingToken.IsCancellationRequested)
                        continue;
                    var message = result.Message.Value;
                    var response = JsonConvert.DeserializeObject<string>(message);
                    if (response is not null) await artistService.GetArtist(response);
                    _consumer.Commit(result);
                    _consumer.StoreOffset(result);
                }
            }
            catch (Exception)
            {
                _consumer.Close();
                _consumer.Dispose();
            }
        }
    }
}
