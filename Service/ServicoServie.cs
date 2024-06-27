using Microsoft.EntityFrameworkCore;

public class ServicoServie
{

    private readonly LojaDBContext _dbContext;
    public ServicoServie(LojaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddServicoAsync(Servico servico)
    {
        _dbContext.servicos.Add(servico);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<Servico> GetServicoById(int id)
    {
        return await _dbContext.servicos.FindAsync(id);
    }

    public async Task UpdateServicoAsync(Servico servico)
    {
        _dbContext.Entry(servico).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }


}