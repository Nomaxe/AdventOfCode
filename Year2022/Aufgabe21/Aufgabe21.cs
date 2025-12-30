using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe21 : IAufgabe
{
    private readonly Dictionary<string, long> _calced = [];
    private List<Job> _jobs = [];

    public Aufgabe21()
    {
        var input = Utilities.ReadInput(2022, 21);
        foreach (var line in input)
        {
            var monkey = line[..4];

            if (line.Contains('+') || line.Contains('-') || line.Contains('*') || line.Contains('/'))
            {
                _jobs.Add(new(monkey, line[6..10], line[13..], line[11]));
            }
            else
            {
                _calced.Add(monkey, long.Parse(line[6..]));
            }
        }
    }

    public string Calc()
    {
        do
        {
            List<Job> nextJobs = [];

            foreach (var job in _jobs)
            {
                if (!_calced.TryGetValue(job.Monkey1, out var monkey1))
                {
                    nextJobs.Add(job);
                    continue;
                }

                if (!_calced.TryGetValue(job.Monkey2, out var monkey2))
                {
                    nextJobs.Add(job);
                    continue;
                }

                var result = job.Sign switch
                {
                    '+' => monkey1 + monkey2,
                    '-' => monkey1 - monkey2,
                    '*' => monkey1 * monkey2,
                    '/' => monkey1 / monkey2,
                    _ => throw new NotImplementedException()
                };

                if (job.Monkey == "root")
                {
                    return result.ToString();
                }
                _calced.Add(job.Monkey, result);
            }

            _jobs = nextJobs;
        } while (_jobs.Count > 0);

        throw new NotImplementedException();
    }

    private readonly record struct Job(string Monkey, string Monkey1, string Monkey2, char Sign);
}
