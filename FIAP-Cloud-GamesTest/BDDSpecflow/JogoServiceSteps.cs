using Application.DTOs;
using Application.Exceptions;
using Application.Helper;
using Application.Services;
using AutoMapper;
using Domain.Entity;
using Domain.Entity.Enum;
using Domain.Repository;
using Infrastructure.Middleware;
using Microsoft.Extensions.Logging;
using Moq;
using TechTalk.SpecFlow;

[Binding]
public class JogoServiceSteps
{
    private readonly ScenarioContext _context;
    private readonly Mock<IJogoRepository> _jogoRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ILogger<JogoService>> _loggerMock = new();
    private readonly Mock<ICorrelationIdGenerator> _correlationIdMock = new();
    private JogoService _service;
    private JogoDTO _jogoDto;
    private Exception _exception;

    public JogoServiceSteps(ScenarioContext context)
    {
        _context = context;
        _service = new JogoService(_jogoRepositoryMock.Object, _mapperMock.Object, new BaseLogger<JogoService>(_loggerMock.Object, _correlationIdMock.Object));
    }

    [Given(@"um jogo com nome ""(.*)"", empresa ""(.*)"", preco (.*), classificacao (.*) e genero (.*)")]
    public void GivenUmJogoComNome(string nome, string empresa, double preco, int classificacao, int genero)
    {
        _jogoDto = new JogoDTO
        {
            Nome = nome,
            Empresa = empresa,
            Preco = preco,
            Classificacao = (EClassificacao) classificacao,
            Genero = (EGenero) genero
        };
    }

    [Given(@"um jogo existente com id (\d+)")]
    public void GivenUmJogoExistenteComId(int id)
    {
        var jogo = new Jogo
        {
            Id = id,
            Nome = "Existente",
            Empresa = "DevCompany",
            Preco = 100,
            Classificacao = EClassificacao.Livre,
            Genero = EGenero.Action
        };

        _jogoRepositoryMock.Setup(r => r.GetById(id)).ReturnsAsync(jogo);
    }

    [Given(@"um jogo DTO com nome ""(.*)"", empresa ""(.*)"", preco (.*), classificacao (.*) e genero (.*)")]
    public void GivenUmJogoDTOParaAtualizar(string nome, string empresa, double preco, int classificacao, int genero)
    {
        _jogoDto = new JogoDTO
        {
            Nome = nome,
            Empresa = empresa,
            Preco = preco,
            Classificacao = (EClassificacao) classificacao,
            Genero = (EGenero) genero
        };
    }

    [When(@"eu adicionar o jogo")]
    public async Task WhenEuAdicionarOJogo()
    {
        _mapperMock.Setup(m => m.Map<Jogo>(_jogoDto)).Returns(new Jogo());
        await _service.AddJogo(_jogoDto);
    }

    [When(@"eu atualizar o jogo com id (\d+)")]
    public async Task WhenEuAtualizarOJogoComId(int id)
    {
        await _service.UpdateJogoById(id, _jogoDto);
    }

    [When(@"eu tentar deletar o jogo com id (\d+)")]
    public async Task WhenEuTentarDeletarOJogoComId(int id)
    {
        try
        {
            await _service.DeleteJogoById(id);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then(@"o repositorio deve ter recebido uma chamada para adicionar o jogo")]
    public void ThenRepositorioDeveReceberChamadaAdd()
    {
        _jogoRepositoryMock.Verify(r => r.Add(It.IsAny<Jogo>()), Times.Once);
    }

    [Then(@"o repositorio deve ter recebido uma chamada para atualizar o jogo")]
    public void ThenRepositorioDeveReceberChamadaUpdate()
    {
        _jogoRepositoryMock.Verify(r => r.Update(It.IsAny<Jogo>()), Times.Once);
    }

    [Then(@"uma excecao NotFoundException deve ser lancada")]
    public void ThenExcecaoNotFoundDeveSerLancada()
    {
        Assert.IsType<NotFoundException>(_exception);
    }
}
