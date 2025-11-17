using System;

public class Program
{
    public class Game
    {
        public int id;
        Dictionary<string, int> colorMap = new Dictionary<string, int>();
        
        public Game(string input)
        {
            string[] parts = input.Split(":"); // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            string idString = parts[0].Split(" ")[1]; // Game 1
            this.id = int.Parse(idString);
            string[] bags = parts[1].Split(";"); // 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            foreach (string bag in bags)
            {
                string[] colors = bag.Split(","); // 3 blue, 4 red
                foreach (string color in colors)
                {
                    parts = color.Trim().Split(" "); // 3 blue
                    // System.Console.WriteLine($"color: [{color}]");
                    // System.Console.WriteLine($"parts[1]: [{parts[1]}]");
                    // System.Console.WriteLine($"parts[0]: [{parts[0]}]");
                    int colorNb = int.Parse(parts[0]);
                    string colorName = parts[1];
                    if (colorMap.TryGetValue(colorName, out int value))
                    {
                        if (colorNb > colorMap[colorName])
                        {
                            colorMap[colorName] = colorNb;
                        }
                    } else
                    {
                        colorMap[colorName] = colorNb;
                    }
                }
            }
        }

        public void print()
        {
            System.Console.WriteLine("Game: " + this.id);
            foreach (var kvp in colorMap)
            {
                System.Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            }
        }

        public Boolean isPossible(Dictionary<string, int> map)
        {
            foreach (var kvp in map)
            {
                if (colorMap[kvp.Key] > map[kvp.Key])
                {
                    return false;
                }
            }
            return true;
        }

        public int getPower()
        {
            int sum = 1;
            foreach (var kvp in colorMap)
            {
                sum *= kvp.Value;
            }
            return sum;
        }
    }

    public static void Main()
    {
        var map = new Dictionary<string, int>
        {
            ["red"] = 12,
            ["green"] = 13,
            ["blue"] = 14
        };

        string[] lines = File.ReadAllLines("input.txt");
        int sum = 0;

        foreach (string line in lines)
        {
            Game game = new Game(line);
            game.print();
            System.Console.WriteLine("power: " + game.getPower());
            // if (game.isPossible(map))
            // {
            //     sum += game.id;
            // }
            sum += game.getPower();
        }
        Console.WriteLine(sum);
    }
}