using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.Schedule;

public class Absence : IEntity
{
    public Guid Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public bool Overlaps(DateOnly date) =>
        StartDate <= date && date <= EndDate;
    public bool Overlaps(DateOnly start, DateOnly end) =>
        StartDate <= end && EndDate >= start;
}
