using ExcelDataReader;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Utils;

internal class CompleteCalc
{
    private readonly List<Type> _types;
    private readonly Dictionary<(int year, string day), string> _test;
    private readonly List<Type> _errors = [];
    private readonly Dictionary<Type, TimeSpan> _duration;
    private TimeSpan _completeTime = TimeSpan.Zero;

    private const int FullYears = 7; //last Day Part 2 is a single star, if you have all before

    private const int PosYear = 5;
    private const int PosDay = PosYear + 5;
    private const int PosSeconds = PosDay + 5;
    private const int PosMilliseconds = PosSeconds + 3;
    private const int PosMicroseconds = PosMilliseconds + 4;
    private const int PosPercent = PosMicroseconds + 5;

    public CompleteCalc()
    {
        var interfaceType = typeof(IAufgabe);
        _types = [..AppDomain.CurrentDomain.GetAssemblies()
                             .SelectMany(x => x.GetTypes())
                             .Where(x => !x.IsInterface)
                             .Where(interfaceType.IsAssignableFrom)
                             .OrderBy(x => x.FullName)];

        _test = new(_types.Count);
        _duration = new(_types.Count);
    }

    public void Calc()
    {
        ReadTest();

        foreach (var type in _types)
        {
            GC.Collect();
            Console.WriteLine(type);
            if (Activator.CreateInstance(type) is not IAufgabe aufgabe)
            {
                Console.WriteLine("#################################################################");
                Console.WriteLine("##############################ERROR##############################");
                Console.WriteLine("#################################################################");
                _errors.Add(type);
                continue;
            }
            var watch = Stopwatch.StartNew();
            var result = aufgabe.Calc();
            watch.Stop();

            CompareTest(type, result);
            Console.WriteLine(result);
            _duration.Add(type, watch.Elapsed);
            _completeTime += watch.Elapsed;
        }

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        WriteErrors();

        Output();
    }

    private void Output()
    {
        const int OutputCount = 250;

        Console.WriteLine($"Worst {int.Min(_types.Count, OutputCount)} of {_types.Count + FullYears} ({_completeTime})");

        int place = 1;

        Console.Write("###");
        Console.CursorLeft = PosYear;
        Console.Write("Year");
        Console.CursorLeft = PosDay;
        Console.Write("Day");
        Console.CursorLeft = PosSeconds;
        Console.Write("Se");
        Console.CursorLeft = PosMilliseconds;
        Console.Write("mmm");
        Console.CursorLeft = PosMicroseconds;
        Console.Write("μμμ");
        Console.CursorLeft = PosPercent;
        Console.Write("%");

        Console.WriteLine();

        var list = _duration.OrderByDescending(x => x.Value).ToList();
        foreach (var type in list.Take(OutputCount))
        {
            OutputLine(type.Value, type: type.Key, place: place++);
        }

        if (list.Count > OutputCount)
        {
            OutputLine(GetTimeSpanSum(list.Skip(OutputCount).Select(x => x.Value)), name: "Rest");
        }
    }

    private void OutputLine(TimeSpan timespanOutput, Type? type = null, int? place = null, string? name = null)
    {
        if (timespanOutput.TotalSeconds >= 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (timespanOutput.TotalMilliseconds > 15)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (timespanOutput.TotalMilliseconds < 1)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        if (place.HasValue)
        {
            Console.Write($"{place,3}");
        }
        else
        {
            Console.Write("###");
        }

        Console.CursorLeft = PosYear;
        if (type != null)
        {
            Console.Write(GetYear(type));
            Console.CursorLeft = PosDay;
            Console.Write(GetDay(type));
        }
        else
        {
            Console.Write(name);
        }

        if (timespanOutput.TotalSeconds >= 1)
        {
            Console.CursorLeft = PosSeconds;
            Console.Write($"{(int)timespanOutput.TotalSeconds,2}");
        }

        if (timespanOutput.TotalMilliseconds >= 1)
        {
            Console.CursorLeft = PosMilliseconds;
            Console.Write($"{timespanOutput.Milliseconds,3}");
        }

        Console.CursorLeft = PosMicroseconds;
        Console.Write($"{timespanOutput.Microseconds,3}");

        Console.CursorLeft = PosPercent;
        Console.Write($"{timespanOutput.TotalNanoseconds / _completeTime.TotalNanoseconds,7:P}");

        Console.ResetColor();
        Console.WriteLine();
    }

    private void WriteErrors()
    {
        if (_errors.Count == 0)
        {
            return;
        }

        Console.BackgroundColor = ConsoleColor.Red;
        Console.WriteLine("FEHLER");
        foreach (var error in _errors)
        {
            Console.WriteLine(error);
        }

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }

    private void ReadTest()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using var stream = File.Open(@"..\..\..\Test.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        var data = reader.AsDataSet(new()
        {
            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
            {
                UseHeaderRow = true,
            },
        }) ?? throw new InvalidOperationException("Es wurden keine Daten gefunden");

        foreach (DataRow row in data.Tables[0]?.Rows ?? throw new InvalidOperationException("Es wurden keine Daten gefunden"))
        {
            var year = Convert.ToInt32(row[0]);
            var day = Convert.ToString(row[1]) ?? throw new InvalidOperationException("Es wurden keine Daten gefunden");
            var result = Convert.ToString(row[2]) ?? throw new InvalidOperationException("Es wurden keine Daten gefunden");
            _test.Add((year, day), result);
        }
    }

    private void CompareTest(Type type, string result)
    {
        var year = GetYear(type);
        var day = GetDay(type);
        if (day[0] == '0')
        {
            day = day[1..];
        }

        if (!_test.TryGetValue((year, day), out var test))
        {
            _errors.Add(type);
            return;
        }

        if (test != result)
        {
            _errors.Add(type);
        }
    }

    private static int GetYear(Type type)
    {
        return int.Parse(type.FullName![17..21]);
    }

    private static string GetDay(Type type)
    {
        return type.FullName![29..];
    }

    private static TimeSpan GetTimeSpanSum(IEnumerable<TimeSpan> timespans)
    {
        TimeSpan sum = TimeSpan.Zero;

        foreach (var timespan in timespans)
        {
            sum += timespan;
        }

        return sum;
    }
}
