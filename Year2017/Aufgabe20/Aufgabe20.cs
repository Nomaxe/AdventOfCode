using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe20 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe20()
    {
        _input = Utilities.ReadInput(2017, 20);
    }

    public string Calc()
    {
        int minAcceleration = int.MaxValue;
        int minVelocity = int.MaxValue;
        int minDistance = int.MaxValue;
        int particle = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            var split = _input[i].GetNumbers();

            var acceleration = new Point3D(split[6], split[7], split[8]).GetManhattenDistanceToZero();
            if (acceleration < minAcceleration)
            {
                minAcceleration = acceleration;
                minVelocity = new Point3D(split[3], split[4], split[5]).GetManhattenDistanceToZero();
                minDistance = new Point3D(split[0], split[1], split[2]).GetManhattenDistanceToZero();
                particle = i;
            }
            else if (acceleration == minAcceleration)
            {
                var velocity = new Point3D(split[3], split[4], split[5]).GetManhattenDistanceToZero();
                if (velocity < minVelocity)
                {
                    minAcceleration = acceleration;
                    minVelocity = velocity;
                    minDistance = new Point3D(split[0], split[1], split[2]).GetManhattenDistanceToZero();
                    particle = i;
                }
                else if (velocity == minVelocity)
                {
                    var distance = new Point3D(split[0], split[1], split[2]).GetManhattenDistanceToZero();
                    if (distance < minDistance)
                    {
                        minAcceleration = acceleration;
                        minVelocity = velocity;
                        minDistance = distance;
                        particle = i;
                    }
                }
            }
        }

        return particle.ToString();
    }
}
