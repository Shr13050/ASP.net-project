namespace GameStore.Api.Dtos;
//it represents the data transfer object for the game entity
//it is a contract that defines the structure of data sent over the network
public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
