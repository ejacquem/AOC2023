class Program
{
    public class Map
    {
        class Row
        {
            public readonly long dest, source, range;
            public Row (long dest, long source, long range)
            {
                this.dest = dest;this.source = source;this.range = range;
            }
        }

        IReadOnlyList<Row> rows;

        public Map(string[] input)
        {
            var list = new List<Row>();
            foreach (string row in input)
            {
                string[] parts = row.Split(" ");
                list.Add(new Row(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2])));
            }
            rows = list;
        }

        // public long getSourceNumber(long destination)
        // {
        // }
        
        public long getDestinationNumber(long source)
        {
            foreach (Row row in rows)
            {
                if (source >= row.source && source < row.source + row.range)
                {
                    return source + (row.dest - row.source);
                }
            }
            return source;
        }

        public void print(long max)
        {
            for (long i = 0; i <= max; i++)
            {
                System.Console.WriteLine($"{i} : {getDestinationNumber(i)}");
            }
        }
    }

    public static long part1(long[] seeds, List<Map> maps)
    {
        long min = long.MaxValue;
        foreach (long seed in seeds)
        {
            long source = seed;
            // System.Console.Write($"{seed}, ");
            foreach (Map map in maps)
            {
                source = map.getDestinationNumber(source);
                // System.Console.Write($"{source}, ");
            }
            min = long.Min(min, source);
            // System.Console.WriteLine();
        }
        // System.Console.WriteLine("Min Location: " + min);
        return min;
    }

    public static long AnalyseSeedRange(long start, long range, List<Map> maps)
    {
        long min = long.MaxValue;
        System.Console.WriteLine("Analysing seed: " + start + " range: " + range);
        for (long j = 0; j < range; j++)
        {
            if (j % 10000000 == 0)
            {
                Console.WriteLine($"Analyse progress: current({j}), range({range}), {(double)j / range * 100:0.00}%");
            }
            long source = start + j;
            // System.Console.Write($"{seed}, ");
            foreach (Map map in maps)
            {
                source = map.getDestinationNumber(source);
                // System.Console.Write($"{source}, ");
            }
            min = long.Min(min, source);
            // System.Console.WriteLine();
        }
        // System.Console.WriteLine(min);
        return min;
    }

    public static void part2(long[] seeds, List<Map> maps)
    {
        long min = long.MaxValue;
        object lockObj = new object();

        Parallel.For(0, seeds.Length / 2, i =>
        {
            long result = AnalyseSeedRange(seeds[i * 2], seeds[i * 2 + 1], maps);

            lock (lockObj)
            {
                min = long.Min(min, result);
            }
        });
        System.Console.WriteLine(min);
    }

    // public static void part2(long[] seeds, List<Map> maps)
    // {
    //     long min = long.MaxValue;
    //     for (int i = 0; i < seeds.Length; i+=2)
    //     {
    //         min = long.Min(min, AnalyseSeedRange(seeds[i], seeds[i+1], maps));
    //     }
    //     System.Console.WriteLine("Min Location: " + min);
    // }

    public static void Main(string[] args)
    {
        string fileName = "input.txt";
        if (args.Length > 0 && args[0] == "example")
        {
            fileName = "example.txt";
        }
        Console.WriteLine("Reading file: " + fileName);

        string[] lines = File.ReadAllLines(fileName);

        // Dictionary<string, string> maps = new Dictionary<string, string>();
        List<Map> maps = new List<Map>();

        // string[] mapInput = {lines[3], lines[4]};
        // Map map = new Map(mapInput);
        // map.print(100);

        long[] seeds = getSeeds(lines[0]);

        for (long i = 2; i < lines.Length; i++)
        {
            List<string> rules = new List<string>();
            if (lines[i].Contains("map:"))
            {
                i++;
                for (; i < lines.Length; i++)
                {
                    if (lines[i].Trim().Length == 0)
                    {
                        break;
                    }
                    rules.Add(lines[i]);
                    // System.Console.WriteLine("1 Parsing: " + lines[i] + " len: " + lines[i].Length);
                }
                Map map = new Map(rules.ToArray());
                maps.Add(map);
            }
        }

        part1(seeds, maps);
        part2(seeds, maps);
    }

    public static long[] getSeeds(string input)
    {
        string[] parts = input.Split(" ");
        long[] seeds = new long[parts.Length - 1];
        for (long i = 1; i < parts.Length; i++)
        {
            seeds[i - 1] = long.Parse(parts[i]);
        }
        return seeds;
    }
}