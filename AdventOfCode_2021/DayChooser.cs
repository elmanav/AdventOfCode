using System.Reflection;

namespace AdventOfCode;

public static class DayChooser
{
    public static void Run()
    {
        const int daysOfCode = 25;
        var daysEntries = Enumerable.Range(1, daysOfCode).ToDictionary<int, string, MethodBase?>(i => i.ToString(), _ => null);
        var entryAssembly = Assembly.GetCallingAssembly();
        foreach (var methodBase in entryAssembly.DefinedTypes
                     .Where(type => type is { IsGenericTypeDefinition: false, IsGenericType: false } && IsDayEntryPoint(type))
                     .Select(GetEntryPoint)
                     .Where(m => m != null && m != entryAssembly.EntryPoint))
        {
            daysEntries[GetDayOfEntryPoint(methodBase!).ToString()] = methodBase;
        }

        while (true)
        {
            MethodBase? entryPoint;
            do
            {
                Console.WriteLine($"Entry number of day for adventure: 1-{daysOfCode} (or type 'quit' to exit).");
                Console.Out.Flush();
                var key = Console.ReadLine();
                if (key is null or "quit")
                {
                    Console.WriteLine("Bye!");
                    return;
                }

                if (!daysEntries.TryGetValue(key, out entryPoint))
                {
                    Console.WriteLine($"Invalid choice.");
                    continue;
                }

                if (entryPoint == null)
                {
                    Console.WriteLine($"The puzzle in day {key} has not yet been solved.");
                }

            } while (entryPoint == null);

            try
            {
                entryPoint.Invoke(null, null);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
            Console.WriteLine();
            Console.WriteLine("(Finished; press return.)");
            Console.ReadLine();
        }

    }

    private static MethodBase? GetEntryPoint(TypeInfo type)
    {
        return type.DeclaredMethods.SingleOrDefault(m => m is { IsStatic: true, Name: "Main", IsGenericMethodDefinition: false });
    }

    private static bool IsDayEntryPoint(Type type) =>
        type.GetTypeInfo()
            .GetCustomAttribute<DayEntryPointAttribute>() != null;

    private static int GetDayOfEntryPoint(MemberInfo method) =>
        method.DeclaringType!.GetCustomAttribute<DayEntryPointAttribute>()!.DayNumber;

}
