using System;
using FluentValidation;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace WebApi.Validation.CreateBookValidation
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
           RuleFor(command => command.Model.Name).NotEmpty().MinimumLength(4);

        }
    }
}