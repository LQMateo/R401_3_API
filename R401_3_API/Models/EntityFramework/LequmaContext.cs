using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace R401_3_API.Models.EntityFramework;

public partial class LequmaContext : DbContext
{
    public LequmaContext()
    {
    }

    public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public LequmaContext(DbContextOptions<LequmaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Avi> Avis { get; set; }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseNpgsql("Server=51.83.36.122; port=5432; Database=lequma; uid=lequma; password=thrzJ1; SearchPath=r41_3;");
            optionsBuilder.UseLazyLoadingProxies(); // → Utile si chargement des requetes en lazy
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Avi>(entity =>
        {
            entity.HasKey(e => new { e.Film, e.Utilisateur }).HasName("pk_avis");

            entity.HasOne(d => d.FilmNavigation).WithMany(p => p.Avis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avis_film");

            entity.HasOne(d => d.UtilisateurNavigation).WithMany(p => p.Avis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_avis_utilisateur");
        });

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_categorie");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('categorie_id_seq'::regclass)");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_film");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('film_id_seq'::regclass)");

            entity.HasOne(d => d.CategorieNavigation).WithMany(p => p.Films)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_film_categorie");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_utilisateur");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('utilisateur_id_seq'::regclass)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
