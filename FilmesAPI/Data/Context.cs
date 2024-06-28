using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> opts) : base(opts) 
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sessao>()
            .HasKey(sessao => new { sessao.cinemaID, sessao.filmeID });

        modelBuilder.Entity<Sessao>()
            .HasOne(sessao => sessao.cinema)
            .WithMany(cinema => cinema.sessoes)
            .HasForeignKey(sessao => sessao.cinemaID);

        modelBuilder.Entity<Sessao>()
            .HasOne(sessao => sessao.filme)
            .WithMany(filme => filme.sessoes)
            .HasForeignKey(sessao => sessao.filmeID);

        modelBuilder.Entity<Endereco>()
            .HasOne(endereco => endereco.Cinema)
            .WithOne(cinema => cinema.endereco)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Filme> Filmes { get; set; }

    public DbSet<Cinema> Cinemas { get; set; }

    public DbSet<Endereco> Enderecos { get; set; }

    public DbSet<Sessao> Sessoes { get; set; } 
}

