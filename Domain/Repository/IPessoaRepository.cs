using Domain.Entity;

namespace Domain.Repository
{
    public interface IPessoaRepository : IRepository<Pessoa>
    {
        Task<Pessoa> GetPessoaByEmailESenha(string email, string senha);
    }
}
