using AutoMapper;
using Domain.Entity;
using Domain.Entity.DTOs;
using Domain.Repository;
using Domain.Services;

namespace FIAP_Cloud_Games.Endpoints
{
    public static class PessoaEndpoint
    {
        public static void MapPessoaEndpoint(this WebApplication app)
        {
            var pessoaMapGroup = app.MapGroup("/usuario");

            pessoaMapGroup.MapPost("/", CreatePessoa);
            pessoaMapGroup.MapPost("/login", Login);
            pessoaMapGroup.MapPost("/reativar/id", ReactivateUser).RequireAuthorization("Administrador");
        }

        public static async Task<IResult> CreatePessoa(PessoaDTO pessoaDTO, PessoaService pessoaService)
        {
            await pessoaService.AddPessoa(pessoaDTO);
            return TypedResults.Created();
        }

        public static async Task<IResult> Login(LoginDTO loginDTO, PessoaService pessoaService)
        {
            LoggedDTO loggedDTO = await pessoaService.Login(loginDTO);
            return TypedResults.Ok(loggedDTO);
        }

        public static async Task<IResult> ReactivateUser(int id, PessoaService pessoaService)
        {
            await pessoaService.ReactivatePessoaById(id);
            return TypedResults.NoContent();
        }


    }
}
