using Application.DTOs;
using Application.Helper;
using AutoMapper;
using Domain.Entity;
using Domain.Repository;
namespace Application.Services
{
    public class JogoService
    {
        IJogoRepository _jogoRepository;
        IMapper _mapper;
        BaseLogger<JogoService> _logger;
        public JogoService(IJogoRepository jogoRepository, IMapper mapper, BaseLogger<JogoService> logger)
        {
            _jogoRepository = jogoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task DeleteJogoById(int id)
        {
            _logger.LogInformation($"Deletando jogo com id: {id}.");
            await _jogoRepository.Delete(id);
            _logger.LogInformation($"Jogo com id {id} deletado.");
        }

        public async Task UpdateJogoById(int id, JogoDTO jogoDTO)
        {
            _logger.LogInformation($"Atualizando jogo com id: {id}.");

            Jogo jogo = await _jogoRepository.GetById(id);

            jogo.Nome = jogoDTO.Nome;
            jogo.Empresa = jogoDTO.Empresa;
            jogo.Preco = jogoDTO.Preco;
            jogo.Classificacao = jogoDTO.Classificacao;
            jogo.Genero = jogoDTO.Genero;

            await _jogoRepository.Update(jogo);

            _logger.LogInformation($"Jogo com id {id} atualizado.");
        }

        public async Task AddJogo(JogoDTO jogoDTO)
        {
            _logger.LogInformation("Criando jogo.");

            Jogo jogo = _mapper.Map<Jogo>(jogoDTO);
            await _jogoRepository.Add(jogo);

            _logger.LogInformation("Jogo criado.");
        }

        public async Task<List<JogoDTO>> GetAllJogos()
        {
            _logger.LogInformation("Buscando todos os jogos.");
            List<Jogo> jogos = (await _jogoRepository.GetAll()).ToList();
            _logger.LogInformation($"{jogos.Count} jogos retonaram.");
            return _mapper.Map<List<JogoDTO>>(jogos);
        }
    }
}
