using System.ComponentModel.DataAnnotations;
using Hospital.Domain.Entities.Users;
using Hospital.Domain.Interfaces.BaseInterfaces;

namespace Hospital.Domain.Entities.MedicalHistory;
public class PatientMedicalHistory : IEntity
{
    [Key]
    public required Guid Id { get; set; } = new Guid();
    public PatientProfile? Patient { get; set; }
    private readonly List<IMedicalEvent> _events = [];
    public IReadOnlyCollection<IMedicalEvent> Events => _events.AsReadOnly();

    public void AddEvent(IMedicalEvent medicalEvent)
    {
        _events.Add(medicalEvent);
    }

    public void RemoveEvent(IMedicalEvent medicalEvent)
    {
        _events.Remove(medicalEvent);
    }
}