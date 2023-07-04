using Elixir.App.Objects;
using Elixir.App.Settings;

namespace Elixir.App.Services
{
    public class MagicElixirService
    {
        private readonly List<Ingredient> availableIngredients;
        private readonly List<MagicElixir> elixirs;
        private HashSet<string> myIngredients;
        private readonly AppSettings appSettings;

        public MagicElixirService(AppSettings appSettings, List<Ingredient> ingredients, List<MagicElixir> elixirs)
        {
            this.myIngredients = new HashSet<string>();
            this.availableIngredients = ingredients.Where(i => i.Id != null).ToList();
            this.appSettings = appSettings;
            this.elixirs = elixirs.Where(x => appSettings.IncludeElixirWithNoIngredients 
                                                || (x.Ingredients != null && x.Ingredients.Count > 0)).ToList();
        }

        public List<MagicElixir> GetPossibleElixirs()
        {
            List<MagicElixir> possibleElixirs = new List<MagicElixir>();
            var relevantElixirs = elixirs.Where(x => x.Ingredients.Count <= myIngredients.Count).ToList();

            Parallel.ForEach(relevantElixirs, elixir =>
            {
                if (CanMakeElixir(elixir, myIngredients))
                {
                    lock (possibleElixirs)
                    {
                        possibleElixirs.Add(elixir);
                    }
                }
            });

            return possibleElixirs;
        }

        public bool AddMyIngredient(string? ingredientId)
        {
            if (string.IsNullOrEmpty(ingredientId))
            {
                return false;
            }

            if (!availableIngredients.Any(x => x.Id.Equals(ingredientId, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Ingredient '{ingredientId}' not found.");
                return true;
            }

            if (myIngredients.Contains(ingredientId, StringComparer.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Ingredient '{ingredientId}' already added to your list.");
                return true;
            }

            myIngredients.Add(ingredientId);
            Console.WriteLine($"Ingredient '{ingredientId}' added to your list.");

            return true;
        }

        public HashSet<string> GetAvailableIngredients()
        {
            return myIngredients;
        }

        private bool CanMakeElixir(MagicElixir elixir, HashSet<string> availableIngredients)
        {
            foreach (Ingredient ingredient in elixir.Ingredients)
            {
                if (!availableIngredients.Any(availableId => availableId.Equals(ingredient.Id, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
