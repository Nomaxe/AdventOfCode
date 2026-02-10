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
        list.AddRange(items);
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

    public void RemoveDuplicatesUntilSingleItem()
    {
        Queue<TItem> queue = new();
        foreach (var item in _items.Where(x => x.Value.Count == 1))
        {
            queue.Enqueue(item.Value.First());
        }

        while (queue.Count > 0)
        {
            var removeItem = queue.Dequeue();

            foreach (var item in _items.Where(x => x.Value.Count > 1))
            {
                item.Value.Remove(removeItem);
                if (item.Value.Count == 1)
                {
                    queue.Enqueue(item.Value.First());
                }
            }
        }
    }

    public void RemoveItemAtAllKeysExcept(TKey key, TItem item)
    {
        foreach (var keyValuePair in _items.Where(x => !x.Key.Equals(key)))
        {
            keyValuePair.Value.Remove(item);
        }
    }

    public void IntersectWith(TKey key, IEnumerable<TItem> hashset)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        if (list is not null)
        {
            list.IntersectWith(hashset);
        }
        else
        {
            list = [];
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

    public IEnumerable<TItem> GetAllItems()
    {
        return _items.Values.SelectMany(x => x);
    }

    public bool Contains(TKey key, TItem item)
    {
        if (_items.TryGetValue(key, out var list))
        {
            return list.Contains(item);
        }

        return false;
    }

    public bool ContainsKey(TKey key)
    {
        return _items.ContainsKey(key);
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
