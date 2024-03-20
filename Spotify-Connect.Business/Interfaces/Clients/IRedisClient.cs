using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Interfaces.Clients
{
	public interface IRedisClient
	{
		Task<bool> Set(string key, object value, TimeSpan expiry);
		Task<T> Get<T>(string key);

	}
}
