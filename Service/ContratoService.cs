using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class ContratoService
{

    private readonly LojaDBContext _dbContext;

    public ContratoService(LojaDBContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Boolean> AddContratoAsync(Contrato contrato)
    {

        var cliente = await _dbContext.cLientes.FindAsync(contrato.UserId);

        var servico = await _dbContext.servicos.FindAsync(contrato.ServicoId);

        if (cliente == null || servico == null)
        {

            return false;

        }

        _dbContext.contratos.Add(contrato);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Contrato>> FindContratosByIdCliente(int clienteId)
    {

        var contratoList = await _dbContext.contratos.Where(c => c.UserId == clienteId).ToListAsync();

        return contratoList;

    }
}