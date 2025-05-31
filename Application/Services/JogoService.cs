using Application.DTOs;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.Enum;
using Domain.Repository;
namespace Application.Services
{
    public class JogoService
    {
        IJogoRepository _jogoRepository;
        IMapper _mapper;
        public JogoService(IJogoRepository jogoRepository, IMapper mapper)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
        }

        public async Task DeleteJogoById(int id)
        {
            await _jogoRepository.Delete(id);
        }

        public async Task UpdateJogoById(int id, JogoDTO jogoDTO)
        {
            Jogo jogo = await _jogoRepository.GetById(id);

            jogo.Nome = jogoDTO.Nome;
            jogo.Empresa = jogoDTO.Empresa;
            jogo.Preco = jogoDTO.Preco;
            jogo.Classificacao = jogoDTO.Classificacao;
            jogo.Genero = jogoDTO.Genero;

            await _jogoRepository.Update(jogo);
        }

        public async Task AddJogo(JogoDTO jogoDTO)
        {
            Jogo jogo = _mapper.Map<Jogo>(jogoDTO);
            await _jogoRepository.Add(jogo);            
        }

        public async Task<List<JogoDTO>> GetAllJogos()
        {
            List<Jogo> jogos = (await _jogoRepository.GetAll()).ToList();
            return _mapper.Map<List<JogoDTO>>(jogos);
        }
    }
}
