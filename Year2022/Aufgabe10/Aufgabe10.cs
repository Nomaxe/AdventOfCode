using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe10 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe10()
    {
        _input = Utilities.ReadInput(2022, 10);
    }

    public string Calc()
    {
        int x = 1;
        int cycle = 0;
        int signalStrength = 0;

        foreach (var line in _input)
        {
            int newX = x;

            switch (line[..4])
            {
                case "noop":
                    cycle++;
                    if (IsRelevantCycle(cycle))
                    {
                        signalStrength += cycle * x;
                    }
                    break;
                case "addx":
                    newX += int.Parse(line[5..]);
                    cycle += 2;

                    if (IsRelevantCycle(cycle - 1))
                    {
                        signalStrength += (cycle - 1) * x;
                    }
                    else if (IsRelevantCycle(cycle))
                    {
                        signalStrength += cycle * x;
                    }

                    x = newX;

                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return signalStrength.ToString();
    }

    private static bool IsRelevantCycle(int cycle)
    {
        return (cycle - 20) % 40 == 0;
    }
}
