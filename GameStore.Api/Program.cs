using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);
//this is the application builder which helps to configure the application

//below secttion is to configure http request pipeline
var app = builder.Build();//instance of the application
//this is basically the host of the application 
List<GameDto> games = [
    new GameDto(1,"The Witcher 3","RPG",29.99m,new DateOnly(2015,5,19)),
    new GameDto(2,"Cyberpunk 2077","Action RPG",59.99m,new DateOnly(2020,12,10)),
    new GameDto(3,"Minecraft","Sandbox",26.95m,new DateOnly(2011,11,18))
];
//new api endpoint
//GET /games
app.MapGet("/games", () => games);

//Get /games/{id}

//for new created game we introduce a endpoint name 
const string GetGameEndpointName = "GetGame";
app.MapGet("/games/{id}", (int id) =>

    games.Find(game => game.Id == id)
    ).WithName(GetGameEndpointName);

//Post /games
app.MapPost("/games",(CreateGameDto newGame) =>
{
    GameDto game= new GameDto(
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
 app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
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
 app.MapDelete("/games/{id}", (int id) =>
 {
     var gameIndex = games.FindIndex(game => game.Id == id);
     if (gameIndex == -1)
     {
         return Results.NotFound();
     }
     games.RemoveAt(gameIndex);
     return Results.NoContent();
 });


app.Run();

//this files defines the code to bootstrap the application 