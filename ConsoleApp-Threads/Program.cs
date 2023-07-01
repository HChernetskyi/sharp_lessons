public class Program
{

    public static void Main()
    {
        var size = short.MaxValue;
        Random random = new Random();
        int[] values = new int[size];

        for (int i = 0; i < size; ++i)
            values[i] = random.Next();

        Thread sortThread = new Thread(Sort);
        sortThread.Start(values);

        Thread moreThanOneTimeThread = new Thread(MoreThanOneTime);
        moreThanOneTimeThread.Start(values);

        Thread findOddThread = new Thread(FindOdd);
        findOddThread.Start(values);

        Console.WriteLine("Main thread is done.");
    }

    static void Sort(object? obj)
    {
        Array.Sort((int[])obj);
        Console.WriteLine("Thread SORT done.");
    }

    static void MoreThanOneTime(object? obj)
    {
        if (obj is int[])
        {
            int[] values = (int[])obj;
            values[0] = values[1];
            var duplicates = values.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToList();

            System.IO.File.WriteAllLines("duplicates.txt", duplicates.ConvertAll<string>(x => x.ToString()));
            Console.WriteLine("Thread MORE_THAN_ONE_TIME done.");
        }
    }

    static void FindOdd(object? obj)
    {
        int[] values = (int[])obj;
        var oddValues = (from t in values where t % 2 != 0 select t.ToString()).ToList();
        System.IO.File.WriteAllLines("odd.txt", oddValues);
        Console.WriteLine("Thread FIND_EVEN done.");
    }
}
