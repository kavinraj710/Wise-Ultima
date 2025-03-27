using Microsoft.Playwright;
using Xunit;
using System.Threading.Tasks;

namespace PlaywrightTests
{
    public class WiseUltimaTests
    {
        [Fact]
        public async Task WiseUltimaLoginAndDiscoverTest()
        {
            // Set up Playwright
            var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false }); // Open browser in non-headless mode
            var page = await browser.NewPageAsync();

            Console.WriteLine("Navigating to the login page...");
            // Navigate to the login page
            await page.GotoAsync("https://dev.ultima.wisework.in/Account/Login?ReturnUrl=%2F");

            // Perform manual login and wait for Enter key
            await PerformManualLogin(page);

            // Wait for the "Discover" button and click it
            await ClickDiscoverButton(page);

            // Continue with other interactions
            await PerformOtherInteractions(page);

            // Close the browser
            await CloseBrowser(browser);

            Console.WriteLine("Test automation completed!");
        }

        private async Task PerformManualLogin(IPage page)
        {
            // Inform the user to manually login
            Console.WriteLine("Please login manually and then press Enter to continue the test...");

            // Wait for the user to hit Enter after logging in manually
            await Task.Run(() => Console.ReadLine());  // Make the Console.ReadLine() async
            Console.WriteLine("User has logged in. Proceeding with the test...");
        }

private async Task ClickDiscoverButton(IPage page)
{
    // Wait for the homepage to load after login
    await page.WaitForSelectorAsync("button.custom-card-button"); // Wait for the "Discover" button
    Console.WriteLine("Waiting for the 'Discover' button to appear...");

    // Interact with the "Discover" button using the improved selector
    var discoverButton = await page.QuerySelectorAsync("button.custom-card-button[title='Discover']");
    if (discoverButton != null && await discoverButton.IsVisibleAsync())
    {
        Console.WriteLine("Discover button found. Clicking the button...");
        // Click the "Discover" button to go to the explore page
        await discoverButton.ClickAsync();
        Console.WriteLine("Button Clicked");

        // Option 1: Wait for navigation to the new page (URL or page load)
        // You can use WaitForNavigationAsync with the `waitUntil` option to ensure that the page has loaded
        try
        {
            await page.WaitForNavigationAsync(new PageWaitForNavigationOptions
            {
                Timeout = 60000, // 60 seconds timeout
                WaitUntil = WaitUntilState.Load // Ensures the page is fully loaded
            });
            Console.WriteLine("Navigation complete.");
        }
        catch (TimeoutException)
        {
            Console.WriteLine("Navigation timed out.");
        }

        // Option 2: Alternatively, wait for an element that's unique to the explore page
        // await page.WaitForSelectorAsync("selector-unique-to-explore-page"); // Example of waiting for an element that only appears on the explore page

    }
    else
    {
        Console.WriteLine("Discover button not found or not visible.");
        return;  // Exit the test if the Discover button is not found or visible
    }
}



        private async Task PerformOtherInteractions(IPage page)
        {
            try
            {
                // Perform other actions like clicking buttons and chips
                Console.WriteLine("Waiting for 'W-Predict' button...");
                await page.WaitForSelectorAsync("button:has-text('W-Predict')", new PageWaitForSelectorOptions { Timeout = 60000 });
                Console.WriteLine("Clicking the 'W-Predict' button...");
                await page.ClickAsync("button:has-text('W-Predict')");
                await Task.Delay(2000); // Add a delay if necessary
                Console.WriteLine("Clicking the 'M-Predict' button...");
                await page.ClickAsync("button:has-text('M-Predict')");
                await Task.Delay(2000); // Add a delay if necessary
                Console.WriteLine("Clicking the 'Current' button...");
                await page.ClickAsync("button:has-text('Current')");
                await Task.Delay(2000); // Add a delay if necessary
                // Click the chip with the text "a00011736p000"
                Console.WriteLine("Clicking the chip with text 'a00011736p000'...");
                await page.ClickAsync("div.mud-chip:has-text('a00011736p000')");
                await Task.Delay(2000); // Add a delay if necessary
                // Click the "Feedback" section
                Console.WriteLine("Clicking the 'Feedback' section...");
                await page.ClickAsync("span.mud-chip-content:has-text('Feedback')");
                await Task.Delay(2000); // Add a delay if necessary
                // Click the first feedback option
                Console.WriteLine("Clicking the first feedback option...");
                await page.ClickAsync("svg.mud-icon-root");
                
                // Wait for 2 seconds
                Console.WriteLine("Waiting for feedback interaction to complete...");
                await Task.Delay(2000);

                // Click the "Close" button to close the feedback dialog
                Console.WriteLine("Clicking the 'Close' button to close the feedback dialog...");
                await page.ClickAsync("button[aria-label='close']");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during interactions: " + ex.Message);
                var pageContent = await page.ContentAsync();  // Get the page content to debug
                Console.WriteLine("Page content: " + pageContent);
            }
        }

        private async Task CloseBrowser(IBrowser browser)
        {
            // Wait for a few seconds and then close the browser
            Console.WriteLine("Waiting before closing the browser...");
            await Task.Delay(2000); // Add a delay if necessary

            Console.WriteLine("Closing the browser...");
            await browser.CloseAsync();
        }
    }
}
