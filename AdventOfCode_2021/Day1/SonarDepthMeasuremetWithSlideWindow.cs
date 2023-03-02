namespace AdventOfCode.Day1;

public class SonarDepthMeasurementWithSlideWindow
{
    private static int GetNumberOfTimesDepthMeasurementIncreases(int[] measurements)
    {
        var windowsSums = new List<int>();
        var windowsSize = 3;
        var startIdx = 0;
        var endIdx = windowsSize;
        while (endIdx <= measurements.Length)
        {
            var window = measurements[startIdx..endIdx];
            windowsSums.Add(window.Sum());
            startIdx++;
            endIdx++;
        }

        return SonarDepthMeasurement.GetNumberOfTimesDepthMeasurementIncreases(windowsSums);
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