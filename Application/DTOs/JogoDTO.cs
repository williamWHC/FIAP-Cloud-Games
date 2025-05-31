using Domain.Entity.Enum;

namespace Application.DTOs
{
    public class JogoDTO
    {
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public double Preco { get; set; }

        public EClassificacao Classificacao { get; set; }
        public EGenero Genero { get; set; }
    }
}
