using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageCommandValidatior : AbstractValidator<UpdateProgrammingLanguageCommand>
    {
        public UpdateProgrammingLanguageCommandValidatior()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(2);
         

        }
    }
}
