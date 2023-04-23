
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.AssociationMembers.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAssociationMemberCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteAssociationMemberCommandHandler : IRequestHandler<DeleteAssociationMemberCommand, IResult>
        {
            private readonly IAssociationMemberRepository _associationMemberRepository;
            private readonly IMediator _mediator;

            public DeleteAssociationMemberCommandHandler(IAssociationMemberRepository associationMemberRepository, IMediator mediator)
            {
                _associationMemberRepository = associationMemberRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAssociationMemberCommand request, CancellationToken cancellationToken)
            {
                var associationMemberToDelete = _associationMemberRepository.Get(p => p.Id == request.Id);

                _associationMemberRepository.Delete(associationMemberToDelete);
                await _associationMemberRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

