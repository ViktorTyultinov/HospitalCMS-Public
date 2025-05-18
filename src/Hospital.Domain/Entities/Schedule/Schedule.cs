using Hospital.Domain.Interfaces.BaseInterfaces;

#pragma warning disable CA1822 // Mark members as static
namespace Hospital.Domain.Entities.Schedule;

public class Schedule : IEntity
{
    private DateOnly Today => DateOnly.FromDateTime(DateTime.Now);
    public Guid Id { get; set; }
    public Guid GeneralPractitionerId { get; set; }
    public List<AvailabilityPattern> AvailabilityPatternHistory { get; set; } = [];
    public AvailabilityPattern? ActivePattern =>
    AvailabilityPatternHistory
        .Where(p => p.ValidFrom <= Today && (p.ValidTo == null || p.ValidTo >= Today))
        .OrderByDescending(p => p.ValidFrom)
        .FirstOrDefault();
    public List<Appointment> Appointments { get; set; } = [];
    public List<Absence> Absences { get; set; } = [];

    private static bool ModificationIsAllowed(DateOnly date) =>
        date > DateOnly.FromDateTime(DateTime.Today.AddDays(2));

    public Guid ScheduleAppointment(DateOnly date, TimeOnly start, TimeSpan duration)
    {
        if (!ModificationIsAllowed(date))
            throw new InvalidOperationException("Appointments must be scheduled at least 48h in advance.");

        var end = start.Add(duration);

        if (Absences.Any(a => a.Overlaps(date)))
            throw new InvalidOperationException("Doctor is absent on this date.");

        if (Appointments.Any(a => a.Date == date && a.Overlaps(start, end)))
            throw new InvalidOperationException("Appointment overlaps with an existing one.");

        var availability = ActivePattern?.GetAvailabilityForTheDay(date.DayOfWeek) ??
            throw new InvalidOperationException("No work time defined.");

        if (start < availability.StartTime || end > availability.EndTime)
            throw new InvalidOperationException("Appointment is outside working hours.");

        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            Date = date,
            StartTime = start,
            EndTime = end
        };

        Appointments.Add(appointment);

        return appointment.Id;
    }

    public void CancelAppointment(Guid appointmentId)
    {
        var removed = Appointments.RemoveAll(a => a.Id == appointmentId);
        if (removed == 0)
            throw new InvalidOperationException("Appointment not found.");
    }

    public Guid RescheduleAppointment(Guid appointmentId, DateOnly newDate, TimeOnly newStart, TimeSpan duration)
    {
        CancelAppointment(appointmentId);
        var id = ScheduleAppointment(newDate, newStart, duration);
        return id;
    }

    public Guid ScheduleAbsence(DateOnly from, DateOnly to)
    {
        if (!ModificationIsAllowed(from))
            throw new InvalidOperationException("Absences must be planned at least 48h in advance.");

        if (Absences.Any(a => a.Overlaps(from, to)))
            throw new InvalidOperationException("Absence overlaps with an existing absence.");

        if (Appointments.Any(a => a.Date >= from && a.Date <= to))
            throw new InvalidOperationException("Appointments exist in this period. Cancel them first.");

        var absence = new Absence
        {
            Id = Guid.NewGuid(),
            StartDate = from,
            EndDate = to
        };

        Absences.Add(absence);
        return absence.Id;
    }

    public void CancelAbsence(Guid absenceId)
    {
        var removed = Absences.RemoveAll(a => a.Id == absenceId);
        if (removed == 0) throw new InvalidOperationException("Absence not found.");
    }

    public void ChangeWorkTime(AvailabilityPattern newWorkTime)
    {
        if (!ModificationIsAllowed(newWorkTime.ValidFrom)) throw new InvalidOperationException("Cannot change work time with less than 48h notice.");
        if (ActivePattern != null) ActivePattern.ValidTo = newWorkTime.ValidFrom.AddDays(-1);
        AvailabilityPatternHistory.Add(newWorkTime);
    }
}