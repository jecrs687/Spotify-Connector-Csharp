using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spotify_Connect.Business.Models;

namespace dot_net_api_study.Data.Mapping
{

	internal class ArtistMapping : IEntityTypeConfiguration<Artist>
	{
		public void Configure(EntityTypeBuilder<Artist> builder)
		{
			builder.ToTable(nameof(Artist));

			builder
				.HasKey(x => x.Id);

			builder
				.Property(x => x.ArtistCode)
				.IsRequired()
				.HasColumnType("varchar(150)");

			builder
				.Property(x => x.Name)
				.IsRequired()
				.HasColumnType("varchar(150)");

			builder
				.Property(x => x.Uri)
				.IsRequired()
				.HasColumnType("varchar(100)");

			builder
				.Property(x => x.DeletedAt)
				.HasColumnType("Datetime");

			builder
				.Property(x => x.CreatedAt)
				.HasColumnType("Datetime");
			builder
				.Property(x => x.UpdatedAt)
				.HasColumnType("Datetime");
		}
	}
}

