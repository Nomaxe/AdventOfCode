using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe11b : IAufgabe
{
    private readonly string _input;
    private int _posX;
    private int _posY;

    public Aufgabe11b()
    {
        _input = Utilities.ReadInputAsString(2017, 11);
    }

    public string Calc()
    {
        int maxDistance = 0;

        foreach (var move in _input.Split(','))
        {
            switch (move)
            {
                case "n":
                    _posX -= 2;
                    break;
                case "ne":
                    _posX--;
                    _posY++;
                    break;
                case "se":
                    _posX++;
                    _posY++;
                    break;
                case "s":
                    _posX += 2;
                    break;
                case "sw":
                    _posX++;
                    _posY--;
                    break;
                case "nw":
                    _posX--;
                    _posY--;
                    break;
                default:
                    throw new NotImplementedException();
            }

            maxDistance = int.Max(maxDistance, GetCurrentDistance());
        }

        return maxDistance.ToString();
    }

    private int GetCurrentDistance()
    {
        int posX = int.Abs(_posX);
        int posY = int.Abs(_posY);

        if (posX > posY)
        {
            return posY + ((posX - posY) / 2);
        }
        else
        {
            return posX + (posY - posX);
        }
    }
}
