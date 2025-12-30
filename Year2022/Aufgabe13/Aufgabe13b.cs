using AdventOfCode.Utils;

namespace AdventOfCode.Year2022
{
    class Aufgabe13b : IAufgabe
    {
        private readonly string[] _input;

        public Aufgabe13b()
        {
            _input = Utilities.ReadInput(2022, 13);
        }

        public string Calc()
        {
            List<Array> arrays = [];

            for (int i = 0; i < _input.Length; i += 3)
            {
                arrays.Add(CreateArrayElement(_input[i]));
                arrays.Add(CreateArrayElement(_input[i + 1]));
            }

            Array array2 = new();
            Array innerArray = new();
            array2.Add(innerArray);
            innerArray.Add(new Number(2));
            arrays.Add(array2);

            Array array6 = new();
            innerArray = new();
            array6.Add(innerArray);
            innerArray.Add(new Number(6));
            arrays.Add(array6);

            arrays.Sort();

            return ((arrays.IndexOf(array2) + 1) * (arrays.IndexOf(array6) + 1)).ToString();
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
                    return CompareArray(number.AsArray());
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
