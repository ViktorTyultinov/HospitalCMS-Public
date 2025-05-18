namespace Hospital.Domain.Entities.Schedule;

public class DailyAvailability
{
    public DayOfWeek Day { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}
