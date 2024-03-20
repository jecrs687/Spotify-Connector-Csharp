using Confluent.Kafka;
using Spotify_Connect.Business.Interfaces.Producers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Producers
{
	public class KafkaProducer : IKafkaProducer
	{
		private readonly IProducer<Null, string> _producer;
		public KafkaProducer(IProducer<Null, string> producer)
		{
			_producer = producer;
		}
		public async Task SendMessage<T>(T message, string topic)
		{
			string messageSerialized = System.Text.Json.JsonSerializer.Serialize(message);
			await _producer.ProduceAsync(topic, new Message<Null, string> {
				Value = messageSerialized
			});
			
		}
	}
}
