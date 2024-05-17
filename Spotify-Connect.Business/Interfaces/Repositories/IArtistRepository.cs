using Spotify_Connect.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Interfaces.Repositories
{
	public interface IArtistRepository
	{
		Task<Artist> GetAsync(string id);
		Task SaveArtist(Artist artist);

	}
}
