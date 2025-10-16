namespace Server.Models;

public record Skill(
    string Name,
    string Category,
    int Proficiency // 1-5 scale
);
