using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Schedule;

public class AvailabilityPattern : IEntity
{
    public Guid Id { get; set; }
    public DateOnly ValidFrom { get; set; }
    public DateOnly? ValidTo { get; set; }
    private ICollection<DailyAvailability> WeeklyAvailability { get; set; } = [];

    public ICollection<DailyAvailability> GetWeeklyAvailability()
    {
        return WeeklyAvailability;
    }

    public void AddAvailability(DailyAvailability availability)
    {
        var current = WeeklyAvailability.FirstOrDefault(av => av.Day == availability.Day);
        if(current is null) WeeklyAvailability.Add(availability);
        else {
            WeeklyAvailability.Remove(current);
            WeeklyAvailability.Add(availability);
        }
    }

    public DailyAvailability? GetAvailabilityForTheDay(DayOfWeek day) =>
        WeeklyAvailability.FirstOrDefault(av => av.Day == day);
}
