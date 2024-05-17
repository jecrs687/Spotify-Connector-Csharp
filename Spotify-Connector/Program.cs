using Confluent.Kafka;
using dot_net_api_study.Consumers;
using Microsoft.EntityFrameworkCore;
using Spotify_Connect.Business.Configuration;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connect.Business.Interfaces.Producers;
using Spotify_Connect.Business.Interfaces.Repositories;
using Spotify_Connect.Business.Interfaces.Services;
using Spotify_Connect.Business.Producers;
using Spotify_Connect.Business.Services;
using Spotify_Connector.Data.Clients;
using Spotify_Connector.Data.Context;
using Spotify_Connector.Data.Repositories;
using Spotify_Connector.Extensions.ApplicationServiceExtension;
using Spotify_Connector.Extensions.DatabaseExtension;
using Spotify_Connector.Extensions.MessageBrokerExtension;
using StackExchange.Redis;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddSqliteExtension(builder.Configuration)
    .AddRedis()
    .AddKafkaExtension(builder.Configuration)
    .AddServicesExtension()
    .AddClientsExtension()
    .AddRepositoryExtension()
    .AddBindExtension(builder.Configuration)
    .AddBackgroundExtension()
    .AddRedis()
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddControllers();



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
