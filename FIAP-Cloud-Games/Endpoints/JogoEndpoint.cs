
using Application.DTOs;
using Application.Services;
using Domain.Entity;

namespace FIAP_Cloud_Games.Endpoints
{
    public static class JogoEndpoint
    {
        public static void MapJogoEndpoint(this WebApplication app)
        {
            var jogoMapGroup = app.MapGroup("/jogo").RequireAuthorization();


            jogoMapGroup.MapGet("/", GetAllJogos);
            jogoMapGroup.MapPost("/", CreateJogo).RequireAuthorization("Administrador");
            jogoMapGroup.MapDelete("/id", DeleteJogo).RequireAuthorization("Administrador");
            jogoMapGroup.MapPut("/id", UpdateJogo).RequireAuthorization("Administrador");
        }

        public static async Task<IResult> CreateJogo(JogoDTO jogoDTO, JogoService jogoService)
        {
            await jogoService.AddJogo(jogoDTO);
            return TypedResults.Created();
        }

        public static async Task<IResult> GetAllJogos(JogoService jogoService)
        {
            List<JogoDTO> jogos = await jogoService.GetAllJogos();
            return TypedResults.Ok(jogos);
        }

        public static async Task<IResult> DeleteJogo(int id, JogoService jogoService)
        {
            await jogoService.DeleteJogoById(id);
            return TypedResults.NoContent();
        }

        public static async Task<IResult> UpdateJogo(int id, JogoDTO jogoDTO, JogoService jogoService)
        {
            await jogoService.UpdateJogoById(id, jogoDTO);
            return TypedResults.NoContent();
        }

    }
}
