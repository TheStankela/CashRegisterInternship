using CashRegister.Domain.Models;

namespace CashRegister.Application.Interfaces
{
    public interface IAuthService
    {
        User Authenticate(UserLogin userLogin);
        string GenerateToken(User user);
    }
}