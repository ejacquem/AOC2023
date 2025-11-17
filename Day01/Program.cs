using System.Diagnostics;

string[] lines = File.ReadAllLines("input.txt");

int sum = 0;

string[] literals = 
{
    "zero","one","two","three","four","five","six","seven","eight","nine"
};

foreach (var line in lines)
{
    int first = 0;
    int last = 0;
    for (int i = 0; i < line.Length; i++)
    {
        first = stringLiteralToInt(line, i);
        if (first >= 0)
        {
            Console.WriteLine("first match litral: " + first);
            break;
        }
        if (Char.IsDigit(line[i]))
        {
            first = line[i] - '0';
            Console.WriteLine("first match number: " + first);
            break;
        }
    }
    for (int i = line.Length - 1; i >= 0; i--)
    {
        last = stringLiteralToInt(line, i);
        if (last >= 0)
        {
            Console.WriteLine("last match litral: " + last);
            break;
        }
        if (Char.IsDigit(line[i]))
        {
            last = line[i] - '0';
            Console.WriteLine("last match number: " + last);
            break;
        }
    }
    sum += first * 10 + last;
}
Console.WriteLine(sum);

int stringLiteralToInt(string str, int start)
{
    int strLen = str.Length - start;
    for (int i = 0; i < literals.Length; i++)
    {
        int j = 0;
        while ( j < strLen && j < literals[i].Length && str[start + j] == literals[i][j])
        {
            j++;
        }
        if (j == literals[i].Length)
        {
            return i;
        }
    }
    return -1;
}