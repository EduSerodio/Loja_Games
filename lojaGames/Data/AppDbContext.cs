using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lojaGames.Model;
using Microsoft.EntityFrameworkCore;

namespace lojaGames.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        //cria a tabela e realiza os relacionamento entre as tabelas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().ToTable("tb_produtos");
            modelBuilder.Entity<Categoria>().ToTable("tb_categorias");

                modelBuilder.Entity<Produto>()
                .HasOne( _ => _.Categoria)
                .WithMany(c => c.Produto)
                .HasForeignKey("CategoriaId")
                .OnDelete(DeleteBehavior.Cascade);
        }


        //para manipular
        public DbSet<Categoria> Categorias { get; set; } = null!;

        public DbSet<Produto> Produtos { get; set; } = null!;

    }
}