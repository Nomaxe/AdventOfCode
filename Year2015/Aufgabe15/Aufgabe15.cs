using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe15 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe15()
    {
        _input = Utilities.ReadInput(2015, 15);
    }

    public string Calc()
    {
        var ingredients = _input.Select(x => x.GetNumbers()).ToArray(); //assume there are always 4
        var max = 0;

        for (int i = 0; i <= 100; i++)
        {
            for (int j = 0; i + j <= 100; j++)
            {
                for (int k = 0; i + j + k <= 100; k++)
                {
                    int l = 100 - (i + j + k);

                    var capacity = GetNumber(ingredients, 0, i, j, k, l);
                    var durability = GetNumber(ingredients, 1, i, j, k, l);
                    var flavor = GetNumber(ingredients, 2, i, j, k, l);
                    var texture = GetNumber(ingredients, 3, i, j, k, l);

                    var result = capacity * durability * flavor * texture;
                    max = int.Max(max, result);
                }
            }
        }

        return max.ToString();
    }

    private static int GetNumber(int[][] ingredients, int number, int count1, int count2, int count3, int count4)
    {
        var result = count1 * ingredients[0][number] +
                     count2 * ingredients[1][number] +
                     count3 * ingredients[2][number] +
                     count4 * ingredients[3][number];

        return int.Max(result, 0);
    }
}
