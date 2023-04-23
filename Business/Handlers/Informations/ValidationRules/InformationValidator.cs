
using Business.Handlers.Informations.Commands;
using FluentValidation;

namespace Business.Handlers.Informations.ValidationRules
{

    public class CreateInformationValidator : AbstractValidator<CreateInformationCommand>
    {
        public CreateInformationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.Priority).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.ModifiedDate).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();

        }
    }
    public class UpdateInformationValidator : AbstractValidator<UpdateInformationCommand>
    {
        public UpdateInformationValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Text).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.Priority).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.ModifiedDate).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();

        }
    }
}