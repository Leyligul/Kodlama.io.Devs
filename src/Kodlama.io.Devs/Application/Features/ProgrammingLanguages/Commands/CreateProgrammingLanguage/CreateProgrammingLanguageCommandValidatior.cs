using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage
{
    public class CreateProgrammingLanguageCommandValidatior : AbstractValidator<CreateProgrammingLanguageCommand>
    {
        public CreateProgrammingLanguageCommandValidatior()
        {
            RuleFor(c => c.Name).NotEmpty();

        }
    }
}
