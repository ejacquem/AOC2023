using System.Text.RegularExpressions;

class Program
{
    class ScratchCard
    {
        int id;
        int winningNumberCount;
        List<int> winningNumbers = new List<int>();
        List<int> chosenNumbers = new List<int>();
        bool[] match = new bool[100];

        public ScratchCard(string input, int winningNumberCount)
        {
            var nums = Regex.Matches(input, @"\d+").Select(m => int.Parse(m.Value));
            this.id = nums.ElementAt(0);
            this.winningNumberCount = winningNumberCount;
            // System.Console.WriteLine("nums.Count(): " + nums.Count());
            for (int i = 0; i < winningNumberCount; i++)
            {
                winningNumbers.Add(nums.ElementAt(i + 1));
            }
            for (int i = 0; i < nums.Count() - winningNumberCount - 1; i++)
            {
                // System.Console.WriteLine("Add element: " + nums.ElementAt(i + winningNumberCount + 1));
                int num = nums.ElementAt(i + winningNumberCount + 1);
                chosenNumbers.Add(num);
                match[num] = true;
            }
        }

        public int getPoints()
        {
            int score = 0;
            for (int i = 0; i < winningNumbers.Count(); i++)
            {
                if (match[winningNumbers.ElementAt(i)])
                {
                    score = (score == 0) ? 1 : score * 2;
                }
            }
            return score;
        }

        public int getMatchNumber()
        {
            int score = 0;
            for (int i = 0; i < winningNumbers.Count(); i++)
            {
                if (match[winningNumbers.ElementAt(i)])
                {
                    score++;
                }
            }
            return score;
        }
    }

    public static void part1(string[] lines, int winningNumberCount)
    {
        int sum = 0;
        foreach (var line in lines)
        {
            // Console.WriteLine(line);
            ScratchCard card = new ScratchCard(line, winningNumberCount);
            sum += card.getPoints();
        }
        System.Console.WriteLine(sum);
    }

    public static void part2(string[] lines, int winningNumberCount)
    {
        List<int> idScoreMap = new List<int>(); // link index with score
        int[] idNumberMap = new int[lines.Count()]; // links index with the number of card
        int cardCount = 0;
        for (int cardId = 0; cardId < lines.Count(); cardId++)
        {
            // Console.WriteLine(line);
            ScratchCard card = new ScratchCard(lines[cardId], winningNumberCount);
            idNumberMap[cardId]++; // count original card
            idScoreMap.Add(card.getMatchNumber());
            // System.Console.WriteLine(card.getMatchNumber());

            int score = idScoreMap.ElementAt(cardId);
            for (int j = 0; j < score; j++)
            {
                int copyIndex = cardId + 1 + j;
                idNumberMap[copyIndex] += idNumberMap[cardId]; // add the number of time the current card appear
            }
            // System.Console.WriteLine("cardId: " + cardId + " count: " + idNumberMap.ElementAt(cardId));
            cardCount += idNumberMap.ElementAt(cardId);
        }
        System.Console.WriteLine(cardCount);
    }

    public static void Main(string[] args)
    {
        string fileName = "input.txt";
        int winningNumberCount = 10;
        if (args.Length > 0 && args[0] == "example")
        {
            fileName = "example.txt";
            winningNumberCount = 5;
        }
        Console.WriteLine("Reading file: " + fileName);

        string[] lines = File.ReadAllLines(fileName);
        part1(lines, winningNumberCount);
        part2(lines, winningNumberCount);
    }
}