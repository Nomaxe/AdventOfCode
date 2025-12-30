using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe19 : IAufgabe
{
    private readonly List<Machine> _machines = [];
    private readonly Dictionary<string, string[]> _workflows = [];

    public Aufgabe19()
    {
        var input = Utilities.ReadInput(2023, 19);
        bool whiteline = false;
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                whiteline = true;
                continue;
            }

            if (whiteline)
            {
                var split = line.GetNumbers();
                _machines.Add(new(split[0], split[1], split[2], split[3]));
            }
            else
            {
                var index = line.IndexOf('{');
                _workflows.Add(line[..index], line[(index + 1)..^1].Split(','));
            }
        }
    }

    public string Calc()
    {
        string workflow;
        int result = 0;

        foreach (var machine in _machines)
        {
            workflow = "in";

            do
            {
                workflow = GetResult(machine, _workflows[workflow]);
            } while (workflow != "A" && workflow != "R");

            if (workflow == "A")
            {
                result += machine.X + machine.M + machine.A + machine.S;
            }
        }

        return result.ToString();
    }

    private string GetResult(Machine machine, string[] workflow)
    {
        foreach (var check in workflow)
        {
            if (!check.Contains(':'))
            {
                return check;
            }

            var value = GetValue(machine, check[0]);
            var sign = check[1];
            var index = check.IndexOf(':');
            var number = int.Parse(check[2..index]);
            if (CheckValueNumber(value, sign, number))
            {
                return check[(index + 1)..];
            }
        }

        throw new NotImplementedException();
    }

    private static int GetValue(Machine machine, char value)
    {
        return value switch
        {
            'x' => machine.X,
            'm' => machine.M,
            'a' => machine.A,
            's' => machine.S,
            _ => throw new NotImplementedException()
        };
    }

    private static bool CheckValueNumber(int value, char sign, int number)
    {
        return sign switch
        {
            '>' => value > number,
            '<' => value < number,
            _ => throw new NotImplementedException()
        };
    }

    private record struct Machine(int X, int M, int A, int S)
    {

    }
}
