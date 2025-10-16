using Server.Models;

namespace Server.Services;

public interface IPortfolioData
{
    Profile GetProfile();
    IEnumerable<Project> GetProjects();
    IEnumerable<Experience> GetExperience();
    IEnumerable<Skill> GetSkills();
}

public sealed class PortfolioData : IPortfolioData
{
    private readonly Lazy<Profile> _profile;
    private readonly Lazy<List<Project>> _projects;
    private readonly Lazy<List<Experience>> _experience;
    private readonly Lazy<List<Skill>> _skills;

    public PortfolioData()
    {
        _profile = new Lazy<Profile>(SeedProfile);
        _projects = new Lazy<List<Project>>(SeedProjects);
        _experience = new Lazy<List<Experience>>(SeedExperience);
        _skills = new Lazy<List<Skill>>(SeedSkills);
    }

    public Profile GetProfile() => _profile.Value;
    public IEnumerable<Project> GetProjects() => _projects.Value;
    public IEnumerable<Experience> GetExperience() => _experience.Value;
    public IEnumerable<Skill> GetSkills() => _skills.Value;

    private static Profile SeedProfile() => new(
        FullName: "Tabitha Example",
        Title: "Software Engineer",
        Summary: "Passionate about building performant web applications and crafting delightful user experiences.",
        Email: "tabitha@example.com",
        Location: "Remote / Anywhere",
        Links: new [] { "https://github.com/tabitha", "https://www.linkedin.com/in/tabitha" },
        Skills: new [] { "C#", "ASP.NET Core", "Vue", "TypeScript", "Azure" }
    );

    private static List<Project> SeedProjects() => new()
    {
        new Project(
            Id: 1,
            Name: "Portfolio Website",
            Description: "Personal website showcasing projects, experience, and blog posts.",
            Technologies: new [] { "ASP.NET Core", "Vue 3", "TypeScript", "Vite" },
            RepoUrl: null,
            DemoUrl: null,
            Highlights: new [] { "Responsive design", "Accessible UI", "SEO friendly" }
        ),
        new Project(
            Id: 2,
            Name: "Task Tracker API",
            Description: "A simple, fast REST API for managing tasks with tagging and due dates.",
            Technologies: new [] { "ASP.NET Core", "EF Core", "SQLite" },
            RepoUrl: "https://github.com/tabitha/task-tracker",
            DemoUrl: null,
            Highlights: new [] { "CQRS pattern", "Comprehensive tests" }
        )
    };

    private static List<Experience> SeedExperience() => new()
    {
        new Experience(
            Id: 1,
            Company: "Tech Corp",
            Role: "Senior Developer",
            Location: "Remote",
            StartDate: new DateOnly(2023, 1, 1),
            EndDate: null,
            Achievements: new [] { "Led migration to .NET 8", "Improved API latency by 35%" },
            Technologies: new [] { "ASP.NET Core", "Azure", "Redis" }
        ),
        new Experience(
            Id: 2,
            Company: "Web Studio",
            Role: "Full Stack Engineer",
            Location: "Remote",
            StartDate: new DateOnly(2021, 6, 1),
            EndDate: new DateOnly(2022, 12, 31),
            Achievements: new [] { "Delivered 15+ client sites", "Introduced automated deployments" },
            Technologies: new [] { "Vue", "Node.js", "PostgreSQL" }
        )
    };

    private static List<Skill> SeedSkills() => new()
    {
        new Skill("C#", "Language", 5),
        new Skill("ASP.NET Core", "Backend", 5),
        new Skill("Entity Framework Core", "Backend", 4),
        new Skill("Vue", "Frontend", 4),
        new Skill("TypeScript", "Language", 4),
        new Skill("Azure", "Cloud", 3)
    };
}
