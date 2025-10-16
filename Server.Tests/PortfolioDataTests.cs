using Server.Services;
using Xunit;

namespace Server.Tests;

public class PortfolioDataTests
{
    private readonly PortfolioData _data = new();

    [Fact]
    public void Profile_Should_Have_Name_And_Skills()
    {
        var profile = _data.GetProfile();
        Assert.False(string.IsNullOrWhiteSpace(profile.FullName));
        Assert.NotEmpty(profile.Skills);
    }

    [Fact]
    public void Projects_Should_Not_Be_Empty()
    {
        var projects = _data.GetProjects();
        Assert.NotEmpty(projects);
    }
}
