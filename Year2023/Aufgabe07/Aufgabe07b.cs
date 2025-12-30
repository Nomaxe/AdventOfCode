using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe07b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe07b()
    {
        _input = Utilities.ReadInput(2023, 7);
    }

    public string Calc()
    {
        var hands = new Hand[_input.Length];
        for (int i = 0; i < _input.Length; i++)
        {
            hands[i] = new(_input[i][..5], int.Parse(_input[i][6..]));
        }

        Array.Sort(hands);

        int sum = 0;

        for (int i = 0; i < hands.Length; i++)
        {
            sum += hands[i].Bid * (i + 1);
        }

        return sum.ToString();
    }

    private enum Type
    {
        HighCard = 0,
        OnePair = 1,
        TwoPair = 2,
        ThreeOfAKind = 3,
        FullHouse = 4,
        FourOfAKind = 5,
        FiveOfAKind = 6
    }

    private readonly struct Hand : IComparable<Hand>
    {
        public Type Type { get; private init; }
        public string Cards { get; private init; }
        public int Bid { get; private init; }

        public Hand(string cards, int bid)
        {
            Cards = cards;
            Bid = bid;

            DictionaryCounter<char> counter = new(Cards.Select(x => x));

            if (counter.Count == 1)
            {
                Type = Type.FiveOfAKind;
                return;
            }

            int jokerCount = counter.GetValueOrDefault('J');
            var max = counter.Max();
            if (max == 4)
            {
                if (jokerCount > 0)
                {
                    Type = Type.FiveOfAKind;
                }
                else
                {
                    Type = Type.FourOfAKind;
                }
                return;
            }

            if (max == 3)
            {
                if (counter.HasCount(2))
                {
                    if (jokerCount >= 2)
                    {
                        Type = Type.FiveOfAKind;
                    }
                    else
                    {
                        Type = Type.FullHouse;
                    }
                    return;
                }
                else
                {
                    if (jokerCount == 3)
                    {
                        Type = Type.FourOfAKind;
                    }
                    else if (jokerCount == 2)
                    {
                        Type = Type.FiveOfAKind;
                    }
                    else if (jokerCount == 1)
                    {
                        Type = Type.FourOfAKind;
                    }
                    else
                    {
                        Type = Type.ThreeOfAKind;
                    }
                    return;
                }
            }

            if (max == 2)
            {
                if (counter.GetCountAmount(2) == 2)
                {
                    if (jokerCount == 2)
                    {
                        Type = Type.FourOfAKind;
                    }
                    else if (jokerCount == 1)
                    {
                        Type = Type.FullHouse;
                    }
                    else
                    {
                        Type = Type.TwoPair;
                    }
                    return;
                }
                else
                {
                    if (jokerCount >= 1)
                    {
                        Type = Type.ThreeOfAKind;
                    }
                    else
                    {
                        Type = Type.OnePair;
                    }
                    return;
                }
            }

            if (jokerCount == 1)
            {
                Type = Type.OnePair;
            }
            else
            {
                Type = Type.HighCard;
            }
        }

        public int CompareTo(Hand other)
        {
            var compare = Type.CompareTo(other.Type);
            if (compare != 0)
            {
                return compare;
            }

            for (int i = 0; i < Cards.Length; i++)
            {
                compare = CardComparer(Cards[i], other.Cards[i]);
                if (compare != 0)
                {
                    return compare;
                }
            }

            return 0;
        }
    }

    private static int CardComparer(char left, char right)
    {
        if (left == right)
        {
            return 0;
        }

        if (left == 'A')
        {
            return 1;
        }
        else if (right == 'A')
        {
            return -1;
        }
        else if (left == 'K')
        {
            return 1;
        }
        else if (right == 'K')
        {
            return -1;
        }
        else if (left == 'Q')
        {
            return 1;
        }
        else if (right == 'Q')
        {
            return -1;
        }
        else if (left == 'T')
        {
            return 1;
        }
        else if (right == 'T')
        {
            return -1;
        }
        else if (left == 'J')
        {
            return -1;
        }
        else if (right == 'J')
        {
            return 1;
        }
        else
        {
            return left.CompareTo(right);
        }
    }
}
