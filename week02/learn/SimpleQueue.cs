public class SimpleQueue {
    public static void Run() {
        // Test 1
        Console.WriteLine("Test 1");
        var queue = new SimpleQueue();
        queue.Enqueue(100);
        var value = queue.Dequeue();
        Console.WriteLine(value);

        Console.WriteLine("------------");

        // Test 2
        Console.WriteLine("Test 2");
        queue = new SimpleQueue();
        queue.Enqueue(200);
        queue.Enqueue(300);
        queue.Enqueue(400);
        Console.WriteLine(queue.Dequeue());
        Console.WriteLine(queue.Dequeue());
        Console.WriteLine(queue.Dequeue());

        Console.WriteLine("------------");

        // Test 3
        Console.WriteLine("Test 3");
        queue = new SimpleQueue();
        try {
            queue.Dequeue();
            Console.WriteLine("Oops ... This shouldn't have worked.");
        } catch (IndexOutOfRangeException) {
            Console.WriteLine("I got the exception as expected.");
        }
    }

    private readonly List<int> _queue = new();

    private void Enqueue(int value) {
        _queue.Add(value); // Append to the end
    }

    private int Dequeue() {
        if (_queue.Count <= 0)
            throw new IndexOutOfRangeException();

        var value = _queue[0]; // Remove from the front
        _queue.RemoveAt(0);
        return value;
    }
}
