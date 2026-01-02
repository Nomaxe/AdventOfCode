using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AdventOfCode.Utils;

internal class DictionaryList<TKey, TItem> : IEnumerable<KeyValuePair<TKey, List<TItem>>>
                                           where TKey : notnull
{
    private readonly Dictionary<TKey, List<TItem>> _items;

    public List<TItem> this[TKey key] => _items[key];
    public int Count => _items.Count;
    public Dictionary<TKey, List<TItem>>.KeyCollection Keys => _items.Keys;

    public DictionaryList()
    {
        _items = [];
    }

    public DictionaryList(int capacity)
    {
        _items = new(capacity);
    }

    public void Add(TKey key, TItem item)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        list ??= [];
        list.Add(item);
    }

    public void Add(TKey key, IEnumerable<TItem> item)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        list ??= [];
        list.AddRange(item);
    }

    public void AddKey(TKey key)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        list ??= [];
    }

    public bool RemoveAll(TKey key)
    {
        return _items.Remove(key);
    }

    public void RemoveItemAtAllKeys(TItem item)
    {
        foreach (var keyValuePair in _items)
        {
            keyValuePair.Value.Remove(item);
        }
    }

    public bool TryGetValue(TKey key, [NotNullWhen(true)] out List<TItem>? item)
    {
        return _items.TryGetValue(key, out item);
    }

    public IEnumerator<KeyValuePair<TKey, List<TItem>>> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
