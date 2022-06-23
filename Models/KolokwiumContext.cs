using Microsoft.EntityFrameworkCore;
using System;

namespace Kolokwium_2.Models
{
    public class KolokwiumContext : DbContext
    {
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<File> File { get; set; }
        public KolokwiumContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>(e =>
            {
                e.ToTable("Organization");
                e.HasKey(e => e.OrganizationID);

                e.Property(e => e.OrganizationName).HasMaxLength(100).IsRequired();
                e.Property(e => e.OrganizationDomain).HasMaxLength(50).IsRequired();

                e.HasData(
                    new Organization
                    {
                        OrganizationID = 1,
                        OrganizationName = "Org",
                        OrganizationDomain = "www.org.pl"
                    }
                );
            });

            modelBuilder.Entity<Member>(e =>
            {
                e.ToTable("Member");
                e.HasKey(e => e.MemberID);

                e.Property(e => e.MemberName).HasMaxLength(20).IsRequired();
                e.Property(e => e.MemberSurname).HasMaxLength(50).IsRequired();
                e.Property(e => e.MemberNickName).HasMaxLength(20).IsRequired(false);

                e.HasOne(e => e.Organization)
                .WithMany(e => e.Members)
                .HasForeignKey(e => e.OrganizationID)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasData(
                    new Member
                    {
                        MemberID = 1,
                        OrganizationID = 1,
                        MemberName = "Jan",
                        MemberSurname = "Kowalski",
                        MemberNickName = "Kow"
                    }
                );
            });

            modelBuilder.Entity<Team>(e =>
            {
                e.ToTable("Team");
                e.HasKey(e => e.TeamID);

                e.Property(e => e.TeamName).HasMaxLength(50).IsRequired();
                e.Property(e => e.TeamDescription).HasMaxLength(500).IsRequired(false);

                e.HasOne(e => e.Organization)
                .WithMany(e => e.Teams)
                .HasForeignKey(e => e.OrganizationID)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasData(
                    new Team
                    {
                        TeamID = 1,
                        OrganizationID = 1,
                        TeamName = "Team",
                        TeamDescription = "To jest Team"
                    }
                );
            });

            modelBuilder.Entity<Membership>(e =>
            {
                e.ToTable("Membership");
                e.HasKey(e => new
                {
                    e.MemberID,
                    e.TeamID
                });

                e.Property(e => e.MembershipDate).IsRequired();

                e.HasOne(e => e.Member)
                 .WithMany(e => e.Memberships)
                 .HasForeignKey(e => e.MemberID)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(e => e.Team)
                .WithMany(e => e.Memberships)
                .HasForeignKey(e => e.TeamID)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasData(
                    new Membership
                    {
                        MemberID = 1,
                        TeamID = 1,
                        MembershipDate = DateTime.ParseExact("2022-06-09", "yyyy-mm-dd", null)
                    }
                );
            });

            modelBuilder.Entity<File>(e =>
            {
                e.ToTable("File");
                e.HasKey(e => new
                {
                    e.FileID,
                    e.TeamID
                });

                e.Property(e => e.FileName).HasMaxLength(100).IsRequired();
                e.Property(e => e.FileExtension).HasMaxLength(4).IsRequired();
                e.Property(e => e.FileSize).IsRequired();

                e.HasOne(e => e.Team)
                 .WithMany(e => e.Files)
                 .HasForeignKey(e => e.TeamID)
                 .OnDelete(DeleteBehavior.NoAction);

                e.HasData(
                    new File
                    {
                        FileID = 1,
                        TeamID = 1,
                        FileName = "File",
                        FileExtension = "pdf",
                        FileSize = 24
                    }
                );
            });
        }
    }
}
