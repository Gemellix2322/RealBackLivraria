using back_teste.Model;
using Microsoft.EntityFrameworkCore;

namespace back_teste.Data
{
    public class LivrariaDBContext : DbContext
    {
        public LivrariaDBContext(DbContextOptions<LivrariaDBContext> options) : base(options) { }

        public DbSet<UsuarioModel> users { get; set; }

        public DbSet<LivrosModel> livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseCollation("utf8mb4_general_ci")
                        .HasCharSet("utf8mb4");

            modelBuilder.Entity<UsuarioModel>(entity =>
            {
                entity.HasKey(e => e.id);
            });

            modelBuilder.Entity<LivrosModel>(entity =>
            {
                entity.HasKey(e => e.id);
            });
        }
    }
}