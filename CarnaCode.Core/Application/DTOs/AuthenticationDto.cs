using CarmaCode.Core.Domain;

namespace CarmaCode.Core.Application.DTOs;

public record CreateUserInput
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public bool ImNotARobot { get; set; }
}

public record CreateUserOutput
{
    public Guid Id { get; set; }
}

public record LoginInput
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool ImNotARobot { get; set; }
}

public record LoginOutput
{
    public Auth Auth { get; set; } = default!;
}
