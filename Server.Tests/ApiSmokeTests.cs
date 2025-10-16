using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Server.Models;
using Xunit;

namespace Server.Tests;

public class ApiSmokeTests : IClassFixture<WebApplicationFactory<ProgramMarker>>
{
    private readonly WebApplicationFactory<ProgramMarker> _factory;

    public ApiSmokeTests(WebApplicationFactory<ProgramMarker> factory)
    {
        _factory = factory.WithWebHostBuilder(_ => { });
    }

    [Fact]
    public async Task Get_Profile_Returns_Profile()
    {
        var client = _factory.CreateClient();
        var profile = await client.GetFromJsonAsync<Profile>("/api/profile");
        Assert.NotNull(profile);
        Assert.False(string.IsNullOrWhiteSpace(profile!.FullName));
    }
}
