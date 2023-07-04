namespace Elixir.App.Objects
{
    using Newtonsoft.Json;

    public class MagicElixir
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Effect { get; set; }
        public string SideEffects { get; set; }
        public string Characteristics { get; set; }
        public string Time { get; set; }
        public string Difficulty { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Inventor> Inventors { get; set; }
        public string Manufacturer { get; set; }
    }
}