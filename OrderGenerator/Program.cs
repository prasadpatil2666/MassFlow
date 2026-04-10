using System.Net.Http.Json;
using OrderGenerator;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var client = new HttpClient();

Log.Information("Starting Order Generator...");

// In top-level statements, you can use 'await' directly at the top level
while (true)
{
    var order = Generator.CreateOrder();

    try
    {
        // Added 'await' directly here instead of wrapping in Task.Run
        var response = await client.PostAsJsonAsync("http://localhost:5088/Orders", order);

        if (response.IsSuccessStatusCode)
            Log.Information("Order sent successfully.");
        else
            Log.Warning("API returned status: {StatusCode}", response.StatusCode);
    }
    catch (Exception ex)
    {
        Log.Error("API Error: {Message}", ex.Message);
        await Task.Delay(TimeSpan.FromSeconds(5));
    }

    // Delay between orders
    await Task.Delay(TimeSpan.FromSeconds(Random.Shared.NextDouble() * 2));
}