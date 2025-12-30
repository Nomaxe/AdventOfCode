using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe23 : IAufgabe
{
    private readonly IntCode[] _intCode;
    private readonly string[] _input;

    public Aufgabe23()
    {
        _intCode = new IntCode[50];
        _input = Utilities.ReadInput(2019, 23);
    }

    public string Calc()
    {
        for (int i = 0; i < _intCode.Length; i++)
        {
            _intCode[i] = new(_input)
            {
                WaitOnInput = true
            };
            _intCode[i].AddInput(i);
        }

        while (true)
        {
            for (int i = 0; i < _intCode.Length; i++)
            {
                if (!_intCode[i].HasInput)
                {
                    _intCode[i].AddInput(-1);
                }

                _intCode[i].Calc();

                var output = _intCode[i].Out;
                for (int j = 0; j < output.Count; j += 3)
                {
                    if (output[j] == 255)
                    {
                        return output[j + 2].ToString();
                    }

                    var intCodeMachine = _intCode[output[j]];
                    intCodeMachine.AddInput(output[j + 1]);
                    intCodeMachine.AddInput(output[j + 2]);
                }

                _intCode[i].ClearOut();
            }
        }
    }
}
