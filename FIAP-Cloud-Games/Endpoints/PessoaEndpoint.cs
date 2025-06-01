using Application.DTOs;
using Application.Services;

namespace FIAP_Cloud_Games.Endpoints
{
    public static class PessoaEndpoint
    {
        public static void MapPessoaEndpoint(this WebApplication app)
        {
            var pessoaMapGroup = app.MapGroup("/usuario");

            pessoaMapGroup.MapPost("/", CreatePessoa);
            pessoaMapGroup.MapPost("/login", Login);
            pessoaMapGroup.MapPatch("/reativar/id", ReactivatePessoa).RequireAuthorization("Administrador");
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

        public static async Task<IResult> ReactivatePessoa(int id, PessoaService pessoaService)
        {
            await pessoaService.ReactivatePessoaById(id);
            return TypedResults.NoContent();
        }


    }
}
