using Confluent.Kafka;
using dot_net_api_study.Consumers;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Producers;
using Spotify_Connect.Business.Interfaces.Services;
using Spotify_Connect.Business.Producers;
using Spotify_Connect.Business.Services;
using Spotify_Connector.Data.Clients;
using StackExchange.Redis;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
var kafkaConfig = new KafkaConfiguration();
builder.Configuration.Bind("Kafka", kafkaConfig);
builder.Services.AddSingleton(kafkaConfig);
var configConsumer = new ConsumerConfig
{
    GroupId = kafkaConfig.GroupId,
    BootstrapServers = kafkaConfig.BootstrapServers,
    AutoOffsetReset = AutoOffsetReset.Earliest,
    PartitionAssignmentStrategy = PartitionAssignmentStrategy.RoundRobin,
    EnableAutoCommit = false,
    EnableAutoOffsetStore = false,
    MaxPollIntervalMs = 600000,
    SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), "Plaintext")
};

var configProducer = new ProducerConfig
{
    BootstrapServers = kafkaConfig.BootstrapServers,
    ClientId = Dns.GetHostName(),
    SecurityProtocol = (SecurityProtocol)Enum.Parse(typeof(SecurityProtocol), "Plaintext"),
    Partitioner = Partitioner.ConsistentRandom
};

builder.Services.AddTransient(_ => new ConsumerBuilder<Ignore, string>(configConsumer).Build());
builder.Services.AddTransient(_ => new ProducerBuilder<Null, string>(configProducer).Build());

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
builder.Services.AddSingleton<IConnectionMultiplexer>(redis);

var redisConfig = new RedisConfiguration();
builder.Configuration.Bind("Redis", redisConfig);
builder.Services.AddSingleton(redisConfig);

builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IRedisClient, RedisClient>();

builder.Services.AddHttpClient<ISpotifyClient, SpotifyClient>();
builder.Services.AddHostedService<ArtistConsumer>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
