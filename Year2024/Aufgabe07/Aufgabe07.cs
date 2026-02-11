using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe07 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Equation> _equations;
    private readonly List<ulong> _results = [];

    public Aufgabe07()
    {
        _input = Utilities.ReadInput(2024, 7);
        _equations = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');
            var equationResult = ulong.Parse(split[0][..^1]);
            List<ulong> numbers = [];
            for (int i = 1; i < split.Length; i++)
            {
                numbers.Add(ulong.Parse(split[i]));
            }

            _equations.Add(new() { Result = equationResult, Numbers = numbers });
        }

        ulong result = 0;

        foreach (var equation in _equations)
        {
            _results.Clear();

            if (GetResult(equation))
            {
                result += equation.Result;
            }
        }

        return result.ToString();
    }

    private bool GetResult(Equation equation)
    {
        char[] operators = new char[equation.Numbers.Count - 1];
        for (int i = 0; i < operators.Length; i++)
        {
            operators[i] = '+';
        }

        for (int count = 0; count < Math.Pow(2, operators.Length); count++)
        {
            ulong result = equation.Numbers[0];
            for (int i = 0; i < operators.Length; i++)
            {
                var number = equation.Numbers[i + 1];

                if (operators[i] == '+')
                {
                    result += number;
                }
                else
                {
                    result *= number;
                }
            }

            _results.Add(result);

            if (result == equation.Result)
            {
                return true;
            }

            for (int i = 0; i < operators.Length; i++)
            {
                if (operators[i] == '+')
                {
                    operators[i] = '*';
                    break;
                }

                operators[i] = '+';
            }
        }

        return false;
    }

    private class Equation
    {
        public ulong Result;
        public List<ulong> Numbers = [];
    }
}
