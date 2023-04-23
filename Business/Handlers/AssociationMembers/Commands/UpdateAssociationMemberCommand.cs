
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.AssociationMembers.ValidationRules;


namespace Business.Handlers.AssociationMembers.Commands
{


    public class UpdateAssociationMemberCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }

        public class UpdateAssociationMemberCommandHandler : IRequestHandler<UpdateAssociationMemberCommand, IResult>
        {
            private readonly IAssociationMemberRepository _associationMemberRepository;
            private readonly IMediator _mediator;

            public UpdateAssociationMemberCommandHandler(IAssociationMemberRepository associationMemberRepository, IMediator mediator)
            {
                _associationMemberRepository = associationMemberRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAssociationMemberValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAssociationMemberCommand request, CancellationToken cancellationToken)
            {
                var isThereAssociationMemberRecord = await _associationMemberRepository.GetAsync(u => u.Id == request.Id);


                isThereAssociationMemberRecord.CompanyName = request.CompanyName;
                isThereAssociationMemberRecord.Logo = request.Logo;
                isThereAssociationMemberRecord.Phone = request.Phone;
                isThereAssociationMemberRecord.WebSite = request.WebSite;
                isThereAssociationMemberRecord.CreateDate = request.CreateDate;
                isThereAssociationMemberRecord.ModifiedDate = request.ModifiedDate;
                isThereAssociationMemberRecord.Status = request.Status;


                _associationMemberRepository.Update(isThereAssociationMemberRecord);
                await _associationMemberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

