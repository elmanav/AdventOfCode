namespace AdventOfCode.Day1;

public class SonarDepthMeasurement
{
    internal static int GetNumberOfTimesDepthMeasurementIncreases(IReadOnlyList<int> measurements)
    {
        int totalIncreases = 0;
        for (int i = 0; i < measurements.Count - 1; i++)
        {
            if (measurements[i + 1] > measurements[i])
            {
                totalIncreases++;
            }
        }

        return totalIncreases;
    }

    public static void Example()
    {
        const string data = """
        199
        200
        208
        210
        200
        207
        240
        269
        260
        263
        """;
        var res = GetNumberOfTimesDepthMeasurementIncreases(SplitToLines(data).Select(s => Convert.ToInt32(s)).ToArray());
        Console.WriteLine($"Example has {res} measurements that are larger than the previous measurement");
        
        IEnumerable<string> SplitToLines(string input)
        {
            using var reader = new StringReader(input);
            while (reader.ReadLine() is { } line)
            {
                yield return line.Trim();
            }
        }
    }

    public static void Puzzle()
    {
        var data = File.ReadAllLines("./day1/input.txt");
        var res = GetNumberOfTimesDepthMeasurementIncreases(data.Select(s => Convert.ToInt32(s)).ToArray());
        Console.WriteLine($"Example has {res} measurements that are larger than the previous measurement");
    }
}