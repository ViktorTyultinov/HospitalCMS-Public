
using Hospital.Domain.Entities.Users;

namespace Hospital.Domain.Entities.Authentication;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public bool Revoked { get; set; } = false;
}