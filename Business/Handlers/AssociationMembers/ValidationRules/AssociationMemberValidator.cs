
using Business.Handlers.AssociationMembers.Commands;
using FluentValidation;

namespace Business.Handlers.AssociationMembers.ValidationRules
{

    public class CreateAssociationMemberValidator : AbstractValidator<CreateAssociationMemberCommand>
    {
        public CreateAssociationMemberValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.Logo).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.WebSite).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.ModifiedDate).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();

        }
    }
    public class UpdateAssociationMemberValidator : AbstractValidator<UpdateAssociationMemberCommand>
    {
        public UpdateAssociationMemberValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.Logo).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
            RuleFor(x => x.WebSite).NotEmpty();
            RuleFor(x => x.CreateDate).NotEmpty();
            RuleFor(x => x.ModifiedDate).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();

        }
    }
}