using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe23b : IAufgabe
{
    private readonly IntCode[] _intCode;
    private readonly string[] _input;

    public Aufgabe23b()
    {
        _intCode = new IntCode[50];
        _input = Utilities.ReadInput(2019, 23);
    }

    public string Calc()
    {
        long xNAT = 0;
        long yNAT = 0;
        long lastYNAT = 0;

        for (int i = 0; i < _intCode.Length; i++)
        {
            _intCode[i] = new(_input)
            {
                WaitOnInput = true
            };
            _intCode[i].AddInput(i);
        }

        bool packetSend = true; //erster Durchlauf versendet nichts
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
                    packetSend = true;
                    if (output[j] == 255)
                    {
                        xNAT = output[j + 1];
                        yNAT = output[j + 2];
                        continue;
                    }

                    var intCodeMachine = _intCode[output[j]];
                    intCodeMachine.AddInput(output[j + 1]);
                    intCodeMachine.AddInput(output[j + 2]);
                }

                _intCode[i].ClearOut();
            }

            if (!packetSend)
            {
                if (lastYNAT == yNAT)
                {
                    return lastYNAT.ToString();
                }

                _intCode[0].AddInput(xNAT);
                _intCode[0].AddInput(yNAT);

                lastYNAT = yNAT;
            }

            packetSend = false;
        }
    }
}
