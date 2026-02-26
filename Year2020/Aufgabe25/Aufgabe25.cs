using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe25 : IAufgabe
{
    private readonly ulong _publicKeyCard;
    private readonly ulong _publicKeyDoor;

    public Aufgabe25()
    {
        var input = Utilities.ReadInput(2020, 25);
        _publicKeyCard = ulong.Parse(input[0]);
        _publicKeyDoor = ulong.Parse(input[1]);
    }

    public string Calc()
    {
        ulong subjectNumber = 1;

        for (ulong i = 1; true; i++)
        {
            subjectNumber *= 7;
            subjectNumber %= 20201227;

            if (_publicKeyCard == subjectNumber)
            {
                return GetPrivateKey(_publicKeyDoor, i).ToString();
            }
            if (_publicKeyDoor == subjectNumber)
            {
                return GetPrivateKey(_publicKeyCard, i).ToString();
            }
        }
    }

    private static ulong GetPrivateKey(ulong subjectnumber, ulong loopsize)
    {
        ulong value = 1;

        for (ulong i = 0; i < loopsize; i++)
        {
            value *= subjectnumber;
            value %= 20201227;
        }

        return value;
    }
}
