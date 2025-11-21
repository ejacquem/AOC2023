class Program
{
    public static void Main(string[] args)
    {
        string fileName = "input.txt";
        if (args.Length > 0 && args[0] == "example")
        {
            fileName = "example.txt";
        }
        Console.WriteLine("Reading file: " + fileName);

        string[] lines = File.ReadAllLines(fileName);
        List<long> time = new List<long>();
        List<long> dist = new List<long>();
        string timeStr = "";
        string distStr = "";
        foreach (string line in lines[0].Split(" ").AsSpan(1))
        {
            if (line.Length > 0)
            {
                timeStr += line;
                // time.Add(long.Parse(line));
            }
        }
        foreach (string line in lines[1].Split(" ").AsSpan(1))
        {
            if (line.Length > 0)
            {
                distStr += line;
                // dist.Add(long.Parse(line));
            }
        }
        time.Add(long.Parse(timeStr));
        dist.Add(long.Parse(distStr));


        long mul = 1;
        for (int i = 0; i < time.Count; i++)
        {
            // long n = BinarySearch(time[i], dist[i]);
            long n = 0;
            while (beatRecord(n, time[i], dist[i]) == false)
            {
                n++;
            }
            long numberBeaten = time[i] - (2 * n) + 1;
            // System.Console.WriteLine("numberBeaten: " + numberBeaten);
            // System.Console.WriteLine("n: " + n);
            mul *= numberBeaten;
        }
        System.Console.WriteLine("answer: " + mul);
    }

    // public static long BinarySearch(long time, long dist)
    // {
    //     long left = 0;
    //     long right = time;
    //     long mid = 0;

    //     while (left <= right)
    //     {
    //         mid = left + (right - left) / 2;

    //         if (beatRecord(mid, time, dist))
    //             right = mid - 1;
    //         else
    //             left = mid + 1;
    //     }

    //     return mid;
    // }

    public static Boolean beatRecord(long chargeTime, long time, long dist)
    {
        // System.Console.WriteLine($"{chargeTime} * ({time} - {chargeTime}) = " + chargeTime * (time - chargeTime));
        return chargeTime * (time - chargeTime) > dist;
    }

    // time = 10
    // dist = 5

    // charge = 3
    // dist = s * t
    //      = 3 * (10 - 3)
    //      = 21
    // charge = 7
    //      = 7 * (10 - 7)
    //      = 21
    //      = s * (t - s)

}