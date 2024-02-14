using CarmaCode.Core.Application.Contracts;
using CarmaCode.Core.Domain;
using Microsoft.JSInterop;

namespace CarnaCode.Infra.Services;

public class AuthLocalStorageService : IAuthenticationService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly IUserRepository _userRepository;
    
    public AuthLocalStorageService(IJSRuntime jsRuntime, IUserRepository userRepository)
    {
        _jsRuntime = jsRuntime;
        _userRepository = userRepository;
    }
    
    public async Task<bool> IsLogged()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
        return !string.IsNullOrEmpty(token);
    }

    public async Task<Auth> Login(string email, string password)
    {
        var user = await _userRepository.FindByEmail(email);
        
        if (user == null || user.Password != password)
        {
            throw new Exception("Invalid credentials");
        }
        
        var auth = new Auth
        {
            Token = user.Id.ToString(),
        };
        
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "token", auth.Token);
        
        return auth;
    }

    public async Task Logout()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "token");
    }

    public async Task<User> CreateUser(string name, string email, string password)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Password = password
        };
        
        return await _userRepository.CreateAsync(user);
    }
    
    public async Task<User> GetUser()
    {
        var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "token");
        
        var userId = Guid.Parse(token);
        
        var user = await _userRepository.FindByIdAsync(userId);
        
        return user!;
    }
}