
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


namespace Business.Handlers.Informations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteInformationCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteInformationCommandHandler : IRequestHandler<DeleteInformationCommand, IResult>
        {
            private readonly IInformationRepository _informationRepository;
            private readonly IMediator _mediator;

            public DeleteInformationCommandHandler(IInformationRepository informationRepository, IMediator mediator)
            {
                _informationRepository = informationRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteInformationCommand request, CancellationToken cancellationToken)
            {
                var informationToDelete = _informationRepository.Get(p => p.Id == request.Id);

                _informationRepository.Delete(informationToDelete);
                await _informationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

