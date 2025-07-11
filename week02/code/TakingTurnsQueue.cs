using System;
using System.Collections.Generic;

public class TakingTurnsQueue
{
    private class QueueEntry
    {
        public Person Person { get; }
        public int RemainingTurns { get; set; }
        public bool HasInfiniteTurns => Person.Turns <= 0;

        public QueueEntry(Person person)
        {
            Person = person;
            RemainingTurns = person.Turns;
        }
    }

    private readonly Queue<QueueEntry> queue = new();

    public int Length => queue.Count;

    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        var entry = new QueueEntry(person);
        queue.Enqueue(entry);
    }

    public Person GetNextPerson()
    {
        if (queue.Count == 0)
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        var current = queue.Dequeue();

        // Return the person
        var result = current.Person;

        // Re-enqueue if infinite or has remaining turns after this round
        if (current.HasInfiniteTurns)
        {
            queue.Enqueue(current);
        }
        else if (current.RemainingTurns > 1)
        {
            current.RemainingTurns--;
            queue.Enqueue(current);
        }

        return result;
    }
}
