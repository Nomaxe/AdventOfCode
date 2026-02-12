using AdventOfCode.Utils;
using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Year2020;

internal class Aufgabe22b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe22b()
    {
        _input = Utilities.ReadInput(2020, 22);
    }

    public string Calc()
    {
        Queue<int> player1 = new(_input.Length - 3);
        Queue<int> player2 = new(_input.Length - 3);
        var currentList = player1;

        for (int i = 1; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                currentList = player2;
                i++;
                continue;
            }

            currentList.Enqueue(int.Parse(_input[i]));
        }

        PlayGame(player1, player2);

        currentList = player1.Count > 0 ? player1 : player2;
        int result = 0;

        for (int i = currentList.Count; i >= 1; i--)
        {
            result += currentList.Dequeue() * i;
        }

        return result.ToString();
    }

    private static Winner PlayGame(Queue<int> player1, Queue<int> player2)
    {
        HashSet<Cards> playedCards = new();

        do
        {
            var player1Card = player1.Dequeue();
            var player2Card = player2.Dequeue();
            Winner winner;
            Cards cards = new(player1.ToArray(), player2.ToArray());

            if (playedCards.Contains(cards))
            {
                return Winner.Player1;
            }
            else if (player1.Count >= player1Card && player2.Count >= player2Card)
            {
                winner = PlayGame(player1.GetNewQueue(player1Card), player2.GetNewQueue(player2Card));
            }
            else if (player1Card > player2Card)
            {
                winner = Winner.Player1;
            }
            else
            {
                winner = Winner.Player2;
            }

            if (winner == Winner.Player1)
            {
                player1.Enqueue(player1Card);
                player1.Enqueue(player2Card);
            }
            else
            {
                player2.Enqueue(player2Card);
                player2.Enqueue(player1Card);
            }

            playedCards.Add(cards);
        } while (player1.Count > 0 && player2.Count > 0);

        return player1.Count > 0 ? Winner.Player1 : Winner.Player2;
    }

    private enum Winner
    {
        Player1,
        Player2
    }

    private readonly struct Cards
    {
        private readonly int[] _player1Cards;
        private readonly int[] _player2Cards;

        public Cards(int[] player1Cards, int[] player2Cards)
        {
            _player1Cards = player1Cards;
            _player2Cards = player2Cards;
        }
        
        public override int GetHashCode()
        {
            int hashCode = 19;

            unchecked
            {
                foreach (var item in _player1Cards)
                {
                    hashCode += item * 31;
                }

                hashCode *= 19;

                foreach (var item in _player2Cards)
                {
                    hashCode += item * 31;
                }
            }

            return hashCode;
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Cards cards && _player1Cards.SequenceEqual(cards._player1Cards) && _player2Cards.SequenceEqual(cards._player2Cards);
        }
    }
}
