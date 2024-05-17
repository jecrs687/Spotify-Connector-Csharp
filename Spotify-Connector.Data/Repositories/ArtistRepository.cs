using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spotify_Connect.Business.Interfaces.Repositories;
using Spotify_Connect.Business.Models;
using Spotify_Connector.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connector.Data.Repositories
{
	public class ArtistRepository:IArtistRepository
	{
		private readonly ApplicationDbContext _context;

		public ArtistRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Artist> GetAsync(string id)
		{
			string sql = "SELECT * FROM Artist";
			var artist = await _context.Artist.FromSqlRaw(sql).SingleOrDefaultAsync();
			return artist;
		}

		public async Task SaveArtist(Artist artist)
		{
			_context.Artist.Add(artist);
			await _context.SaveChangesAsync();
		}

	}
}
