using AdventOfCode.Utils;

namespace AdventOfCode.Year2022
{
    class Aufgabe13 : IAufgabe
    {
        private readonly string[] _input;

        public Aufgabe13()
        {
            _input = Utilities.ReadInput(2022, 13);
        }

        public string Calc()
        {
            int result = 0;

            for (int i = 0; i < _input.Length; i += 3)
            {
                Array check = CreateArrayElement(_input[i]);
                Array test = CreateArrayElement(_input[i + 1]);

                if (check.CompareTo(test) < 0)
                {
                    result += (i / 3) + 1;
                }
            }

            return result.ToString();
        }

        private static Array CreateArrayElement(string input)
        {
            Array root = new();
            List<Array> arrays = [root];

            for (int i = 1; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '[':
                        Array array = new();
                        arrays[^1].Add(array);
                        arrays.Add(array);
                        break;
                    case ']':
                        arrays.RemoveAt(arrays.Count - 1);
                        break;
                    case ',':
                        break;
                    default:
                        //Zahlen
                        int rangePosition = i + 1;
                        int oldI = i;
                        if (char.IsAsciiDigit(input[i + 1]))
                        {
                            rangePosition++;
                            i++;
                        }
                        arrays[^1].Add(new Number(int.Parse(input[oldI..rangePosition])));
                        break;
                }
            }

            return root;
        }

        private interface IElement : IComparable<IElement>
        {

        }

        private class Number(int value) : IElement
        {
            public int Value { get; init; } = value;

            public override string ToString()
            {
                return Value.ToString();
            }

            public Array AsArray()
            {
                Array array = new();
                array.Add(this);
                return array;
            }

            public int CompareTo(IElement? other)
            {
                if (other == null)
                {
                    return 1;
                }
                else if (other is Number number)
                {
                    return Value.CompareTo(number.Value);
                }
                else if (other is Array array)
                {
                    return AsArray().CompareTo(array);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        private class Array : IElement
        {
            private readonly List<IElement> _values = [];
            public IReadOnlyList<IElement> Values => _values.AsReadOnly();

            public void Add(IElement element)
            {
                _values.Add(element);
            }

            public int CompareTo(IElement? other)
            {
                if (other == null)
                {
                    return 1;
                }
                else if (other is Number number)
                {
                    return CompareTo(number.AsArray());
                }
                else if (other is Array array)
                {
                    return CompareArray(array);
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            private int CompareArray(Array other)
            {
                int minCount = int.Min(Values.Count, other.Values.Count);
                for (int i = 0; i < minCount; i++)
                {
                    var compare = Values[i].CompareTo(other.Values[i]);
                    if (compare != 0)
                    {
                        return compare;
                    }
                }

                return Values.Count.CompareTo(other.Values.Count);
            }

            public override string ToString()
            {
                return $"[{string.Join(", ", _values)}]";
            }
        }
    }
}
