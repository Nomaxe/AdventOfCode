using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Utils;

internal class DictionaryHashSet<TKey, TItem> : IEnumerable<KeyValuePair<TKey, HashSet<TItem>>>
                                              where TKey : notnull
{
    private readonly Dictionary<TKey, HashSet<TItem>> _items;

    public HashSet<TItem> this[TKey key] => _items[key];

    public DictionaryHashSet()
    {
        _items = [];
    }

    public DictionaryHashSet(int capacity)
    {
        _items = new(capacity);
    }

    public bool Add(TKey key, TItem item)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        list ??= [];
        return list.Add(item);
    }

    public void AddRange(TKey key, IEnumerable<TItem> items)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        list ??= [];
        foreach (var item in items)
        {
            list.Add(item);
        }
    }

    public bool Remove(TKey key, TItem item)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrNullRef(_items, key);
        if (Unsafe.IsNullRef(ref list))
        {
            throw new KeyNotFoundException($"key {key} not present");
        }

        return list.Remove(item);
    }

    public int RemoveWhere(TKey key, Predicate<TItem> match, bool throwKeyNotFoundException = true)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrNullRef(_items, key);
        if (Unsafe.IsNullRef(ref list))
        {
            if (throwKeyNotFoundException)
            {
                throw new KeyNotFoundException($"key {key} not present");
            }
            else
            {
                return -1;
            }
        }

        return list.RemoveWhere(match);
    }

    public bool RemoveAll(TKey key)
    {
        return _items.Remove(key);
    }

    public void RemoveItemAtAllKeysExcept(TKey key, TItem item)
    {
        foreach (var keyValuePair in _items.Where(x => !x.Key.Equals(key)))
        {
            keyValuePair.Value.Remove(item);
        }
    }

    public IEnumerable<TItem> GetItems(TKey key)
    {
        if (_items.TryGetValue(key, out var list))
        {
            return list;
        }

        return [];
    }

    public IEnumerable<TKey> GetKeysOfItem(TItem item)
    {
        return _items.Where(x => x.Value.Contains(item)).Select(x => x.Key);
    }

    public bool Contains(TKey key, TItem item)
    {
        if (_items.TryGetValue(key, out var list))
        {
            return list.Contains(item);
        }

        return false;
    }

    public IEnumerator<KeyValuePair<TKey, HashSet<TItem>>> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
