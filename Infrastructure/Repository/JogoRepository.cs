using Domain.Entity;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class JogoRepository : EFRepository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
