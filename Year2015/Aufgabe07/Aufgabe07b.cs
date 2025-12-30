using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe07b : IAufgabe
{
    private List<string> _input;
    private readonly string[] _inputOrig;
    private readonly Dictionary<string, ushort> _values = [];

    public Aufgabe07b()
    {
        _inputOrig = Utilities.ReadInput(2015, 7);
        _input = [.. _inputOrig];
        _values = new(_input.Count);
    }

    public string Calc()
    {
        CalcWires();

        _input = [.. _inputOrig];
        for (int i = 0; i < _input.Count; i++)
        {
            if (_input[i].EndsWith(" b"))
            {
                _input[i] = $"{_values["a"]} -> b";
                break;
            }
        }

        _values.Clear();
        CalcWires();

        return _values["a"].ToString();
    }

    private void CalcWires()
    {
        do
        {
            List<string> nextInput = new(_input.Count);

            foreach (var line in _input)
            {
                if (line.Contains("AND"))
                {
                    var numbers = GetNumbers(line);
                    if (!numbers.HasValue)
                    {
                        nextInput.Add(line);
                        continue;
                    }

                    var number1 = numbers.Value.Number1;
                    var number2 = numbers.Value.Number2;


                    _values.Add(line[(line.LastIndexOf(' ') + 1)..], (ushort)(number1 & number2));
                }
                else if (line.Contains("OR"))
                {
                    var numbers = GetNumbers(line);
                    if (!numbers.HasValue)
                    {
                        nextInput.Add(line);
                        continue;
                    }

                    var number1 = numbers.Value.Number1;
                    var number2 = numbers.Value.Number2;

                    _values.Add(line[(line.LastIndexOf(' ') + 1)..], (ushort)(number1 | number2));
                }
                else if (line.Contains("LSHIFT"))
                {
                    var numbers = GetNumbersShift(line);
                    if (!numbers.HasValue)
                    {
                        nextInput.Add(line);
                        continue;
                    }

                    var number1 = numbers.Value.Number1;
                    var number2 = numbers.Value.Number2;

                    _values.Add(line[(line.LastIndexOf(' ') + 1)..], (ushort)(number1 << number2));
                }
                else if (line.Contains("RSHIFT"))
                {
                    var numbers = GetNumbersShift(line);
                    if (!numbers.HasValue)
                    {
                        nextInput.Add(line);
                        continue;
                    }

                    var number1 = numbers.Value.Number1;
                    var number2 = numbers.Value.Number2;

                    _values.Add(line[(line.LastIndexOf(' ') + 1)..], (ushort)(number1 >> number2));
                }
                else if (line.Contains("NOT"))
                {
                    var number = GetNumber(line);
                    if (!number.HasValue)
                    {
                        nextInput.Add(line);
                        continue;
                    }

                    _values.Add(line[(line.LastIndexOf(' ') + 1)..], (ushort)~number.Value);
                }
                else
                {
                    var index = line.IndexOf(' ');
                    var value = line[..index];
                    if (ushort.TryParse(value, out var number))
                    {
                        _values.Add(line[(index + 4)..], number);
                    }
                    else
                    {
                        if (_values.TryGetValue(value, out number))
                        {
                            _values.Add(line[(index + 4)..], number);
                        }
                        else
                        {
                            nextInput.Add(line);
                        }
                    }
                }
            }

            _input = nextInput;
        } while (_input.Count > 0);
    }

    private (ushort Number1, ushort Number2)? GetNumbers(string line)
    {
        var index = line.IndexOf(' ');
        if (ushort.TryParse(line[..index], out var number1))
        {

        }
        else if (!_values.TryGetValue(line[..index], out number1))
        {
            return null;
        }

        index = line.IndexOf(' ', index + 1) + 1;
        var endIndex = line.IndexOf('-') - 1;
        if (!_values.TryGetValue(line[index..endIndex], out var number2))
        {
            return null;
        }

        return (number1, number2);
    }

    private (ushort Number1, ushort Number2)? GetNumbersShift(string line)
    {
        var index = line.IndexOf(' ');
        if (!_values.TryGetValue(line[..index], out var number1))
        {
            return null;
        }

        index = line.IndexOf(' ', index + 1) + 1;
        var endIndex = line.IndexOf('-') - 1;

        return (number1, ushort.Parse(line[index..endIndex]));
    }

    private ushort? GetNumber(string line)
    {
        var index = line.IndexOf(' ') + 1;
        var endIndex = line.IndexOf(' ', index);

        if (!_values.TryGetValue(line[index..endIndex], out var number))
        {
            return null;
        }

        return number;
    }
}
