using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Utils;

internal class DictionaryCounter<TKey> : IEnumerable<KeyValuePair<TKey, int>>
                                       where TKey : notnull
{
    private readonly Dictionary<TKey, int> _items;

    public int this[TKey key] => _items[key];
    public int Count => _items.Count;
    public Dictionary<TKey, int>.KeyCollection Keys => _items.Keys;

    public DictionaryCounter()
    {
        _items = [];
    }

    public DictionaryCounter(int capacity)
    {
        _items = new(capacity);
    }

    public DictionaryCounter(IEnumerable<TKey> items)
    {
        _items = [];
        foreach (var item in items)
        {
            Add(item);
        }
    }

    private DictionaryCounter(DictionaryCounter<TKey> counter)
    {
        _items = counter._items.ToDictionary(x => x.Key, x => x.Value);
    }

    public void Add(TKey item)
    {
        Add(item, 1);
    }

    public bool Add(TKey item, int value)
    {
        ref var pointer = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, item, out var exists);
        pointer += value;
        return !exists;
    }

    public void Add(KeyValuePair<TKey, int> item)
    {
        Add(item.Key, item.Value);
    }

    public int AddReturn(TKey item, int value)
    {
        ref var pointer = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, item, out _);
        pointer += value;
        return pointer;
    }

    public void AddRange(IEnumerable<KeyValuePair<TKey, int>> items)
    {
        foreach (var item in items)
        {
            Add(item.Key, item.Value);
        }
    }

    public void AddKey(TKey key)
    {
        CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
    }

    public void Decrease(TKey item)
    {
        Decrease(item, 1);
    }

    public void Decrease(TKey item, int value)
    {
        ref var pointer = ref CollectionsMarshal.GetValueRefOrNullRef(_items, item);
        if (Unsafe.IsNullRef(ref pointer))
        {
            throw new KeyNotFoundException($"Item {item} not present");
        }

        pointer -= value;
    }

    public int GetValueOrDefault(TKey item, int defaultValue = 0)
    {
        if (_items.TryGetValue(item, out var value))
        {
            return value;
        }

        return defaultValue;
    }

    public DictionaryCounter<TKey> Clone()
    {
        return new(this);
    }

    public TKey GetMaxKey()
    {
        return GetMax().Key;
    }

    public KeyValuePair<TKey, int> GetMax()
    {
        return _items.MaxBy(x => x.Value);
    }

    public int Max()
    {
        return _items.Values.Max();
    }

    public TKey GetMinKey()
    {
        return GetMin().Key;
    }

    public KeyValuePair<TKey, int> GetMin()
    {
        return _items.MinBy(x => x.Value);
    }

    public (int Max, int Min) GetMaxMin()
    {
        int max = 0;
        int min = int.MaxValue;

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

    public long GetTotalCount()
    {
        long total = 0;
        foreach (var value in _items.Values)
        {
            total += value;
        }

        return total;
    }

    public bool HasCount(int count)
    {
        return _items.Values.Any(x => x == count);
    }

    public int GetCountAmount(int count)
    {
        return _items.Values.Count(x => x == count);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public IEnumerator<KeyValuePair<TKey, int>> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
