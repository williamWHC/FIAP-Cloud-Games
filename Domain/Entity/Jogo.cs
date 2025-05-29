using Domain.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Jogo : EntityBase
    {
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public double Preco { get; set; }

        public EClassificacao Classificacao { get; set; }
        public EGenero Genero { get; set; }

        public ICollection<Promocao> Promocoes { get; set; }

        public ICollection<Pessoa> Pessoas { get; set; }
    }
}
