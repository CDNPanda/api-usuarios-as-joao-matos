using FluentValidation;
using Application.DTOs;

public class UsuarioUpdateDtoValidator : AbstractValidator<UsuarioUpdateDto>
{
    public UsuarioUpdateDtoValidator()
    {
        RuleFor(u => u.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("O email é obrigatório.")
            .EmailAddress().WithMessage("Formato de email inválido.");

        RuleFor(u => u.DataNascimento)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .Must(BeAtLeast18YearsOld).WithMessage("O usuário deve ter pelo menos 18 anos.");

        RuleFor(u => u.Telefone)
            .Matches(@"^\(\d{2}\)\s?\d{4,5}-\d{4}$")
            .When(u => !string.IsNullOrWhiteSpace(u.Telefone))
            .WithMessage("O telefone deve estar no formato (XX) XXXXX-XXXX.");

        RuleFor(u => u.Ativo)
            .NotNull().WithMessage("O campo Ativo é obrigatório.");
    }

    private bool BeAtLeast18YearsOld(DateTime dataNascimento)
    {
        var idade = DateTime.Today.Year - dataNascimento.Year;
        if (dataNascimento.Date > DateTime.Today.AddYears(-idade)) idade--;
        return idade >= 18;
    }
}