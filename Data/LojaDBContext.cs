using Microsoft.EntityFrameworkCore;

public class LojaDBContext : DbContext
{
    public LojaDBContext(DbContextOptions<LojaDBContext> options) : base(options)
    {
    }

    public DbSet<CLiente> cLientes { get; set; }
    public DbSet<Contrato> contratos { get; set; }
    public DbSet<Servico> servicos{ get; set; }



}