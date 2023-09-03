using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public partial class MonsterDungeonContext : DbContext
{
    public MonsterDungeonContext()
    {
    }

    public MonsterDungeonContext(DbContextOptions<MonsterDungeonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MonsterDungeon;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Cyrillic_General_CI_AS");

        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.Time).HasPrecision(0);

            entity.HasOne(d => d.HostNavigation).WithMany(p => p.Games)
                .HasForeignKey(d => d.Host)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Games_Users");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PK_Session_1");

            entity.ToTable("Session");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.GameId).HasColumnName("GameID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Game).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Games");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Session_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
