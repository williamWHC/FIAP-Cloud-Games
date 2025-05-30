using Domain.Entity;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PessoaRepository : EFRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Pessoa> GetPessoaByEmailESenha(string email, string senha)
        {

            return await _dbSet.FirstOrDefaultAsync(entity => entity.Email == email && entity.Senha == senha);
        }
    }
}
