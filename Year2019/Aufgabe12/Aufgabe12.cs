using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe12 : IAufgabe
{
    private readonly string[] _input;
    private readonly Moon[] _moons;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2019, 12);
        _moons = new Moon[_input.Length];
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            _moons[i] = new(_input[i]);
        }

        for (int step = 0; step < 1000; step++)
        {
            Moon[] newMoons = _moons.ToArray();

            for (int i = 0; i < _moons.Length; i++)
            {
                for (int j = i + 1; j < _moons.Length; j++)
                {
                    if (_moons[i].Position.X > _moons[j].Position.X)
                    {
                        newMoons[i].Velocity = new(newMoons[i].Velocity.X - 1, newMoons[i].Velocity.Y, newMoons[i].Velocity.Z);
                        newMoons[j].Velocity = new(newMoons[j].Velocity.X + 1, newMoons[j].Velocity.Y, newMoons[j].Velocity.Z);
                    }
                    else if (_moons[i].Position.X < _moons[j].Position.X)
                    {
                        newMoons[i].Velocity = new(newMoons[i].Velocity.X + 1, newMoons[i].Velocity.Y, newMoons[i].Velocity.Z);
                        newMoons[j].Velocity = new(newMoons[j].Velocity.X - 1, newMoons[j].Velocity.Y, newMoons[j].Velocity.Z);
                    }

                    if (_moons[i].Position.Y > _moons[j].Position.Y)
                    {
                        newMoons[i].Velocity = new(newMoons[i].Velocity.X, newMoons[i].Velocity.Y - 1, newMoons[i].Velocity.Z);
                        newMoons[j].Velocity = new(newMoons[j].Velocity.X, newMoons[j].Velocity.Y + 1, newMoons[j].Velocity.Z);
                    }
                    else if (_moons[i].Position.Y < _moons[j].Position.Y)
                    {
                        newMoons[i].Velocity = new(newMoons[i].Velocity.X, newMoons[i].Velocity.Y + 1, newMoons[i].Velocity.Z);
                        newMoons[j].Velocity = new(newMoons[j].Velocity.X, newMoons[j].Velocity.Y - 1, newMoons[j].Velocity.Z);
                    }

                    if (_moons[i].Position.Z > _moons[j].Position.Z)
                    {
                        newMoons[i].Velocity = new(newMoons[i].Velocity.X, newMoons[i].Velocity.Y, newMoons[i].Velocity.Z - 1);
                        newMoons[j].Velocity = new(newMoons[j].Velocity.X, newMoons[j].Velocity.Y, newMoons[j].Velocity.Z + 1);
                    }
                    else if (_moons[i].Position.Z < _moons[j].Position.Z)
                    {
                        newMoons[i].Velocity = new(newMoons[i].Velocity.X, newMoons[i].Velocity.Y, newMoons[i].Velocity.Z + 1);
                        newMoons[j].Velocity = new(newMoons[j].Velocity.X, newMoons[j].Velocity.Y, newMoons[j].Velocity.Z - 1);
                    }
                }
            }

            for (int i = 0; i < newMoons.Length; i++)
            {
                _moons[i] = newMoons[i].Move();
            }
        }

        return _moons.Sum(x => x.GetEnergy()).ToString();
    }

    private struct Moon
    {
        public Point3D Position { get; private set; }
        public Point3D Velocity { get; set; }

        public Moon(string line)
        {
            var numbers = line.GetNumbers();
            Position = new(numbers);
            Velocity = new(0, 0, 0);
        }

        public readonly Moon Move()
        {
            Moon moon = new()
            {
                Position = Position.Move(Velocity),
                Velocity = Velocity
            };
            return moon;
        }

        public readonly int GetEnergy()
        {
            return (int.Abs(Position.X) + int.Abs(Position.Y) + int.Abs(Position.Z)) * (int.Abs(Velocity.X) + int.Abs(Velocity.Y) + int.Abs(Velocity.Z));
        }
    }
}
