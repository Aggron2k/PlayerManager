using Microsoft.EntityFrameworkCore;
using PlayerManager.Models;

namespace PlayerManager.Context
{
    public class EfContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\..\\database\\database.db");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>() // entitás a külső kulccsal
            .HasOne(p => p.ReferencedTeam) // hivatkozzuk a másik entitást
            .WithMany() // jelezzük, hogy ez 1:N kapcsolat
            .HasForeignKey(p => p.TeamId) // megadjuk a külső kulcsot
            .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
