using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe14b : IAufgabe
{
    private readonly string[] _input;
    private const int Seconds = 2503;

    public Aufgabe14b()
    {
        _input = Utilities.ReadInput(2015, 14);
    }

    public string Calc()
    {
        List<Reindeer> list = new(_input.Length);
        list.AddRange(_input.Select(x => new Reindeer(x)));

        for (int i = 1; i <= Seconds; i++)
        {
            int maxDistance = 0;

            foreach (var reindeer in list)
            {
                reindeer.Move();
                maxDistance = int.Max(reindeer.Distance, maxDistance);
            }

            foreach (var reindeer in list.Where(x => x.Distance == maxDistance))
            {
                reindeer.AddPoint();
            }
        }

        return list.Max(x => x.Points).ToString();
    }

    private class Reindeer
    {
        public int Distance { get; private set; }
        public int Points { get; private set; }

        private readonly int _speed;
        private readonly int _activeSeconds;
        private readonly int _inactiveSeconds;
        private int _currentSeconds;
        private bool _actice;

        public Reindeer(string line)
        {
            var numbers = line.GetUnsignedNumbers();
            _speed = numbers[0];
            _activeSeconds = numbers[1];
            _inactiveSeconds = numbers[2];
            _currentSeconds = _activeSeconds;
            _actice = true;

            Distance = 0;
            Points = 0;
        }

        public void Move()
        {
            if (_currentSeconds > 0)
            {
                if (_actice)
                {
                    Distance += _speed;
                }
            }
            else
            {
                if (_actice)
                {
                    _currentSeconds = _inactiveSeconds;
                    _actice = false;
                }
                else
                {
                    _currentSeconds = _activeSeconds;
                    _actice = true;
                    Distance += _speed;
                }
            }

            _currentSeconds--;
        }

        public void AddPoint()
        {
            Points++;
        }
    }
}
