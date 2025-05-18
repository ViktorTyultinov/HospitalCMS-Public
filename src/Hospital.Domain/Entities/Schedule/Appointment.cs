using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Schedule;

public class Appointment : IEntity
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public Guid PatientId { get; set; }

    public bool Overlaps(TimeOnly start, TimeOnly end) =>
        StartTime < end && EndTime > start;
}
