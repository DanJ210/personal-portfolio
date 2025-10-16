namespace Server.Models;

public record Profile(
    string FullName,
    string Title,
    string Summary,
    string Email,
    string Location,
    string[] Links,
    string[] Skills
);
