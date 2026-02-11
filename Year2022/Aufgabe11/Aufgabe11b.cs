using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe11b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Monkey> _monkeys;
    private readonly DictionaryList<int, long> _monkeyItems;
    private readonly LargeCounter<int> _counter;
    private long _modolo = 1;

    public Aufgabe11b()
    {
        _input = Utilities.ReadInput(2022, 11);
        _monkeys = new((_input.Length + 1) / 7);
        _monkeyItems = new(_monkeys.Count);
        _counter = new(_monkeys.Count);
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i += 7)
        {
            _monkeys.Add(new(_input[(i + 2)..(i + 6)]));
            _monkeyItems.Add(i / 7, _input[i + 1].GetUnsignedLongNumbers());
            _modolo *= _monkeys[i / 7].TestValue;
        }

        for (int i = 0; i < 10000; i++)
        {
            for (int j = 0; j < _monkeys.Count; j++)
            {
                var monkey = _monkeys[j];
                foreach (var item in _monkeyItems[j])
                {
                    var value = item;
                    value = monkey.Operate(value);
                    value %= _modolo; //weil alle Testcases Primzahlen sind
                    _monkeyItems.Add(monkey.GetNextMonkey(value), value);
                }

                _counter.Add(j, (ulong)_monkeyItems[j].Count);
                _monkeyItems[j].Clear();
            }
        }

        var count = _counter.OrderByDescending(x => x.Value).Select(x => x.Value).Take(2).ToList();
        return (count[0] * count[1]).ToString();
    }

    private readonly struct Monkey
    {
        public Operation Operation { get; private init; }
        public long? OperationValue { get; private init; }
        public int TestValue { get; private init; }
        public int NextMonkeyIfTrue { get; private init; }
        public int NextMonkeyIfFalse { get; private init; }

        public Monkey(string[] input)
        {
            var operationValueIndex = input[0].LastIndexOf(' ');
            if (long.TryParse(input[0][(operationValueIndex + 1)..], out var operationValue))
            {
                OperationValue = operationValue;
            }
            var operationIndex = input[0].LastIndexOf(' ', operationValueIndex - 1);
            Operation = input[0][operationIndex + 1] == '+' ? Operation.Addition : Operation.Multiplication;
            var testValueIndex = input[1].LastIndexOf(' ');
            TestValue = int.Parse(input[1][testValueIndex..]);
            NextMonkeyIfTrue = input[2][^1].ToNumber();
            NextMonkeyIfFalse = input[3][^1].ToNumber();
        }

        public long Operate(long value)
        {
            var ownValue = OperationValue.GetValueOrDefault(value);
            return Operation switch
            {
                Operation.Addition => value + ownValue,
                Operation.Multiplication => value * ownValue,
                _ => throw new NotImplementedException(),
            };
        }

        public int GetNextMonkey(long value)
        {
            return value % TestValue == 0 ? NextMonkeyIfTrue : NextMonkeyIfFalse;
        }
    }

    private enum Operation
    {
        Addition,
        Multiplication
    }
}
