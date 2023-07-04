using Elixir.App.HttpClients;
using Elixir.App.Objects;
using Elixir.App.Services;
using Elixir.App.Settings;

class Program
{
    private static MagicElixirService magicElixirService;

    static async Task Main(string[] args)
    {
        if (!await InitializeMagicElixirService())
        {
            return;
        }

        Console.WriteLine("Enter the available ingredients (one ingredient ID per line):");
        Console.WriteLine("Leave a blank line and press Enter to finish.");

        string? ingredientId;
        do
        {
            ingredientId = Console.ReadLine();
        }
        while (magicElixirService.AddMyIngredient(ingredientId));

        List<MagicElixir> possibleElixirs = magicElixirService.GetPossibleElixirs();

        Console.WriteLine("\nPossible Elixirs:");
        if (possibleElixirs.Count > 0)
        {
            possibleElixirs.ForEach(x => Console.WriteLine($"- {x.Name}"));
        }
        else
        {
            Console.WriteLine("No possible elixirs can be made with the available ingredients.");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    private static async Task<bool> InitializeMagicElixirService()
    {
        var appSettings = AppSettings.LoadAppSettings();
        if (appSettings == null || string.IsNullOrEmpty(appSettings.BaseUrl))
        {
            Console.WriteLine("Error loading app settings!");
            Console.ReadKey();
            return false;
        }

        var httpClient = new HttpClient { BaseAddress = new Uri(appSettings.BaseUrl) };
        var theWizardWorldClient = new TheWizardWorldClient(httpClient);

        var ingredients = await theWizardWorldClient.GetIngredients();
        if (ingredients == null)
        {
            Console.WriteLine("Error getting ingredients!");
            Console.ReadKey();
            return false;
        }

        var elixirs = await theWizardWorldClient.GetElixirs();
        if (elixirs == null)
        {
            Console.WriteLine("Error getting elixirs!");
            Console.ReadKey();
            return false;
        }

        magicElixirService = new MagicElixirService(appSettings, ingredients, elixirs);
        return true;
    }
}
