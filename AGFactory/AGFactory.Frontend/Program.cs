using MudBlazor.Services;
using AGFactory.Frontend.Components;
using AGFactory.Frontend.Repositories;
using AGFactory.Frontend.AuthenticationProviders;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7108") });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProviderTest>();
builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();