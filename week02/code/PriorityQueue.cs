using System;
using System.Collections.Generic;
using System.Linq;

public class PriorityQueue
{
    private class Item
    {
        public string Value { get; }
        public int Priority { get; }
        public int InsertOrder { get; }

        public Item(string value, int priority, int insertOrder)
        {
            Value = value;
            Priority = priority;
            InsertOrder = insertOrder;
        }
    }

    private readonly List<Item> _items = new();
    private int _insertCounter = 0;

    public void Enqueue(string value, int priority)
    {
        _items.Add(new Item(value, priority, _insertCounter++));
    }

    public string Dequeue()
    {
        if (_items.Count == 0)
            throw new InvalidOperationException("The queue is empty.");

        // Find item with highest priority, then lowest insert order (FIFO)
        var highest = _items
            .OrderByDescending(i => i.Priority)
            .ThenBy(i => i.InsertOrder)
            .First();

        _items.Remove(highest);
        return highest.Value;
    }
}
