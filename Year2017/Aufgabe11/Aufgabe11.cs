using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe11 : IAufgabe
{
    private readonly string _input;

    public Aufgabe11()
    {
        _input = Utilities.ReadInput(2017, 11)[0];
    }

    public string Calc()
    {
        int posX = 0;
        int posY = 0;

        foreach (var move in _input.Split(','))
        {
            switch (move)
            {
                case "n":
                    posX -= 2;
                    break;
                case "ne":
                    posX--;
                    posY++;
                    break;
                case "se":
                    posX++;
                    posY++;
                    break;
                case "s":
                    posX += 2;
                    break;
                case "sw":
                    posX++;
                    posY--;
                    break;
                case "nw":
                    posX--;
                    posY--;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        posX = int.Abs(posX);
        posY = int.Abs(posY);

        if (posX > posY)
        {
            return (posY + ((posX - posY) / 2)).ToString();
        }
        else
        {
            return (posX + (posY - posX)).ToString();
        }
    }
}
