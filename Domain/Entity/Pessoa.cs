using Domain.Entity.Enum;

namespace Domain.Entity
{
    public class Pessoa : EntityBase
    {
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ERole Role { get; set; }
        public ICollection<Jogo> Jogos { get; set; }

    }
}
