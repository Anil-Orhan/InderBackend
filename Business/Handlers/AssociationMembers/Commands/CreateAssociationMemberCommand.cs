
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.AssociationMembers.ValidationRules;

namespace Business.Handlers.AssociationMembers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAssociationMemberCommand : IRequest<IResult>
    {

        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }


        public class CreateAssociationMemberCommandHandler : IRequestHandler<CreateAssociationMemberCommand, IResult>
        {
            private readonly IAssociationMemberRepository _associationMemberRepository;
            private readonly IMediator _mediator;
            public CreateAssociationMemberCommandHandler(IAssociationMemberRepository associationMemberRepository, IMediator mediator)
            {
                _associationMemberRepository = associationMemberRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAssociationMemberValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAssociationMemberCommand request, CancellationToken cancellationToken)
            {
                var isThereAssociationMemberRecord = _associationMemberRepository.Query().Any(u => u.CompanyName == request.CompanyName);

                if (isThereAssociationMemberRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAssociationMember = new AssociationMember
                {
                    CompanyName = request.CompanyName,
                    Logo = request.Logo,
                    Phone = request.Phone,
                    WebSite = request.WebSite,
                    CreateDate = request.CreateDate,
                    ModifiedDate = request.ModifiedDate,
                    Status = request.Status,

                };

                _associationMemberRepository.Add(addedAssociationMember);
                await _associationMemberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}