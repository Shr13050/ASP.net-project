using System;
using GameStore.Api.Dtos;
//this file is to define the endpoints related to games
//we will use extension methods to organize the endpoints , in easy language we will create a static class and inside that class we will create static methods which will extend the WebApplication class to add the endpoints related to games
namespace GameStore.Api.EndPoints;

public static class GamesEndpoints
{
    //for new created game we introduce a endpoint name 
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
        new GameDto(1,"The Witcher 3","RPG",29.99m,new DateOnly(2015,5,19)),
    new GameDto(2,"Cyberpunk 2077","Action RPG",59.99m,new DateOnly(2020,12,10)),
    new GameDto(3,"Minecraft","Sandbox",26.95m,new DateOnly(2011,11,18))
    ];

    //explanation of the below method in easy language and detail is that we are creating a static method named MapGamesEndpoints which extends the WebApplication class.
    // this method will be used to define all the endpoints related to games.when this method is called on an instance of WebApplication,it will add the defined endpoints to that application instance.
    // this approach helps in organizing the code better by grouping related endpoints together and makes it easier to maintain and understand.the method uses various HTTP verbs like GET,POST,PUT,DELETE to define endpoints for different operations related to games.
    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group=app.MapGroup("/games");
        //new api endpoint
        //GET /games
        group.MapGet("/", () => games);

        //Get /games/{id}


        group.MapGet("/{id}", (int id) =>

            {
                var game = games.Find(game => game.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
            }
            ).WithName(GetGameEndpointName);

        //Post /games
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new GameDto(
                games.Count() + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        }
        );

        //Put /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var gameIndex = games.FindIndex(game => game.Id == id);
            if (gameIndex == -1)
            {
                return Results.NotFound();
            }
            var game = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            games[gameIndex] = game;
            return Results.NoContent();
        });

        //Delete /games/{id}
        group.MapDelete("/{id}", (int id) =>
        {
            var gameIndex = games.FindIndex(game => game.Id == id);
            if (gameIndex == -1)
            {
                return Results.NotFound();
            }
            games.RemoveAt(gameIndex);
            return Results.NoContent();
        });


    }
}