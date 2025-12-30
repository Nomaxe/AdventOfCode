using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe11 : IAufgabe
{
    private readonly List<Monkey> _monkeys;
    private readonly DictionaryList<int, int> _monkeyItems;
    private readonly LargeCounter<int> _counter;

    public Aufgabe11()
    {
        var input = Utilities.ReadInput(2022, 11);
        _monkeys = new((input.Length + 1) / 7);
        _monkeyItems = new(_monkeys.Count);
        for (int i = 0; i < input.Length; i += 7)
        {
            _monkeys.Add(new(input[(i + 2)..(i + 6)]));
            _monkeyItems.Add(i / 7, input[i + 1].GetUnsignedNumbers());
        }
        _counter = new(_monkeys.Count);
    }

    public string Calc()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < _monkeys.Count; j++)
            {
                var monkey = _monkeys[j];
                foreach (var item in _monkeyItems[j])
                {
                    var value = item;
                    value = monkey.Operate(value);
                    value /= 3;
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
        public int? OperationValue { get; private init; }
        public int TestValue { get; private init; }
        public int NextMonkeyIfTrue { get; private init; }
        public int NextMonkeyIfFalse { get; private init; }

        public Monkey(string[] input)
        {
            var operationValueIndex = input[0].LastIndexOf(' ');
            if (int.TryParse(input[0][(operationValueIndex + 1)..], out var operationValue))
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

        public int Operate(int value)
        {
            var ownValue = OperationValue.GetValueOrDefault(value);
            return Operation switch
            {
                Operation.Addition => value + ownValue,
                Operation.Multiplication => value * ownValue,
                _ => throw new NotImplementedException(),
            };
        }

        public int GetNextMonkey(int value)
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
