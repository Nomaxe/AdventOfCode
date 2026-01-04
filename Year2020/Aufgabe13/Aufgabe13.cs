using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe13 : IAufgabe
{
    private readonly string[] _input;
    public Aufgabe13()
    {
        _input = Utilities.ReadInput(2020, 13);
    }

    public string Calc()
    {
        var arrivalTime = int.Parse(_input[0]);
        var busIds = _input[1].GetUnsignedNumbers();
        var bestBusArrivalTime = int.MaxValue;
        var waitTime = 0;
        var bestBusId = 0;

        foreach (var busId in busIds)
        {
            var number = arrivalTime / busId;
            if (arrivalTime % busId != 0)
            {
                number++;
            }
            number *= busId;

            if (number < bestBusArrivalTime)
            {
                bestBusArrivalTime = number;
                waitTime = number - arrivalTime;
                bestBusId = busId;
            }
        }

        return (waitTime * bestBusId).ToString();
    }
}
