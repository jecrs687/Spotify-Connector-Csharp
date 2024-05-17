using Confluent.Kafka;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Producers;
using Spotify_Connect.Business.Producers;
using System.Net;

namespace Spotify_Connector.Extensions.MessageBrokerExtension
{
	public static class MessageBrokerExtension
	{
		public static IServiceCollection AddKafkaExtension(this IServiceCollection service, IConfiguration configuration)
		{
			string groupId = configuration.GetSection("Kafka").GetSection("GroupId").ToString();
			string bootstrapServers = configuration.GetSection("Kafka").GetSection("BootstrapServers").ToString();	
			var configConsumer = new ConsumerConfig
			{
				GroupId = groupId,
				BootstrapServers = bootstrapServers,
				AutoOffsetReset = AutoOffsetReset.Earliest,
				PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
				EnableAutoCommit = false,
				EnableAutoOffsetStore = false,
				MaxPollIntervalMs = 600000,
				SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), "Plaintext")
			};

			var configProducer = new ProducerConfig
			{
				BootstrapServers = bootstrapServers,
				ClientId = Dns.GetHostName(),
				SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), "Plaintext"),
				Partitioner = Partitioner.ConsistentRandom
			};
			service.AddTransient(_ => new ConsumerBuilder<Ignore, string>(configConsumer).Build());
			service.AddTransient(_ => new ProducerBuilder<Null, string>(configProducer).Build());
			service.AddScoped<IKafkaProducer, KafkaProducer>();

			return service;

		}
	}
}
