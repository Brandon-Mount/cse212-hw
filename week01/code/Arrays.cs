public static class Arrays
{
    /********************  Problem 1  ********************
     * MultiplesOf(number, length)
     * Plan
     * 1. Create a new double[] sized to 'length'.
     * 2. For i from 0 to length‑1:
     *      result[i] = number * (i + 1);
     *      (Because the first element should be 1×number, the second 2×number, etc.)
     * 3. Return the filled array.
     ****************************************************/

    public static double[] MultiplesOf(double number, int length)
    {
        // length is guaranteed > 0 per specification
        double[] result = new double[length];

        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }

        return result;
    }

    /********************  Problem 2  ********************
     * RotateListRight(data, amount)
     * Plan
     * 1. Normalize 'amount' in case it equals data.Count
     *      amount %= data.Count;
     * 2. No work if amount == 0 after normalization.
     * 3. Copy the last 'amount' items (tail) to a temporary list.
     * 4. Remove those same items from the end of the original list.
     * 5. Insert the tail items at the front of the list, preserving order.
     *    (Alternatively, use built‑in methods: List<T>.GetRange, RemoveRange, InsertRange.)
     * 6. Because we mutate 'data' in‑place, no return is needed.
     ****************************************************/

    public static void RotateListRight(List<int> data, int amount)
    {
        int n = data.Count;
        amount %= n;           // step 1
        if (amount == 0) return; // step 2

        // step 3 – copy tail
        List<int> tail = data.GetRange(n - amount, amount);

        // step 4 – remove tail from original
        data.RemoveRange(n - amount, amount);

        // step 5 – insert tail at front
        data.InsertRange(0, tail);
    }
}
// Note: The RotateListRight method modifies the input list in place.
//       If you want to return a new list instead, you can create a copy of 'data' at the start,
//       perform the operations on that copy, and return it instead.