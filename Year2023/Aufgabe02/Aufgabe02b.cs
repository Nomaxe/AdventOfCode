using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2023, 2);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            result += CheckGame(line);
        }

        return result.ToString();
    }

    private static int CheckGame(string input)
    {
        int maxRed = 0;
        int maxGreen = 0;
        int maxBlue = 0;

        int colon = input.IndexOf(':');

        var games = input[(colon + 2)..].Split("; ");
        foreach (var game in games)
        {
            var split = game.Split(", ");
            foreach (var dice in split)
            {
                var diceSplit = dice.Split(' ');
                var diceAmount = int.Parse(diceSplit[0]);
                switch (diceSplit[1])
                {
                    case "red":
                        maxRed = int.Max(maxRed, diceAmount);
                        break;
                    case "green":
                        maxGreen = int.Max(maxGreen, diceAmount);
                        break;
                    case "blue":
                        maxBlue = int.Max(maxBlue, diceAmount);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        return maxRed * maxGreen * maxBlue;
    }
}
