namespace Server.Models;

public record Experience(
    int Id,
    string Company,
    string Role,
    string Location,
    DateOnly StartDate,
    DateOnly? EndDate,
    string[] Achievements,
    string[] Technologies
);
