using Domain.Entity.Enum;

namespace Application.DTOs
{
    public class PessoaDTO
    {
        public string Nome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ERole Role { get; set; }

    }
}
