using System;
using FluentValidation;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;

namespace WebApi.Validation.CreateAuhtorValidation
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Birthday.Date).NotEmpty();
            RuleFor(command => command.Model.Surname).NotEmpty();


        }
    }
}