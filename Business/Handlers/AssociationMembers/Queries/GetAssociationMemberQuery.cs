
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.AssociationMembers.Queries
{
    public class GetAssociationMemberQuery : IRequest<IDataResult<AssociationMember>>
    {
        public int Id { get; set; }

        public class GetAssociationMemberQueryHandler : IRequestHandler<GetAssociationMemberQuery, IDataResult<AssociationMember>>
        {
            private readonly IAssociationMemberRepository _associationMemberRepository;
            private readonly IMediator _mediator;

            public GetAssociationMemberQueryHandler(IAssociationMemberRepository associationMemberRepository, IMediator mediator)
            {
                _associationMemberRepository = associationMemberRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AssociationMember>> Handle(GetAssociationMemberQuery request, CancellationToken cancellationToken)
            {
                var associationMember = await _associationMemberRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<AssociationMember>(associationMember);
            }
        }
    }
}
