using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe21 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Recipe> _recipes;

    public Aufgabe21()
    {
        _input = Utilities.ReadInput(2020, 21);
        _recipes = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            _recipes.Add(new(line));
        }

        DictionaryHashSet<string, string> allergenList = new();

        foreach (var recipe in _recipes)
        {
            foreach (var allergen in recipe.Allergens)
            {
                if (allergenList.ContainsKey(allergen))
                {
                    allergenList.IntersectWith(allergen, recipe.Ingredients);
                }
                else
                {
                    allergenList.AddRange(allergen, recipe.Ingredients);
                }
            }
        }

        allergenList.RemoveDuplicatesUntilSingleItem();
        var ingredientWithAllergens = allergenList.GetAllItems().ToHashSet();

        int count = 0;
        foreach (var recipe in _recipes)
        {
            foreach (var ingredient in recipe.Ingredients)
            {
                if (!ingredientWithAllergens.Contains(ingredient))
                {
                    count++;
                }
            }
        }

        return count.ToString();
    }

    private readonly struct Recipe
    {
        public HashSet<string> Ingredients { get; private init; }
        public HashSet<string> Allergens { get; private init; }

        public Recipe(string line)
        {
            int index = line.IndexOf('(');
            Ingredients = line[..(index - 1)].Split(' ').ToHashSet();
            Allergens = line[(index + 10)..^1].Split(", ").ToHashSet();
        }
    }
}
