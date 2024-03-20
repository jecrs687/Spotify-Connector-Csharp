using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Interfaces.Producers
{
    public interface IKafkaProducer
    {
        Task SendMessage<T>(T message, string topic);
    }
}
