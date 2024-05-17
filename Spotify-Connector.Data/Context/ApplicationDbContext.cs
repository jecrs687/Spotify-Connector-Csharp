using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Spotify_Connect.Business.Models;

namespace Spotify_Connector.Data.Context
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Artist> Artist { get; set; }
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	: base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
			modelBuilder.ApplyGlobalFilters<ISoftDeleteEntity>(e => e.DeletedAt == null);

			//if (this.GetService<IHostEnvironment>().IsDevelopment())
			//    SeedTestData.Configure(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			UpdateAuditDates();

			return base.SaveChanges();
		}

		public override Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = default)
		{
			UpdateAuditDates();

			return base.SaveChangesAsync(cancellationToken);
		}

		private void UpdateAuditDates()
		{
			foreach (var entry in ChangeTracker.Entries())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.CurrentValues[nameof(Entity.CreatedAt)] = DateTime.UtcNow;
						break;
					case EntityState.Modified:
						entry.CurrentValues[nameof(Entity.UpdatedAt)] = DateTime.UtcNow;
						break;
					case EntityState.Deleted:
						entry.State = EntityState.Modified;
						entry.CurrentValues[nameof(Entity.DeletedAt)] = DateTime.UtcNow;
						break;
				}
			}
		}
	}
}
