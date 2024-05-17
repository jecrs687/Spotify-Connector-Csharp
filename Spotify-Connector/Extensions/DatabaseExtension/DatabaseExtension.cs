using Microsoft.EntityFrameworkCore;
using Spotify_Connect.Business.Interfaces.Clients;
using Spotify_Connector.Data.Clients;
using Spotify_Connector.Data.Context;
using StackExchange.Redis;

namespace Spotify_Connector.Extensions.DatabaseExtension
{
	public static class DatabaseExtension
	{
		 public static IServiceCollection AddSqliteExtension(this IServiceCollection service, IConfiguration configuration )
		{
			service.AddDbContext<ApplicationDbContext>(x => x.UseSqlite(configuration["Database:ConnectionString"].ToString()));
			return service;
		}
		public static IServiceCollection AddRedis(this IServiceCollection service)
		{

			service.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect("localhost"));
			service.AddScoped<IRedisClient, RedisClient>();
			return service;
		}
	}
}
