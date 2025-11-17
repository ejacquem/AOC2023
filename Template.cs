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

        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
    }
}