namespace Server.Models;

public record Project(
    int Id,
    string Name,
    string Description,
    string[] Technologies,
    string? RepoUrl,
    string? DemoUrl,
    string[] Highlights
);
