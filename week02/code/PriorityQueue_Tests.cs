using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities
    // Expected Result: Dequeue returns items in order of highest priority first (highest number), not insertion order
    // Defect(s) Found: Queue ignored priority when selecting item to dequeue.
    public void TestPriorityQueue_DifferentPriorities()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 1);
        pq.Enqueue("B", 3);
        pq.Enqueue("C", 2);

        Assert.AreEqual("B", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());
        Assert.AreEqual("A", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same priority
    // Expected Result: Dequeue returns them in FIFO order
    // Defect(s) Found:  Queue violated FIFO policy when multiple items had the same priority.
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("X", 5);
        pq.Enqueue("Y", 5);
        pq.Enqueue("Z", 5);

        Assert.AreEqual("X", pq.Dequeue());
        Assert.AreEqual("Y", pq.Dequeue());
        Assert.AreEqual("Z", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Mix of priorities, checking both priority and FIFO
    // Expected Result: Highest priority dequeued first, FIFO respected within priority level
    // Defect(s) Found:  Queue failed to maintain correct priority AND FIFO behavior together. 
    public void TestPriorityQueue_MixedPrioritiesAndFIFO()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 3);
        pq.Enqueue("B", 1);
        pq.Enqueue("C", 3);
        pq.Enqueue("D", 2);
        pq.Enqueue("E", 3);

        Assert.AreEqual("A", pq.Dequeue()); // highest priority: 3, first inserted
        Assert.AreEqual("C", pq.Dequeue()); // same priority: 3, second inserted
        Assert.AreEqual("E", pq.Dequeue()); // same priority: 3, third inserted
        Assert.AreEqual("D", pq.Dequeue()); // next highest: 2
        Assert.AreEqual("B", pq.Dequeue()); // next: 1
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: InvalidOperationException is thrown
    // Defect(s) Found: Queue does not guard against underflow (dequeueing from an empty queue).
    public void TestPriorityQueue_EmptyQueueThrows()
    {
        var pq = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
    }
}
