using CarmaCode.Core.Application;
using CarmaCode.Core.Application.Contracts;
using CarmaCode.Core.Domain.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarnaCode;
using CarnaCode.Infra.Repository;
using CarnaCode.Infra.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IUserRepository, UserLocalStorageRepository>();
builder.Services.AddScoped<IBmiRepository, BmiLocalStorageRepository>();

builder.Services.AddScoped<IAuthenticationService, AuthLocalStorageService>();

builder.Services.AddScoped<IAddBmiOfUser, AddBmiOfUserUseCase>();

await builder.Build().RunAsync();
