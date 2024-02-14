using CarmaCode.Core.Domain;

namespace CarmaCode.Core.Application.Contracts;

public interface IAuthenticationService
{
    public Task<bool> IsLogged();
    public Task<Auth> Login(string email, string password);
    public Task Logout();
    public Task<User> CreateUser(string name, string email, string password);
    public Task<User> GetUser();
}
