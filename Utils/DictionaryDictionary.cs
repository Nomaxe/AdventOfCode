using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Utils;

internal class DictionaryDictionary<TKey, TItemKey, TItem> where TKey : notnull
                                                           where TItemKey : notnull
{
    private readonly Dictionary<TKey, Dictionary<TItemKey, TItem>> _items;

    public Dictionary<TItemKey, TItem> this[TKey key] => _items[key];
    public Dictionary<TKey, Dictionary<TItemKey, TItem>>.KeyCollection Keys => _items.Keys;

    public DictionaryDictionary()
    {
        _items = [];
    }

    public DictionaryDictionary(int capacity)
    {
        _items = new(capacity);
    }

    public void Add(TKey key, TItemKey itemKey, TItem item)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(_items, key, out _);
        list ??= [];
        list.Add(itemKey, item);
    }

    public TItem GetValue(TKey key, TItemKey itemKey)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrNullRef(_items, key);
        if (Unsafe.IsNullRef(ref list))
        {
            throw new KeyNotFoundException($"{key} not in Dictionary");
        }

        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(list, itemKey);
        if (Unsafe.IsNullRef(ref value))
        {
            throw new KeyNotFoundException($"{itemKey} not in Inner-Dictionary");
        }

        return value;
    }

    public TItem? GetValueOrDefault(TKey key, TItemKey itemKey, TItem? defaultValue = default)
    {
        ref var list = ref CollectionsMarshal.GetValueRefOrNullRef(_items, key);
        if (Unsafe.IsNullRef(ref list))
        {
            return defaultValue;
        }

        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(list, itemKey);
        if (Unsafe.IsNullRef(ref value))
        {
            return defaultValue;
        }

        return value;
    }

    public bool TryGetValue(TKey key, TItemKey itemKey, out TItem? outValue)
    {
        outValue = default;

        ref var list = ref CollectionsMarshal.GetValueRefOrNullRef(_items, key);
        if (Unsafe.IsNullRef(ref list))
        {
            return false;
        }

        ref var value = ref CollectionsMarshal.GetValueRefOrNullRef(list, itemKey);
        if (Unsafe.IsNullRef(ref value))
        {
            return false;
        }

        outValue = value;
        return true;
    }
}
