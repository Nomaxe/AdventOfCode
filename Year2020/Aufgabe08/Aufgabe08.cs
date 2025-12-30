using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe08 : IAufgabe
{
    private readonly string[] _input;
    private int _accumulator = 0;
    private int _pointer = 0;
    private readonly HashSet<int> _alreadyExecuted = [];

    public Aufgabe08()
    {
        _input = Utilities.ReadInput(2020, 8);
    }

    public string Calc()
    {
        do
        {
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

        return _accumulator.ToString();
    }
}
