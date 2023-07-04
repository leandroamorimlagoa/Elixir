using Elixir.App.Objects;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Elixir.App.HttpClients
{
    public class TheWizardWorldClient
    {
        private readonly HttpClient httpClient;
        private JsonSerializerOptions options;

        public TheWizardWorldClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<Ingredient>?> GetIngredients()
        {
            HttpResponseMessage response = await httpClient.GetAsync("ingredients");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error getting ingredients! Status code: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();
            var lista = JsonSerializer.Deserialize<List<Ingredient>>(json, options);
            return lista;
        }

        public async Task<List<MagicElixir>?> GetElixirs()
        {
            HttpResponseMessage response = await httpClient.GetAsync("elixirs");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error getting elixirs! Status code: {response.StatusCode} - {response.ReasonPhrase}");
                return null;
            }
            string json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<MagicElixir>>(json, options);
        }
    }
}
