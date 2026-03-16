using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe18 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Pair> _numbers;

    public Aufgabe18()
    {
        _input = Utilities.ReadInput(2021, 18);
        _numbers = new();
    }

    public string Calc()
    {
        var parentNode = CreatePairNode(_input[0]);

        for (int i = 1; i < _input.Length; i++)
        {
            var nextNode = CreatePairNode(_input[i]);
            parentNode = new(parentNode, nextNode);

            parentNode = Addition(parentNode);
        }

        return parentNode.GetMagnitude().ToString();
    }

    private Pair CreatePairNode(string input)
    {
        Pair currentNode = new();

        for (int i = 1; i < input.Length - 1; i++)
        {
            var character = input[i];
            switch (character)
            {
                case '[':
                    Pair nextNode = new(currentNode);
                    currentNode.SetChildren(nextNode);
                    currentNode = nextNode;
                    break;
                case ']':
                    currentNode = currentNode.Parent ?? throw new InvalidOperationException();
                    break;
                case ',':
                    break;
                default:
                    var newNode = currentNode.SetChildren(character.ToNumber());
                    _numbers.Add(newNode);
                    break;
            }
        }

        return currentNode;
    }

    private Pair Addition(Pair pair)
    {
        bool didSomething;

        do
        {
            didSomething = false;

            var pairToExplode = GetPairToExplode(pair);
            if (pairToExplode != null)
            {
                didSomething = true;
                Explode(pairToExplode);
                continue;
            }

            var pairToSplit = GetPairToSplit();
            if (pairToSplit != null)
            {
                didSomething = true;
                Split(pairToSplit);
            }

        } while (didSomething);

        return pair;
    }

    private static Pair? GetPairToExplode(Pair pair, int depth = 0)
    {
        if (depth >= 4 && pair.IsNumberPair())
        {
            return pair;
        }

        if (pair.Left != null)
        {
            var pairToExplode = GetPairToExplode(pair.Left, depth + 1);
            if (pairToExplode != null)
            {
                return pairToExplode;
            }
        }

        if (pair.Right != null)
        {
            var pairToExplode = GetPairToExplode(pair.Right, depth + 1);
            if (pairToExplode != null)
            {
                return pairToExplode;
            }
        }

        return null;
    }

    private void Explode(Pair pair)
    {
        if (pair.Left == null || pair.Right == null || pair.Left.Number == null || pair.Right.Number == null)
        {
            throw new InvalidOperationException();
        }

        var numberIndex = _numbers.IndexOf(pair.Left!);
        if (numberIndex == -1)
        {
            throw new InvalidOperationException();
        }

        if (numberIndex > 0)
        {
            _numbers[numberIndex - 1].Number += pair.Left.Number.Value;
        }
        if (numberIndex + 2 < _numbers.Count)
        {
            _numbers[numberIndex + 2].Number += pair.Right.Number.Value;
        }

        _numbers.RemoveAt(numberIndex + 1);
        _numbers[numberIndex] = pair;

        pair.Right = null;
        pair.Left = null;
        pair.Number = 0;
    }

    private Pair? GetPairToSplit()
    {
        return _numbers.FirstOrDefault(x => x.Number >= 10);
    }

    private void Split(Pair pair)
    {
        if (!pair.Number.HasValue)
        {
            throw new InvalidOperationException();
        }

        var numberLeft = pair.Number.Value / 2;
        var numberRight = numberLeft;
        if (pair.Number % 2 != 0)
        {
            numberRight++;
        }

        pair.Number = null;
        var newNodeLeft = pair.SetChildren(numberLeft);
        var newNodeRigth = pair.SetChildren(numberRight);

        var numberIndex = _numbers.IndexOf(pair);
        if (numberIndex < 0)
        {
            throw new InvalidOperationException();
        }

        _numbers[numberIndex] = newNodeLeft;
        _numbers.Insert(numberIndex + 1, newNodeRigth);
    }

    private class Pair
    {
        public int? Number { get; set; }
        public Pair? Left { get; set; }
        public Pair? Right { get; set; }
        public Pair? Parent { get; set; }

        public Pair()
        {
            Number = null;
            Left = null;
            Right = null;
            Parent = null;
        }

        public Pair(Pair parent)
        {
            Number = null;
            Left = null;
            Right = null;
            Parent = parent;
        }

        private Pair(Pair parent, int value)
        {
            Number = value;
            Left = null;
            Right = null;
            Parent = parent;
        }

        public Pair(Pair child1, Pair child2)
        {
            Number = null;
            Left = child1;
            Right = child2;
            Parent = null;

            Left.Parent = this;
            Right.Parent = this;
        }

        public void SetChildren(Pair child)
        {
            if (Left == null)
            {
                Left = child;
            }
            else if (Right == null)
            {
                Right = child;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public Pair SetChildren(int value)
        {
            Pair newNode = new(this, value);

            if (Left == null)
            {
                Left = newNode;
            }
            else if (Right == null)
            {
                Right = newNode;
            }
            else
            {
                throw new InvalidOperationException();
            }

            return newNode;
        }

        public bool IsNumberPair()
        {
            if (Left == null)
            {
                return false;
            }

            if (Right == null)
            {
                return false;
            }

            return Left.Number.HasValue && Right.Number.HasValue;
        }

        public int GetMagnitude()
        {
            if (Number.HasValue)
            {
                return Number.Value;
            }

            if (Left == null || Right == null)
            {
                throw new InvalidOperationException();
            }

            return Left.GetMagnitude() * 3 + Right.GetMagnitude() * 2;
        }
    }
}
