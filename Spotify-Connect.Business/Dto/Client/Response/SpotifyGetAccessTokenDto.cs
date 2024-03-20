using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Dto.Client.Response
{
    public class SpotifyGetAccessTokenDto
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }
        [JsonProperty("token_type")]
		public string token_type { get; set; }
        [JsonProperty("expires_in")]
	    public int expires_in { get; set; }

    }
}
