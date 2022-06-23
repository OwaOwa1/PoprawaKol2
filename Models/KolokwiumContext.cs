using Microsoft.EntityFrameworkCore;
using System;

namespace Kolokwium_2.Models
{
    public class KolokwiumContext : DbContext
    {
        public DbSet<Musician> Musician { get; set; }
        public DbSet<Musician_Track> Musician_Track { get; set; }
        public DbSet<Track> Track { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<MusicLabel> MusicLabel { get; set; }
        public KolokwiumContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Musician>(e =>
            {
                e.ToTable("Musician");
                e.HasKey(e => e.IdMusician);

                e.Property(e => e.FirstName).HasMaxLength(30).IsRequired();
                e.Property(e => e.LastName).HasMaxLength(50).IsRequired();
                e.Property(e => e.Nickname).HasMaxLength(20).IsRequired(false);

                e.HasData(
                    new Musician
                    {
                        IdMusician = 1,
                        FirstName = "Michal",
                        LastName = "Kowalski",
                        Nickname = "Kow"
                    }
                );
            });

            modelBuilder.Entity<MusicLabel>(e =>
            {
                e.ToTable("MusicLabel");
                e.HasKey(e => e.IdMusicLabel);

                e.Property(e => e.Name).HasMaxLength(50).IsRequired();

                e.HasData(
                    new MusicLabel
                    {
                        IdMusicLabel = 1,
                        Name = "Label"
                    }
                );
            });

            modelBuilder.Entity<Album>(e =>
            {
                e.ToTable("Album");
                e.HasKey(e => e.IdAlbum);

                e.Property(e => e.AlbumName).HasMaxLength(30).IsRequired();
                e.Property(e => e.PublishDate).IsRequired();

                e.HasOne(e => e.IdMusicLabel)
                .WithMany(e => e.Albums)
                .HasForeignKey(e => e.IdMusicLabel)
                .OnDelete(DeleteBehavior.Cascade);
                
                e.HasData(
                    new Album
                    {
                        IdAlbum = 1,
                        AlbumName = "Tak",
                        PublishDate = DateTime.ParseExact("2022-06-09", "yyyy-mm-dd", null),
                        IdMusicLabel = 1
                    }
                );
            });

            modelBuilder.Entity<Track>(e =>
            {
                e.ToTable("Track");
                e.HasKey(e => e.IdTrack);

                e.Property(e => e.TrackName).HasMaxLength(20).IsRequired();
                e.Property(e => e.Duration).IsRequired();

                e.HasOne(e => e.Album)
                .WithMany(e => e.Tracks)
                .HasForeignKey(e => e.Album)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

                e.HasData(
                    new Track
                    {
                        IdTrack = 1,
                        TrackName = "Piosenka",
                        Duration = 21.14F,
                        Album = 1
                    }
                );
            });

            modelBuilder.Entity<Musician_Track>(e =>
            {
                e.ToTable("Musician_Track");
                e.HasKey(e => new
                {
                    e.Musician,
                    e.Track
                });

                e.HasOne(e => e.Musician)
                 .WithMany(e => e.Musician_Tracks)
                 .HasForeignKey(e => e.Musician)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(e => e.Track)
                 .WithMany(e => e.Musician_Tracks)
                 .HasForeignKey(e => e.Track)
                 .OnDelete(DeleteBehavior.Cascade);

                e.HasData(
                    new Musician_Track
                    {
                        Musician = 1,
                        Track = 1
                    }
                );
            });
        }
    }
}
