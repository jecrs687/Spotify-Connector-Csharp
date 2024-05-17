using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Models
{
	public class Artist: Entity
	{
		public int Id { get; set; }
		public string ArtistCode { get; set; }
		public string Name { get; set; }
		public string Uri { get; set; }

	}
}
