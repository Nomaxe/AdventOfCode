using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe07b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<char, char> _steps;
    private readonly Worker[] _workers;

    private const int AdditionalSeconds = 60;

    public Aufgabe07b()
    {
        _input = Utilities.ReadInput(2018, 7);
        _steps = new(_input.Length);
        _workers = [new(), new(), new(), new(), new()];
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            _steps.Add(line[36], line[5]);
            _steps.AddKey(line[5]);
        }

        int steps = 0;

        do
        {
            foreach (var worker in _workers.Where(x => x.Character != '\0'))
            {
                worker.Remaining--;

                if (worker.Remaining == 0)
                {
                    _steps.RemoveItemAtAllKeys(worker.Character);
                    worker.Character = '\0';
                }
            }

            foreach (var worker in _workers.Where(x => x.Character == '\0'))
            {
                var nextCharacter = _steps.Where(x => x.Value.Count == 0).OrderBy(x => x.Key).Select(x => x.Key).FirstOrDefault();
                if (nextCharacter != '\0')
                {
                    worker.Character = nextCharacter;
                    worker.Remaining = AdditionalSeconds + nextCharacter - 'A' + 1;
                    _steps.RemoveAll(nextCharacter);
                }
            }

            steps++;
        } while (_steps.Count > 0 || !WorkerFinished());

        return (steps - 1).ToString();
    }

    private bool WorkerFinished()
    {
        return _workers.All(x => x.Character == '\0' && x.Remaining == 0);
    }

    private class Worker
    {
        public char Character { get; set; }
        public int Remaining { get; set; }
    }
}
