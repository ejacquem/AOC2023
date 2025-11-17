using System.Globalization;

class Program
{
    public static int[][] direction = {
        new int[2] {-1, -1}, new int[2] {-1, 0}, new int[2] {-1, 1},
        new int[2] { 0, -1},                     new int[2] { 0, 1},
        new int[2] { 1, -1}, new int[2] { 1, 0}, new int[2] { 1, 1},
    };

    public static void part2(string[] input)
    {
        int sum = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                // if (Char.IsSymbol(input[i][j]) || input[i][j] == '*' || input[i][j] == '/' || input[i][j] == '%' || input[i][j] == '@' || input[i][j] == '#')
                if (!Char.IsDigit(input[i][j]) && input[i][j] != '.')
                {
                    System.Console.WriteLine("symbol: " + input[i][j]);
                    sum += getGearRatioAt(i, j, input);
                }
            }
        }
        System.Console.WriteLine(sum);
    }

    public static int getGearRatioAt(int oy, int ox, string[] input)
    {
        int a = 0, b = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int x = ox + j;
                int y = oy + i;
                if (x < 0 || y < 0 || y >= input.Length || x >= input[y].Length)
                {
                    continue;
                }
                if (char.IsDigit(input[y][x]))
                {
                    // System.Console.WriteLine("is digit: " + input[y][x]);
                    int tempx = x;
                    while (tempx >= 0 && char.IsDigit(input[y][tempx]))
                    {
                        tempx--;
                    }
                    tempx++;
                    if (a == 0)
                    {
                        a = extractNumber(input[y].AsSpan(tempx));
                        System.Console.WriteLine("a: " + a);
                    } else if (b == 0)
                    {
                        b = extractNumber(input[y].AsSpan(tempx));
                        System.Console.WriteLine("b: " + b);
                    } else
                    {
                        return 0;
                    }
                    if (j == -1 && char.IsDigit(input[y][x + 1]))
                    {
                        j++; // skipping case like 111
                    }
                    j++; // skipping case like 11. or .11, works with 1.1
                }
            }
        }
        System.Console.WriteLine("ratio: " + (a * b));
        return a * b;
    }

    public static int extractNumber(ReadOnlySpan<char> line)
    {
        int result = 0;

        foreach (char c in line)
        {
            if (!char.IsDigit(c))
                break;
            result = result * 10 + (c - '0');
        }
        return result;
    }

    public static void Main(string[] args)
    {
        string fileName = "input.txt";
        if (args.Length > 0 && args[0] == "example")
        {
            fileName = "example.txt";
        }
        Console.WriteLine("Reading file: " + fileName);

        string[] lines = File.ReadAllLines(fileName);

        part2(lines);
        return;

        // int sum = 0;
        // int number = 0;
        // bool symbolNearby = false;

        // for (int i = 0; i < lines.Length; i++)
        // {
        //     for (int j = 0; j < lines[i].Length; j++)
        //     {
        //         if (Char.IsDigit(lines[i][j]))
        //         {
        //             if (isSymbolAt(i - 1, j - 1, lines) || isSymbolAt(i - 1, j, lines) || isSymbolAt(i - 1, j + 1, lines) || 
        //                 isSymbolAt(i    , j - 1, lines)                                || isSymbolAt(i    , j + 1, lines) || 
        //                 isSymbolAt(i + 1, j - 1, lines) || isSymbolAt(i + 1, j, lines) || isSymbolAt(i + 1, j + 1, lines))
        //             {
        //                 symbolNearby = true;
        //             }
        //             number = number * 10 + (lines[i][j] - '0');
        //         } else
        //         {
        //             if (symbolNearby)
        //             {
        //                 System.Console.WriteLine("adding: " + number);
        //                 sum += number;
        //             }
        //             symbolNearby = false;
        //             number = 0;
        //         }
        //     }
        // }
        // if (symbolNearby)
        // {
        //     System.Console.WriteLine("adding: " + number);
        //     sum += number;
        // }
        // System.Console.WriteLine(sum);
    }

    public static bool isSymbolAt(int i, int j, string[] arr)
    {
        if (j < 0 || i < 0 || i >= arr.Length || j >= arr[i].Length || arr[i][j] == '.' || Char.IsDigit(arr[i][j]))
        {
            return false;
        }
        return true;
    }
}