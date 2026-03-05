using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2021;

internal class Aufgabe16b : IAufgabe
{
    private readonly string _input;
    private string _numbers;
    private int _currentIndex;

    public Aufgabe16b()
    {
        _input = Utilities.ReadInputAsString(2021, 16);
        _numbers = string.Empty;
        _currentIndex = 0;
    }

    public string Calc()
    {
        _numbers = GetNumberString();

        var result = CalcPackage();

        return result.ToString();
    }

    private long CalcPackage()
    {
        _currentIndex += 3;

        var type = _numbers[_currentIndex..(_currentIndex + 3)];
        _currentIndex += 3;

        if (type == "100")
        {
            List<bool> number = new();
            bool continueReading = true;
            while (continueReading)
            {
                continueReading = _numbers[_currentIndex] == '1';
                number.AddRange(_numbers[(_currentIndex + 1)..(_currentIndex + 5)].Select(x => x == '1'));
                _currentIndex += 5;
            }

            return number.GetDecimalNumber();
        }

        var lengthType = _numbers[_currentIndex];
        _currentIndex++;
        List<long> numbers = new();

        if (lengthType == '0')
        {
            var length = (int)_numbers[_currentIndex..(_currentIndex + 15)].Select(x => x == '1').ToList().GetDecimalNumber();
            _currentIndex += 15;

            var endIndex = _currentIndex + length;
            do
            {
                numbers.Add(CalcPackage());
            } while (_currentIndex < endIndex);
        }
        else
        {
            var count = _numbers[_currentIndex..(_currentIndex + 11)].Select(x => x == '1').ToList().GetDecimalNumber();
            _currentIndex += 11;

            for (long i = 0; i < count; i++)
            {
                numbers.Add(CalcPackage());
            }
        }

        return type switch
        {
            "000" => numbers.Sum(),
            "001" => numbers.Mul(),
            "010" => numbers.Min(),
            "011" => numbers.Max(),
            "101" => numbers[0] > numbers[1] ? 1 : 0,
            "110" => numbers[0] < numbers[1] ? 1 : 0,
            "111" => numbers[0] == numbers[1] ? 1 : 0,
            _ => throw new NotImplementedException()
        };
    }

    private string GetNumberString()
    {
        StringBuilder builder = new(_input.Length * 4);

        foreach (var character in _input)
        {
            switch (character)
            {
                case '0':
                    builder.Append("0000");
                    break;
                case '1':
                    builder.Append("0001");
                    break;
                case '2':
                    builder.Append("0010");
                    break;
                case '3':
                    builder.Append("0011");
                    break;
                case '4':
                    builder.Append("0100");
                    break;
                case '5':
                    builder.Append("0101");
                    break;
                case '6':
                    builder.Append("0110");
                    break;
                case '7':
                    builder.Append("0111");
                    break;
                case '8':
                    builder.Append("1000");
                    break;
                case '9':
                    builder.Append("1001");
                    break;
                case 'A':
                    builder.Append("1010");
                    break;
                case 'B':
                    builder.Append("1011");
                    break;
                case 'C':
                    builder.Append("1100");
                    break;
                case 'D':
                    builder.Append("1101");
                    break;
                case 'E':
                    builder.Append("1110");
                    break;
                case 'F':
                    builder.Append("1111");
                    break;
            }
        }

        return builder.ToString();
    }
}
