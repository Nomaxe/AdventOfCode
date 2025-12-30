using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Utils;

internal class LargeCounter<TKey> : IEnumerable<KeyValuePair<TKey, ulong>>
                                  where TKey : notnull
{
    private readonly Dictionary<TKey, ulong> _items;

    public ulong this[TKey key] => _items[key];
    public int Count => _items.Count;

    public LargeCounter()
    {
        _items = [];
    }

    public LargeCounter(int capacity)
    {
        _items = new(capacity);
    }

    public LargeCounter(IEnumerable<TKey> items)
    {
        _items = [];
        foreach (var item in items)
        {
            Add(item);
        }
    }

    private LargeCounter(LargeCounter<TKey> counter)
    {
        _items = counter._items.ToDictionary(x => x.Key, x => x.Value);
    }

    public void Add(TKey item)
    {
        Add(item, 1);
    }

    public bool Add(TKey item, ulong value)
    {
        ref var pointer = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, item, out var exists);
        pointer += value;
        return !exists;
    }

    public void Add(KeyValuePair<TKey, ulong> item)
    {
        Add(item.Key, item.Value);
    }

    public void AddRange(IEnumerable<KeyValuePair<TKey, ulong>> items)
    {
        foreach (var item in items)
        {
            Add(item.Key, item.Value);
        }
    }

    public bool Decrease(TKey item)
    {
        return Decrease(item, 1);
    }

    public bool Decrease(TKey item, ulong value)
    {
        ref var pointer = ref CollectionsMarshal.GetValueRefOrNullRef(_items, item);
        if (Unsafe.IsNullRef(ref pointer))
        {
            throw new KeyNotFoundException($"Item {item} not present");
        }

        if (pointer > value)
        {
            pointer -= value;
            return false;
        }
        else if (pointer == value)
        {
            _items.Remove(item);
            return true;
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"The value {pointer} of item {item} can not be decreased by {value}");
        }
    }

    public ulong GetValueOrDefault(TKey item, ulong defaultValue = 0)
    {
        if (_items.TryGetValue(item, out var value))
        {
            return value;
        }

        return defaultValue;
    }

    public bool TryGetValue(TKey item, out ulong value)
    {
        return _items.TryGetValue(item, out value);
    }

    public LargeCounter<TKey> Clone()
    {
        return new(this);
    }

    public TKey GetMaxKey()
    {
        return GetMax().Key;
    }

    public KeyValuePair<TKey, ulong> GetMax()
    {
        return _items.MaxBy(x => x.Value);
    }

    public ulong Max()
    {
        return _items.Values.Max();
    }

    public TKey GetMinKey()
    {
        return GetMin().Key;
    }

    public KeyValuePair<TKey, ulong> GetMin()
    {
        return _items.MinBy(x => x.Value);
    }

    public (ulong Max, ulong Min) GetMaxMin()
    {
        ulong max = 0;
        ulong min = ulong.MaxValue;

        foreach (var value in _items.Values)
        {
            if (value > max)
            {
                max = value;
            }
            if (value < min)
            {
                min = value;
            }
        }

        return (max, min);
    }

    public ulong GetTotalCount()
    {
        ulong total = 0;
        foreach (var value in _items.Values)
        {
            total += value;
        }

        return total;
    }

    public bool HasCount(ulong count)
    {
        return _items.Values.Any(x => x == count);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public IEnumerator<KeyValuePair<TKey, ulong>> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
