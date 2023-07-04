using Elixir.App.Objects;
using Elixir.App.Services;
using Elixir.App.Settings;

namespace Elixir.App.Tests
{
    public class MagicElixirServiceTests
    {
        [Fact]
        public void GetPossibleElixirs_ReturnsPossibleElixirs_ShouldReturnValidList()
        {
            // Arrange
            var expectedElixirCount = 1;
            var appSettings = new AppSettings { IncludeElixirWithNoIngredients = false };
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = "ingredient1", Name = "Ingredient 1" },
                new Ingredient { Id = "ingredient2", Name = "Ingredient 2" },
                new Ingredient { Id = "ingredient3", Name = "Ingredient 3" }
            };

            var elixirs = new List<MagicElixir>
            {
                new MagicElixir { Name = "Elixir 1", Ingredients = new List<Ingredient> { ingredients[0], ingredients[1] } },
                new MagicElixir { Name = "Elixir 2", Ingredients = new List<Ingredient> { ingredients[1], ingredients[2] } },
                new MagicElixir { Name = "Elixir 3", Ingredients = new List<Ingredient> { ingredients[0], ingredients[2] } },
                new MagicElixir { Name = "Elixir 4", Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[2] } },
            };

            var magicElixirService = new MagicElixirService(appSettings, ingredients, elixirs);

            magicElixirService.AddMyIngredient("ingredient1");
            magicElixirService.AddMyIngredient("ingredient2");

            // Act
            var possibleElixirs = magicElixirService.GetPossibleElixirs();

            // Assert
            Assert.NotNull(possibleElixirs);
            Assert.Equal(expectedElixirCount, possibleElixirs.Count);
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 1");
            Assert.DoesNotContain(possibleElixirs, elixir => elixir.Name == "Elixir 4");
        }

        [Fact]
        public void AddAvailableIngredient_AddIngredientWithUpperCase_ShouldReturnElixir()
        {
            // Arrange
            var expectedElixirCount = 1;
            var appSettings = new AppSettings { IncludeElixirWithNoIngredients = false };
            var ingredient1 = new Ingredient { Id = "ingredient1", Name = "Ingredient 1" };
            var ingredient2 = new Ingredient { Id = "ingredient2", Name = "Ingredient 2" };
            var ingredient3 = new Ingredient { Id = "ingredient3", Name = "Ingredient 3" };
            var ingredients = new List<Ingredient>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var elixirs = new List<MagicElixir>
            {
                new MagicElixir { Name = "Elixir 1", Ingredients = new List<Ingredient> { ingredient1, ingredient3 } }
            };

            var magicElixirService = new MagicElixirService(appSettings, ingredients, elixirs);

            // Act
            var result1 = magicElixirService.AddMyIngredient(ingredient1.Id);
            var result2 = magicElixirService.AddMyIngredient(ingredient3.Id.ToUpper());

            var possibleElixirs = magicElixirService.GetPossibleElixirs();

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.Equal(expectedElixirCount, possibleElixirs.Count);
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 1");
        }

        [Fact]
        public void AddAvailableIngredient_AddManyIngredient_ShouldReturnElixir()
        {
            // Arrange
            var expectedElixirCount = 3;
            var appSettings = new AppSettings { IncludeElixirWithNoIngredients = false };
            var ingredient1 = new Ingredient { Id = "ingredient1", Name = "Ingredient 1" };
            var ingredient2 = new Ingredient { Id = "ingredient2", Name = "Ingredient 2" };
            var ingredient3 = new Ingredient { Id = "ingredient3", Name = "Ingredient 3" };
            var ingredients = new List<Ingredient>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var elixirs = new List<MagicElixir>
            {
                new MagicElixir { Name = "Elixir 1", Ingredients = new List<Ingredient> { ingredient1 } },
                new MagicElixir { Name = "Elixir 2", Ingredients = new List<Ingredient> { ingredient2 } },
                new MagicElixir { Name = "Elixir 3", Ingredients = new List<Ingredient> { ingredient3 } },
            };

            var magicElixirService = new MagicElixirService(appSettings, ingredients, elixirs);

            // Act
            var result1 = magicElixirService.AddMyIngredient(ingredient1.Id);
            var result2 = magicElixirService.AddMyIngredient(ingredient2.Id);
            var result3 = magicElixirService.AddMyIngredient(ingredient3.Id.ToLower());

            var possibleElixirs = magicElixirService.GetPossibleElixirs();

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.Equal(expectedElixirCount, possibleElixirs.Count);
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 1");
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 2");
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 3");
        }

        [Fact]
        public void AddAvailableIngredient_IncludingElixirWithNoIngredients_ShouldReturnElixir()
        {
            // Arrange
            var expectedElixirCount = 4;
            var appSettings = new AppSettings { IncludeElixirWithNoIngredients = true };
            var ingredient1 = new Ingredient { Id = "ingredient1", Name = "Ingredient 1" };
            var ingredient2 = new Ingredient { Id = "ingredient2", Name = "Ingredient 2" };
            var ingredient3 = new Ingredient { Id = "ingredient3", Name = "Ingredient 3" };
            var ingredients = new List<Ingredient>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var elixirs = new List<MagicElixir>
            {
                new MagicElixir { Name = "Elixir 1", Ingredients = new List<Ingredient> { ingredient1 } },
                new MagicElixir { Name = "Elixir 2", Ingredients = new List<Ingredient> { ingredient2 } },
                new MagicElixir { Name = "Elixir 3", Ingredients = new List<Ingredient> { ingredient3 } },
                new MagicElixir { Name = "Elixir 4", Ingredients = new List<Ingredient>() },
            };

            var magicElixirService = new MagicElixirService(appSettings, ingredients, elixirs);

            // Act
            var result1 = magicElixirService.AddMyIngredient(ingredient1.Id);
            var result2 = magicElixirService.AddMyIngredient(ingredient2.Id);
            var result3 = magicElixirService.AddMyIngredient(ingredient3.Id.ToLower());

            var possibleElixirs = magicElixirService.GetPossibleElixirs();

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.Equal(expectedElixirCount, possibleElixirs.Count);
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 1");
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 2");
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 3");
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 4");
        }

        [Fact]
        public void GetPossibleElixirs_AddingNoIngredientAndIncludingElixirWithNoIngredients_ShouldReturnElixir()
        {
            // Arrange
            var expectedElixirCount = 1;
            var appSettings = new AppSettings { IncludeElixirWithNoIngredients = true };
            var ingredient1 = new Ingredient { Id = "ingredient1", Name = "Ingredient 1" };
            var ingredient2 = new Ingredient { Id = "ingredient2", Name = "Ingredient 2" };
            var ingredient3 = new Ingredient { Id = "ingredient3", Name = "Ingredient 3" };
            var ingredients = new List<Ingredient>
            {
                ingredient1,
                ingredient2,
                ingredient3
            };

            var elixirs = new List<MagicElixir>
            {
                new MagicElixir { Name = "Elixir 1", Ingredients = new List<Ingredient> { ingredient1 } },
                new MagicElixir { Name = "Elixir 2", Ingredients = new List<Ingredient>() },
            };

            var magicElixirService = new MagicElixirService(appSettings, ingredients, elixirs);

            // Act
            var possibleElixirs = magicElixirService.GetPossibleElixirs();

            // Assert
            Assert.Equal(expectedElixirCount, possibleElixirs.Count);
            Assert.DoesNotContain(possibleElixirs, elixir => elixir.Name == "Elixir 1");
            Assert.Contains(possibleElixirs, elixir => elixir.Name == "Elixir 2");
        }
    }
}
