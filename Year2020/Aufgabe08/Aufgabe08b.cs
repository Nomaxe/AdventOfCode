using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe08b : IAufgabe
{
    private readonly string[] _input;
    private readonly string[] _orginal;
    private int _accumulator = 0;
    private int _pointer = 0;
    private readonly HashSet<int> _alreadyExecuted = [];

    public Aufgabe08b()
    {
        _orginal = Utilities.ReadInput(2020, 8);
        _input = new string[_orginal.Length];
    }

    public string Calc()
    {
        for (int i = 0; i < _orginal.Length; i++)
        {
            var instruction = _orginal[i][0..3];
            if (instruction == "nop")
            {
                Array.Copy(_orginal, _input, _orginal.Length);
                _input[i] = $"jmp {_orginal[i][5..]}";
            }
            else if (instruction == "jmp")
            {
                Array.Copy(_orginal, _input, _orginal.Length);
                _input[i] = $"nop {_orginal[i][5..]}";
            }
            else
            {
                continue;
            }

            _accumulator = 0;
            _pointer = 0;
            _alreadyExecuted.Clear();
            if (!IsLoop())
            {
                break;
            }
        }

        return _accumulator.ToString();
    }

    public bool IsLoop()
    {
        do
        {
            if (_pointer >= _input.Length)
            {
                return false;
            }

            _alreadyExecuted.Add(_pointer);

            switch (_input[_pointer][0..3])
            {
                case "acc":
                    _accumulator += int.Parse(_input[_pointer][4..]);
                    _pointer++;
                    break;
                case "jmp":
                    _pointer += int.Parse(_input[_pointer][4..]);
                    break;
                case "nop":
                    _pointer++;
                    break;
                default:
                    throw new NotImplementedException();
            }
        } while (!_alreadyExecuted.Contains(_pointer));

        return true;
    }
}
