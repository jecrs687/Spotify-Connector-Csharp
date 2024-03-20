using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Configuration
{
	public class RedisConfiguration
	{
		public int Database { get; set; }
		public int TTL { get; set; }
	}
}
