using Microsoft.EntityFrameworkCore;
using TesteIlegra.Domain.Modelo;

namespace TesteIlegra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Revenda> Revendas => Set<Revenda>();
        public DbSet<PedidoCliente> PedidosClientes => Set<PedidoCliente>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Revenda>().OwnsMany(r => r.Telefones, a =>
            {
                a.WithOwner().HasForeignKey("RevendaId");
                a.Property(t => t.Numero).IsRequired();
                a.ToTable("Telefones");
            });

            modelBuilder.Entity<Revenda>().OwnsMany(r => r.Contatos, a =>
            {
                a.WithOwner().HasForeignKey("RevendaId");
                a.Property(c => c.Nome).IsRequired();
                a.Property(c => c.Principal).IsRequired();
                a.ToTable("Contatos");
            });

            modelBuilder.Entity<Revenda>().OwnsMany(r => r.EnderecosEntrega, a =>
            {
                a.WithOwner().HasForeignKey("RevendaId");
                a.ToTable("EnderecosEntrega");
            });

            modelBuilder.Entity<PedidoCliente>().OwnsMany(p => p.Itens, a =>
            {
                a.WithOwner().HasForeignKey("PedidoClienteId");
                a.ToTable("ItensPedidoCliente");
            });
        }
    }
}
