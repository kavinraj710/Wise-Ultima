using System.Threading.Tasks;
using Microsoft.Playwright;
using Xunit;

public class LoginTests : IAsyncLifetime
{
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IPage _page;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 500
        });


        var context = await _browser.NewContextAsync();
        _page = await context.NewPageAsync();
    }

    [Fact]
    public async Task LoginTest()
    {
        await _page.GotoAsync("https://dev.ultima.wisework.in/");

        // Wait for the first text input (email) to be available
        await _page.WaitForSelectorAsync("input[type='text']", new PageWaitForSelectorOptions { Timeout = 60000 });
        await _page.FillAsync("input[type='text']", "ajithkumar161105@gmail.com");

        // Wait for the password input field
        await _page.WaitForSelectorAsync("input[type='password']", new PageWaitForSelectorOptions { Timeout = 60000 });
        await _page.FillAsync("input[type='password']", "Ajithkumar@11");

        // Wait for the login button before clicking
        await _page.WaitForSelectorAsync("button:has-text('Login')");
        await _page.ClickAsync("button:has-text('Login')");

        // Wait for the page to load after login
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        Assert.Contains("dev.ultima.wisework.in", _page.Url);

    }

    public async Task DisposeAsync()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
