using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Application.DTOs;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddValidatorsFromAssemblyContaining<UsuarioCreateDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/usuarios", async (IUsuarioService service, CancellationToken ct) =>
{
  var list = await service.ListarAsync(ct);
  return Results.Ok(list);
});

app.MapGet("/usuarios/{id:int}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
  var user = await service.ObterAsync(id, ct);
  return user != null ? Results.Ok(user) : Results.NotFound(new { error = "Usuário não encontrado" });
});

app.MapPost("/usuarios", async (
    UsuarioCreateDto dto,
    IUsuarioService service,
    IValidator<UsuarioCreateDto> validator,
    CancellationToken ct) =>
{
  var validationResult = await validator.ValidateAsync(dto, ct);
  if (!validationResult.IsValid)
    return Results.ValidationProblem(validationResult.ToDictionary());

  if (await service.EmailJaCadastradoAsync(dto.Email, ct))
    return Results.Conflict(new { error = "Email já cadastrado" });

  var created = await service.CriarAsync(dto, ct);
  return Results.Created($"/usuarios/{created.Id}", created);
});

app.MapPut("/usuarios/{id:int}", async (
    int id,
    UsuarioUpdateDto dto,
    IUsuarioService service,
    IValidator<UsuarioUpdateDto> validator,
    CancellationToken ct) =>
{
  var validationResult = await validator.ValidateAsync(dto, ct);
  if (!validationResult.IsValid)
    return Results.ValidationProblem(validationResult.ToDictionary());

  try
  {
    var updated = await service.AtualizarAsync(id, dto, ct);
    return Results.Ok(updated);
  }
  catch (KeyNotFoundException)
  {
    return Results.NotFound(new { error = "Usuário não encontrado" });
  }
});

app.MapDelete("/usuarios/{id:int}", async (int id, IUsuarioService service, CancellationToken ct) =>
{
  var removed = await service.RemoverAsync(id, ct);
  return removed ? Results.NoContent() : Results.NotFound(new { error = "Usuário não encontrado" });
});

app.MapGet("/health", () => Results.Ok(new { status = "UP" }));

app.Run();