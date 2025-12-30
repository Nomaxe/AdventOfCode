using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
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
        const int MaxRed = 12;
        const int MaxGreen = 13;
        const int MaxBlue = 14;

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
                        if (diceAmount > MaxRed)
                        {
                            return 0;
                        }
                        break;
                    case "green":
                        if (diceAmount > MaxGreen)
                        {
                            return 0;
                        }
                        break;
                    case "blue":
                        if (diceAmount > MaxBlue)
                        {
                            return 0;
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        return int.Parse(input[5..colon]);
    }
}
