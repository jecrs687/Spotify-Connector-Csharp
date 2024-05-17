using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Models
{
	public interface ISoftDeleteEntity
	{
		public DateTime DeletedAt { get; set; }

	}
	public class Entity : ISoftDeleteEntity
	{
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime DeletedAt { get; set; }

	}
}
