namespace Hospital.Domain.Interfaces.BaseInterfaces;
public interface IMedicalEvent : IEntity
{
    DateTime CreatedAt { get; set; }
}