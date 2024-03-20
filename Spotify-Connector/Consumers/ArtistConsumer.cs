
using Confluent.Kafka;
using Newtonsoft.Json;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Services;
using Spotify_Connect.Business.Services;

namespace dot_net_api_study.Consumers
{
    public class ArtistConsumer : BackgroundService, IDisposable
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly KafkaConfiguration _kafkaConfiguration;
		private readonly IArtistService _artistService;


		public ArtistConsumer(IConsumer<Ignore, string> consumer, KafkaConfiguration kafkaConfiguration, IArtistService artistService)
		{
			_consumer = consumer;
			_kafkaConfiguration = kafkaConfiguration;
			_artistService = artistService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            try
            {
                _consumer.Subscribe(_kafkaConfiguration.Topics.Artist);
                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(stoppingToken);
                    if (result is null || result.IsPartitionEOF || stoppingToken.IsCancellationRequested)
                        continue;
                    var message = result.Message.Value;
                    var response = JsonConvert.DeserializeObject<string>(message);
                    if (response is not null) await _artistService.GetArtist(response);
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
