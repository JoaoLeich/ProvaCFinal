using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


public class ClienteService
{
    private readonly LojaDBContext _dbContext;
    public ClienteService(LojaDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CLiente> GetClienteById(int id)
    {
        return await _dbContext.cLientes.FindAsync(id);
    }

    public async Task<Boolean> Login(LoginModel login)
    {

        var person = await _dbContext.cLientes.FirstOrDefaultAsync(x => x.email.Equals(login.email));

        if (person.senha.Equals(login.senha))
        {

            return true;

        }

        return false;
    }

    public async Task AddClienteAsync(CLiente cliente)
    {
        _dbContext.cLientes.Add(cliente);
        await _dbContext.SaveChangesAsync();
    }
}
