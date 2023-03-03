namespace AdventOfCode;

public class DayEntryPointAttribute : Attribute
{
    public int DayNumber { get; }

    /// <inheritdoc />
    public DayEntryPointAttribute(int dayNumber)
    {
        DayNumber = dayNumber;
    }
}
