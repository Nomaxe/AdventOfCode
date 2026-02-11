using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe20b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Particle> _particles;

    public Aufgabe20b()
    {
        _input = Utilities.ReadInput(2017, 20);
        _particles = new(_input.Length);
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            _particles.Add(new(i, _input[i]));
        }

        for (int i = 0; i < 1000; i++)
        {
            DictionaryCounter<Point3D> points = new(_particles.Count);
            foreach (var particle in _particles)
            {
                particle.Move();
                points.Add(particle.Position);
            }

            foreach (var collision in points.Where(x => x.Value >= 2))
            {
                _particles.RemoveAll(x => x.Position == collision.Key);
            }
        }

        return _particles.Count.ToString();
    }

    private class Particle
    {
        public int Index { get; private init; }
        public Point3D Position { get; private set; }
        public Point3D Velocity { get; private set; }
        public Point3D Acceleration { get; private init; }

        public Particle(int index, string input)
        {
            Index = index;
            var split = input.GetNumbers();
            Position = new(split[0], split[1], split[2]);
            Velocity = new(split[3], split[4], split[5]);
            Acceleration = new(split[6], split[7], split[8]);
        }

        public void Move()
        {
            Velocity = Velocity.Move(Acceleration);
            Position = Position.Move(Velocity);
        }
    }
}
