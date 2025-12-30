using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe12b : IAufgabe
{
    private readonly Moon[] _moons;
    private readonly Moon[] _startPositions;

    public Aufgabe12b()
    {
        var input = Utilities.ReadInput(2019, 12);
        _moons = new Moon[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            _moons[i] = new(input[i]);
        }

        _startPositions = _moons.ToArray();
    }

    public string Calc()
    {
        ulong step = 0;
        ulong? x = null;
        ulong? y = null;
        ulong? z = null;

        while (!x.HasValue || !y.HasValue || !z.HasValue)
        {
            step++;
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

            if (!x.HasValue &&
                _moons[0].Position.X == _startPositions[0].Position.X &&
                _moons[0].Velocity.X == 0 &&
                _moons[1].Position.X == _startPositions[1].Position.X &&
                _moons[1].Velocity.X == 0 &&
                _moons[2].Position.X == _startPositions[2].Position.X &&
                _moons[2].Velocity.X == 0 &&
                _moons[3].Position.X == _startPositions[3].Position.X &&
                _moons[3].Velocity.X == 0)
            {
                x = step;
            }

            if (!y.HasValue &&
                _moons[0].Position.Y == _startPositions[0].Position.Y &&
                _moons[0].Velocity.Y == 0 &&
                _moons[1].Position.Y == _startPositions[1].Position.Y &&
                _moons[1].Velocity.Y == 0 &&
                _moons[2].Position.Y == _startPositions[2].Position.Y &&
                _moons[2].Velocity.Y == 0 &&
                _moons[3].Position.Y == _startPositions[3].Position.Y &&
                _moons[3].Velocity.Y == 0)
            {
                y = step;
            }

            if (!z.HasValue &&
                _moons[0].Position.Z == _startPositions[0].Position.Z &&
                _moons[0].Velocity.Z == 0 &&
                _moons[1].Position.Z == _startPositions[1].Position.Z &&
                _moons[1].Velocity.Z == 0 &&
                _moons[2].Position.Z == _startPositions[2].Position.Z &&
                _moons[2].Velocity.Z == 0 &&
                _moons[3].Position.Z == _startPositions[3].Position.Z &&
                _moons[3].Velocity.Z == 0)
            {
                z = step;
            }
        }

        return MathEnhancement.GetLowestCommonMultiple([x.Value, y.Value, z.Value]).ToString();
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

        public readonly override int GetHashCode()
        {
            return HashCode.Combine(Position, Velocity);
        }
    }
}
